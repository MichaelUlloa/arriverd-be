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
    public async Task<IEnumerable<ListExcursionModel>> GetAll()
    {
        var excursions = await _dbContext.Excursions.ToListAsync();
        return excursions.Select(x => new ListExcursionModel(x));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExcursionModel>> GetById(int id)
    {
        var excursion = await _dbContext.Excursions.FindAsync(id);

        if (excursion is null)
            return NotFound();

        return new ExcursionModel(excursion);
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

        short capacity = (short)(request.Capacity - excursion.Capacity);

        if (capacity is not 0)
        {
            short newQuantity = (short)(excursion.AvailableSeats + capacity);

            if (newQuantity < 0)
                return BadRequest("The capacity cannot be changed to less than current reservations.");

            excursion.AvailableSeats = newQuantity;
        }

        _dbContext.Entry(excursion).CurrentValues.SetValues(request);
        excursion.Meeting = request.Schedule.Meeting;
        excursion.Departure = request.Schedule.Departure;
        excursion.Return = request.Schedule.Return;

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id}/images")]
    public async Task<ActionResult<IEnumerable<Image>>> GetAllImages(int id)
    {
        var excursion = await _dbContext.Excursions.FindAsync(id);

        if (excursion is null)
            return NotFound();

        return await _dbContext
            .Images
            .Where(x => x.ExcursionId == id)
            .ToListAsync();
    }

    [HttpPost("{id}/images")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddImage(int id, CreateImageRequest request)
    {
        var excursion = await _dbContext.Excursions.FindAsync(id);

        if (excursion is null)
            return BadRequest("The excursion must have a valid id.");

        excursion.Images.Add(new Image()
        {
            Data = request.Data,
        });

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}/images/{imageId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteImage(int id, int imageId)
    {
        var image = await _dbContext.Images.FirstOrDefaultAsync(x => x.ExcursionId == id && x.Id == imageId);

        if (image is null)
            return NotFound();

        _dbContext.Images.Remove(image);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id}/faq")]
    public async Task<ActionResult<IEnumerable<FAQ>>> GetAllFAQs(int id)
    {
        var excursion = await _dbContext.Excursions.FindAsync(id);

        if (excursion is null)
            return NotFound();

        return await _dbContext
            .FAQs
            .Where(x => x.ExcursionId == id)
            .ToListAsync();
    }

    [HttpPost("{id}/faq")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddFAQ(int id, CreateFAQRequest request)
    {
        var excursion = await _dbContext.Excursions.FindAsync(id);

        if (excursion is null)
            return BadRequest("The excursion must have a valid id.");

        var faq = request.ToFAQ();
        excursion.FAQs.Add(faq);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}/faq/{faqId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateFAQ(int id, int faqId, UpdateFAQRequest request)
    {
        var faq = await _dbContext.FAQs.FirstOrDefaultAsync(x => x.ExcursionId == id && x.Id == faqId);

        if (faq is null)
            return NotFound();

        _dbContext.Entry(faq).CurrentValues.SetValues(request);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}/faq/{faqId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFAQ(int id, int faqId)
    {
        var faq = await _dbContext.FAQs.FirstOrDefaultAsync(x => x.ExcursionId == id && x.Id == faqId);

        if (faq is null)
            return NotFound();

        _dbContext.FAQs.Remove(faq);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }


    [HttpGet("{id}/service")]
    public async Task<ActionResult<IEnumerable<Service>>> GetAllServices(int id)
    {
        var excursion = await _dbContext.Excursions.FindAsync(id);

        if (excursion is null)
            return NotFound();

        return await _dbContext
            .Services
            .Where(x => x.ExcursionId == id)
            .ToListAsync();
    }

    [HttpPost("{id}/service")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddService(int id, CreateServiceRequest request)
    {
        var excursion = await _dbContext.Excursions.FindAsync(id);

        if (excursion is null)
            return BadRequest("The excursion must have a valid id.");

        excursion.Services.Add(new()
        {
            Name = request.Name,
        });

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}/service/{serviceId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateService(int id, int serviceId, UpdateServiceRequest request)
    {
        var service = await _dbContext.Services.FirstOrDefaultAsync(x => x.ExcursionId == id && x.Id == serviceId);

        if (service is null)
            return NotFound();

        service.Name = request.Name;
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}/service/{serviceId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteService(int id, int serviceId)
    {
        var service = await _dbContext.Services.FirstOrDefaultAsync(x => x.ExcursionId == id && x.Id == serviceId);

        if (service is null)
            return NotFound();

        _dbContext.Services.Remove(service);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}
