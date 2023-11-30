using arriverd_be.Data;
using arriverd_be.Entities;
using arriverd_be.Migrations;
using arriverd_be.Models.Excursions;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace arriverd_be.Controllers;

public class ExcursionsController : BaseApiController
{
    private readonly ArriveDbContext _dbContext;
    private readonly IBlobService _blobService;

    public ExcursionsController(ArriveDbContext dbContext, IBlobService blobService)
    {
        _dbContext = dbContext;
        _blobService = blobService;
    }

    [HttpGet]
    public async Task<IEnumerable<ListExcursionModel>> GetAll([FromQuery(Name = "has_available_seats")] bool? hasAvailableSeats)
    {
        var query = _dbContext.Excursions.AsQueryable();

        query = query.Include(x => x.Images!.OrderBy(i => i.Order).Take(1));

        if (hasAvailableSeats is true)
            query = query.Where(x => x.AvailableSeats > 0);
        else if (hasAvailableSeats is false)
            query = query.Where(x => x.AvailableSeats == 0);

        var excursions = await query.ToListAsync();

        return excursions.Select(x => new ListExcursionModel(x));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExcursionModel>> GetById(int id)
    {
        var excursion = await _dbContext.Excursions
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == id);

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

        foreach (var imageRequest in request.Images ?? new())
        {
            await UploadImageAsync(excursion, imageRequest);
        }

        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = excursion.Id }, null);
    }

    private async Task UploadImageAsync(Excursion excursion, CreateImageRequest request)
    {
        var response = await _blobService.UploadImageAsync(request.Data);

        int order = request.Order ?? 0;

        if (order is 0)
        {
            var lastImage = await _dbContext.Images
                .OrderByDescending(x => x.Order)
                .FirstOrDefaultAsync();

            order = (lastImage?.Order ?? 0) + 1;
        }

        await _dbContext.Images.AddAsync(new()
        {
            Excursion = excursion,
            Url = response.ImageUri,
            ImageId = response.ImageId,
            Order = order,
        });
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
                return BadRequest("La capacidad no puede cambiarse a un valor menor que las reservaciones ya hechas.");

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
    public async Task<ActionResult<ListImageModel>> GetAllImages(int id)
    {
        var excursion = await _dbContext.Excursions.FindAsync(id);

        if (excursion is null)
            return NotFound();

        var images = await _dbContext.Images
            .Where(x => x.ExcursionId == id)
            .OrderBy(x => x.Order)
            .ToListAsync();

        return Ok(images.Select(x => new ListImageModel(x)));
    }

    [HttpPost("{id}/images")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddImage(int id, CreateImageRequest request)
    {
        var excursion = await _dbContext.Excursions.FindAsync(id);

        if (excursion is null)
            return BadRequest("La excursión debe tener un id válido.");

        await UploadImageAsync(excursion, request);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}/image/{imageId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateImage(int id, int imageId, UpdateImageRequest request)
    {
        var image = await _dbContext.Images.FirstOrDefaultAsync(x => x.ExcursionId == id && x.Id == imageId);

        if (image is null)
            return NotFound();

        image.Order = request.Order;

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

        await _blobService.DeleteImageAsync((Guid)image.ImageId!);

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
            return BadRequest("La excursión debe tener un id válido.");

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
            return BadRequest("La excursión debe tener una id válida.");

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
