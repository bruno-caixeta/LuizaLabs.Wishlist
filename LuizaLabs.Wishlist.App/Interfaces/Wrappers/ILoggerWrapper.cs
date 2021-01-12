using System;

namespace LuizaLabs.Wishlist.App.Interfaces.Wrappers
{
    public interface ILoggerWrapper
    {
        void LogInformation(string message, params object[] args);
        void LogError(Exception ex, string message, params object[] args);
    }

    public interface ILoggerWrapper<T> : ILoggerWrapper { }
}
