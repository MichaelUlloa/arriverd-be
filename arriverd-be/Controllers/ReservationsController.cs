using arriverd_be.Data;
using arriverd_be.Entities;
using arriverd_be.Models.Reservations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace arriverd_be.Controllers;

public class ReservationsController : BaseApiController
{
    private readonly ArriveDbContext _dbContext;

    public ReservationsController(ArriveDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<Reservation>> GetAll()
        => await _dbContext.Reservations
        .Include(x => x.Excursion)
        .ToListAsync();

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Reservation>> GetById(int id)
    {
        var reservation = await _dbContext.Reservations
            .Include(x => x.Excursion)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (reservation is null)
            return NotFound();

        return reservation;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateReservationRequest request)
    {
        var reservation = request.ToReservation();

        var excursion = await _dbContext.Excursions.FindAsync(request.ExcursionId);

        if (excursion is null)
            return BadRequest("The excursion must have a valid id.");

        reservation.Excursion = excursion;

        await _dbContext.Reservations.AddAsync(reservation);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateReservationRequest request)
    {
        var reservation = await _dbContext.Reservations.FindAsync(id);

        if (reservation is null)
            return NotFound();

        var excursion = await _dbContext.Excursions.FindAsync(request.ExcursionId);

        if (excursion is null)
            return BadRequest("The excursion must have a valid id.");

        reservation.Excursion = excursion;

        _dbContext.Entry(reservation).CurrentValues.SetValues(request);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}
