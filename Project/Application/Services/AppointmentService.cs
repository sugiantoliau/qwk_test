using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Collections.Concurrent;

namespace AppointmentAPI.Application.Services
{
    public class AppointmentService : AppointmentAPI.Application.Interfaces.IAppointmentService
    {
        private static readonly ConcurrentDictionary<DateTime, List<AppointmentAPI.Domain.Customer>> _appointments = new();
        private static readonly ConcurrentDictionary<DateTime, AppointmentAPI.Domain.Holiday> _holidays = new();
        private const int MaxAppointmentsPerDay = 5;
        private static int _currentToken = 1;

        public async Task<bool> BookAppointmentAsync(string customerName)
        {
            var date = DateTime.Today;

            // Find next available date if today is full or a holiday
            while (_holidays.ContainsKey(date) ||
                   (_appointments.ContainsKey(date) && _appointments[date].Count >= MaxAppointmentsPerDay))
            {
                date = date.AddDays(1);
            }

            if (!_appointments.ContainsKey(date))
                _appointments[date] = new List<AppointmentAPI.Domain.Customer>();

            var customer = new AppointmentAPI.Domain.Customer
            {
                Id = _currentToken,
                Name = customerName,
                AppointmentDate = date,
                TokenNumber = _currentToken++
            };

            _appointments[date].Add(customer);
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<AppointmentAPI.Domain.Customer>> GetAppointmentsForDayAsync(DateTime date)
        {
            return await Task.FromResult(_appointments.ContainsKey(date) ? _appointments[date] : Enumerable.Empty<AppointmentAPI.Domain.Customer>());
        }

        public async Task<bool> SetHolidayAsync(DateTime date, string description)
        {
            if (!_holidays.ContainsKey(date))
            {
                _holidays[date] = new AppointmentAPI.Domain.Holiday { Id = _holidays.Count + 1, Date = date, Description = description };
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}
