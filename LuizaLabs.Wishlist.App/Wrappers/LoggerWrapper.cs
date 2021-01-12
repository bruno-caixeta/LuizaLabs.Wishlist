using LuizaLabs.Wishlist.App.Interfaces.Wrappers;
using Microsoft.Extensions.Logging;
using System;

namespace LuizaLabs.Wishlist.App.Wrappers
{
    public class LoggerWrapper<T> : ILoggerWrapper<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerWrapper(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogError(Exception ex, string message, params object[] args)
        {
            _logger.LogError(ex, message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
    }
}
