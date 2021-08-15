using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AireLineTicketSystem.Infraestructure
{
    public static class FactoryMethods
    {
        public static Serilog.Core.Logger CreateLogger()
        {
            var now = DateTime.Now;
            return new LoggerConfiguration()
           .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
           .Enrich.FromLogContext()
           .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Logs_{now.Year}-{now.Month}-{now.Day}.txt"))
            .CreateLogger();
        }

    }
}
