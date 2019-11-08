using ContextConnection.ContextDB;

namespace Infrastructure.DBFactory
{
    public class DbFactory : Disposable, IDbFactory
    {
        private Context dbContext;

        public Context Init()
        {
            return dbContext ?? (dbContext = new Context());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}