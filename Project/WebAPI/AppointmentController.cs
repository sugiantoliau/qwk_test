using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

[ApiController]
[Route("webapi/[controller]")]

public class AppointmentController : ControllerBase
{
    private readonly AppointmentAPI.Application.Interfaces.IAppointmentService _appointmentService;

    public AppointmentController(AppointmentAPI.Application.Interfaces.IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpPost("book")]
    public async Task<IActionResult> BookAppointment([FromBody] string customerName)
    {
        var result = await _appointmentService.BookAppointmentAsync(customerName);
        return result ? Ok("Appointment booked!") : BadRequest("Failed to book appointment.");
    }

    [HttpGet("{date}")]
    public async Task<IActionResult> GetAppointmentsForDay(DateTime date)
    {
        var appointments = await _appointmentService.GetAppointmentsForDayAsync(date);
        return Ok(appointments);
    }

    [HttpPost("holiday")]
    public async Task<IActionResult> SetHoliday([FromBody] AppointmentAPI.Domain.Holiday holiday)
    {
        var result = await _appointmentService.SetHolidayAsync(holiday.Date, holiday.Description);
        return result ? Ok("Holiday set!") : BadRequest("Failed to set holiday.");
    }
}
