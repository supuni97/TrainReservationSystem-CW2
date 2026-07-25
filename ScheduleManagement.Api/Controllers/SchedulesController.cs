using Microsoft.AspNetCore.Mvc;
using ScheduleManagement.Api.DTOs;
using ScheduleManagement.Api.Interfaces;

namespace ScheduleManagement.Api.Controllers;

[ApiController]
[Route("api/schedules")]
public class SchedulesController(IScheduleService scheduleService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<ScheduleDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ScheduleDto>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await scheduleService.GetAllAsync(cancellationToken));

    [HttpGet("{id:int}")]
    [ProducesResponseType<ScheduleDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScheduleDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var schedule = await scheduleService.GetByIdAsync(id, cancellationToken);
        return schedule is null ? NotFound() : Ok(schedule);
    }

    [HttpPost]
    [ProducesResponseType<ScheduleDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ScheduleDto>> Create(CreateScheduleRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var schedule = await scheduleService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { schedule.Id }, schedule);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ProblemDetails { Detail = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateScheduleRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await scheduleService.UpdateAsync(id, request, cancellationToken)
                ? NoContent()
                : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ProblemDetails { Detail = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) =>
        await scheduleService.DeleteAsync(id, cancellationToken)
            ? NoContent()
            : NotFound();
}