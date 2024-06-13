using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Dept_subDept_Module.Data;
using Dept_subDept_Module.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dept_subDept_Module.Services
{
    public class ReminderService : BackgroundService
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReminderService> _logger;

        public ReminderService(IServiceProvider serviceProvider, ILogger<ReminderService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var emailSender = scope.ServiceProvider.GetRequiredService<EmailSender>();

                    var reminders = context.Reminder
                        .Where(r => r.ReminderDateTime <= DateTime.Now && !r.IsSent)
                        .ToList();

                    foreach (var reminder in reminders)
                    {
                        try
                        {
                            await emailSender.SendEmailAsync(reminder.RecipientEmail, "Reminder: " + reminder.Title, "This is a reminder for: " + reminder.Title);
                            reminder.IsSent = true;
                            context.Reminder.Update(reminder);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error sending reminder email.");
                        }
                    }

                    await context.SaveChangesAsync();
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); 
            }
        }

    }
}
