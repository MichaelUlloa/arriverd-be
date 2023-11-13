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

    [HttpGet("{id}/schedules")]
    public async Task<ActionResult<IEnumerable<Schedule>>> GetAllSchedules(int id)
    {
        var excursion = await _dbContext.Excursions.FindAsync(id);

        if (excursion is null)
            return NotFound();

        return await _dbContext
            .Schedules
            .Where(x => x.ExcursionId == id)
            .ToListAsync();
    }

    [HttpPost("{id}/schedules")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddSchedule(int id, CreateScheduleRequest request)
    {
        var excursion = await _dbContext.Excursions.FindAsync(id);

        if (excursion is null)
            return BadRequest("The excursion must have a valid id.");

        var schedule = request.ToSchedule();
        excursion.Schedules.Add(schedule);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}/schedules/{scheduleId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSchedule(int id, int scheduleId, UpdateScheduleRequest request)
    {
        var schedule = await _dbContext.Schedules.FirstOrDefaultAsync(x => x.ExcursionId == id && x.Id == scheduleId);

        if (schedule is null)
            return NotFound();

        _dbContext.Entry(schedule).CurrentValues.SetValues(request);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}/schedules/{scheduleId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSchedule(int id, int scheduleId)
    {
        var schedule = await _dbContext.Schedules.FirstOrDefaultAsync(x => x.ExcursionId == id && x.Id == scheduleId);

        if (schedule is null)
            return NotFound();

        _dbContext.Schedules.Remove(schedule);
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
}
