using arriverd_be.Data;
using arriverd_be.Models.Reservations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace arriverd_be.Controllers;

public class MeController : BaseApiController
{
    private readonly ArriveDbContext _dbContext;

    public MeController(ArriveDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("reservations")]
    public async Task<IEnumerable<ListReservationModel>> GetAll()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var excursions = await _dbContext
            .Reservations
            .Where(x => x.UserId == userId)
            .Include(x => x.Excursion)
            .ToListAsync();

        return excursions.Select(x => new ListReservationModel(x));
    }
}
