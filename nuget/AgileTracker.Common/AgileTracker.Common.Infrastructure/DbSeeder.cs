namespace AgileTracker.Common.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using AgileTracker.Common.Domain.Seeding;

    using Microsoft.EntityFrameworkCore;

    public abstract class DbSeeder: IInitializer
    {
        private readonly IEnumerable<IInitialData> _dataProviders;
        private readonly DbContext _db;

        public DbSeeder(DbContext db, IEnumerable<IInitialData> dataProviders)
        {
            this._db = db;
            this._dataProviders = dataProviders;
        }

        public virtual void Initialize()
        {
            this._db.Database.Migrate();

            foreach(var provider in this._dataProviders)
            {
                if (this.DataSetIsEmpty(provider.EntityType))
                {
                    var entities = provider.GetData();
                    foreach(var entity in entities)
                    {
                        this._db.Add(entity);
                    }
                }
            }

            this._db.SaveChanges();
        }

        protected virtual bool DataSetIsEmpty(Type entityType)
        {
            var setMethod = this.GetType()
                .GetMethod(nameof(this.GetSet), BindingFlags.Instance | BindingFlags.NonPublic)!
                .MakeGenericMethod(entityType);

            var set = setMethod.Invoke(this, Array.Empty<object>());

            var countMethod = typeof(Queryable)
                .GetMethods()
                .First(m => m.Name == nameof(Queryable.Count) && m.GetParameters().Length == 1)
                .MakeGenericMethod(entityType);

            int result = (int)countMethod.Invoke(null, new[] { set })!;

            return result == 0;
        }

        protected virtual DbSet<TEntity> GetSet<TEntity>()
            where TEntity : class
            => _db.Set<TEntity>();
    }
}
