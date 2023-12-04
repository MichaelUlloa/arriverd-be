using arriverd_be.Data;
using arriverd_be.Models;
using arriverd_be.Models.Excursions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace arriverd_be.Controllers;

public class ReportsController : BaseApiController
{
    private readonly ArriveDbContext _dbContext;

    public ReportsController(ArriveDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("most-reserved-excursions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IEnumerable<ListExcursionModel>> MostReservedExcursions()
    {
        var excursions = await _dbContext
            .Excursions
            .Include(e => e.Reservations)
            .OrderByDescending(e => e.Reservations!.Count)
            .ToListAsync();

        return excursions.Select(x => new ListExcursionModel(x));
    }

    [HttpGet("excursions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IEnumerable<ExcursionReservationReport>> ExcursionReservationsData()
    {
        var excursions = await _dbContext.Excursions
            .Where(x => x.IsActive == true)
            .Select(e => new
            {
                Excursion = e,
                ReservationCount = e.Reservations.Select(x => x.Quantity).Sum(x => x)
            })
            .OrderBy(x => x.Excursion.Departure)
            .ToListAsync();

        return excursions.Select(x => new ExcursionReservationReport(x.Excursion, x.ReservationCount));
    }

    [HttpGet("excursions/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IEnumerable<ExcursionDetailsReport>> ExcursionSummary(int id)
    {
        var reservations = await _dbContext.Reservations
            .Include(x => x.User)
            .Include(x => x.Excursion)
            .Include(x => x.PaymentMethod)
            .Where(x => x.Excursion!.Id == id)
            .ToListAsync();

        return reservations.Select(x => new ExcursionDetailsReport(x));
    }
}
