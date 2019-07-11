using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.Models.Stock;

namespace TinyERP4Fun.Interfaces
{
    public interface IDefaultContext
    {
        DbSet<IdentityUser> Users { get; set; }
        DbSet<IdentityRole> Roles { get; set; }
        DatabaseFacade Database { get; }
        ChangeTracker ChangeTracker { get; }
        IModel Model { get; }
        EntityEntry Add( object entity);
        EntityEntry<TEntity> Add<TEntity>( TEntity entity) where TEntity : class;
        Task<EntityEntry> AddAsync( object entity, CancellationToken cancellationToken = default);
        Task<EntityEntry<TEntity>> AddAsync<TEntity>( TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        void AddRange( IEnumerable<object> entities);
        void AddRange( params object[] entities);
        Task AddRangeAsync( IEnumerable<object> entities, CancellationToken cancellationToken = default);
        Task AddRangeAsync( params object[] entities);
        EntityEntry<TEntity> Attach<TEntity>( TEntity entity) where TEntity : class;
        EntityEntry Attach( object entity);
        void AttachRange( params object[] entities);
        void AttachRange( IEnumerable<object> entities);
        void Dispose();
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Entry(object entity);
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
        object Find(Type entityType, params object[] keyValues);
        TEntity Find<TEntity>(params object[] keyValues) where TEntity : class;
        Task<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;
        Task<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken);
        Task<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class;
        Task<object> FindAsync(Type entityType, params object[] keyValues);
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();
        DbQuery<TQuery> Query<TQuery>() where TQuery : class;
        EntityEntry Remove(object entity);
        EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
        void RemoveRange(IEnumerable<object> entities);
        void RemoveRange(params object[] entities);
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();
        EntityEntry Update(object entity);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
        void UpdateRange(params object[] entities);
        void UpdateRange(IEnumerable<object> entities);


        DbSet<Country> Country { get; set; }
        DbSet<State> State { get; set; }
        DbSet<City> City { get; set; }
        DbSet<CommunicationType> CommunicationType { get; set; }
        DbSet<Communication> Communication { get; set; }
        DbSet<Position> Position { get; set; }
        DbSet<BusinessDirection> BusinessDirection { get; set; }
        DbSet<CostItem> CostItem { get; set; }
        DbSet<Person> Person { get; set; }
        DbSet<Company> Company { get; set; }
        DbSet<Employee> Employee { get; set; }
        DbSet<Department> Department { get; set; }
        DbSet<DocumentType> DocumentType { get; set; }
        DbSet<Expences> Expences { get; set; }
        DbSet<Currency> Currency { get; set; }
        DbSet<CurrencyRates> CurrencyRates { get; set; }
        DbSet<Unit> Unit { get; set; }
        DbSet<Warehouse> Warehouse { get; set; }
        DbSet<Item> Item { get; set; }
        DbSet<Stock> Stock { get; set; }

    }
}
