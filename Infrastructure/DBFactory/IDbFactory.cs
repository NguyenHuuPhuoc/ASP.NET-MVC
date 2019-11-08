using ContextConnection.ContextDB;
using System;

namespace Infrastructure.DBFactory
{
    public interface IDbFactory : IDisposable
    {
        Context Init();
    }
}