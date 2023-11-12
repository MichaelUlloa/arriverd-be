using arriverd_be.Data;
using arriverd_be.Entities;
using arriverd_be.Models.Excursions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace arriverd_be.Controllers;

public class ExcursionsController : BaseApiController
{
    private readonly ArriveDbContext _dbContext;

    public ExcursionsController(ArriveDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<Excursion>> GetAll()
        => await _dbContext.Excursions.ToListAsync();

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Excursion>> GetById(int id)
    {
        var excursion = await _dbContext.Excursions.FindAsync(id);

        if (excursion is null)
            return NotFound();

        return excursion;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateExcursionRequest request)
    {
        var excursion = request.ToExcursion();

        await _dbContext.Excursions.AddAsync(excursion);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = excursion.Id }, null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateExcursionRequest request)
    {
        var excursion = await _dbContext.Excursions.FindAsync(id);

        if (excursion is null)
            return NotFound();

        _dbContext.Entry(excursion).CurrentValues.SetValues(request);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}
