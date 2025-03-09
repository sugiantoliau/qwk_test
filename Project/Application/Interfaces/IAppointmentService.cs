using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AppointmentAPI.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<bool> BookAppointmentAsync(string customerName);
        Task<IEnumerable<AppointmentAPI.Domain.Customer>> GetAppointmentsForDayAsync(DateTime date);
        Task<bool> SetHolidayAsync(DateTime date, string description);
    }
}