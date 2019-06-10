using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TinyERP4Fun.Scheduler
{
    internal class TimedHostedServiceUpdateCurrencies : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        
        public TimedHostedServiceUpdateCurrencies(ILogger<TimedHostedServiceUpdateCurrencies> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(60));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");
            var options = CommonFunctions.DefaultContextOptions.GetOptions();
            using (var context = new DefaultContext(options))
            {
                var CRC = new CurrencyRatesController(context);
                await CRC.UpdateBYNVoid();
            }
            
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
