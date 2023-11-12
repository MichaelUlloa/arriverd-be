using arriverd_be.Data;
using arriverd_be.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace arriverd_be.Controllers;

[Route("api/payment-methods")]
public class PaymentMethodController : BaseApiController
{
    private readonly ILogger<PaymentMethodController> _logger;
    private readonly ArriveDbContext _dbContext;

    public PaymentMethodController(
        ILogger<PaymentMethodController> logger,
        ArriveDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<PaymentMethod>> Get()
        => await _dbContext.PaymentMethods.ToListAsync();
}