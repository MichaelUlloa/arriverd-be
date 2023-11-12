using arriverd_be.Data;
using arriverd_be.Entities;
using Microsoft.AspNetCore.Mvc;

namespace arriverd_be.Controllers;

[ApiController]
[Route("api/payment-methods")]
public class PaymentMethodController : ControllerBase
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
    public IEnumerable<PaymentMethod> Get()
        => _dbContext.PaymentMethods;
}