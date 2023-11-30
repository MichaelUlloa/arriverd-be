using arriverd_be.Data;
using arriverd_be.Entities;
using arriverd_be.Models.Invoices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace arriverd_be.Controllers;

public class InvoicesController : BaseApiController
{
    private readonly ArriveDbContext _dbContext;

    public InvoicesController(ArriveDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<Invoice>> GetAll()
        => await _dbContext.Invoices
        .Include(x => x.Reservation)
        .Include(x => x.PaymentMethod)
        .ToListAsync();

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Invoice>> GetById(int id)
    {
        var invoice = await _dbContext.Invoices
            .Include(x => x.Reservation)
            .Include(x => x.PaymentMethod)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (invoice is null)
            return NotFound();

        return invoice;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateInvoiceRequest request)
    {
        var invoice = request.ToInvoice();

        var reservation = await _dbContext.Reservations.FindAsync(request.ReservationId);

        if (reservation is null)
            return BadRequest("La reservación debe tener un id válido.");

        invoice.Reservation = reservation;

        var paymentMethod = await _dbContext.PaymentMethods.FindAsync(request.PaymentMethodId);

        if (paymentMethod is null)
            return BadRequest("El método de pago debe tener un id válido.");

        invoice.PaymentMethod = paymentMethod;

        await _dbContext.Invoices.AddAsync(invoice);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateInvoiceRequest request)
    {
        var invoice = await _dbContext.Invoices.FindAsync(id);

        if (invoice is null)
            return NotFound();

        _dbContext.Entry(invoice).CurrentValues.SetValues(request);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}
