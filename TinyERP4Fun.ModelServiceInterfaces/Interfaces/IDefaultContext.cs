﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.Models.Stock;

namespace TinyERP4Fun.Interfaces
{
    public interface IDefaultContext
    {
        //
        // Summary:
        //     Provides access to database related information and operations for this context.
        public virtual DatabaseFacade Database { get; }
        //
        // Summary:
        //     Provides access to information and operations for entity instances this context
        //     is tracking.
        public virtual ChangeTracker ChangeTracker { get; }
        //
        // Summary:
        //     The metadata about the shape of entities, the relationships between them, and
        //     how they map to the database.
        public virtual IModel Model { get; }

        //
        // Summary:
        //     Begins tracking the given entity, and any other reachable entities that are not
        //     already being tracked, in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state such that they will be inserted into the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //     Use Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry.State to set the
        //     state of only a single entity.
        //
        // Parameters:
        //   entity:
        //     The entity to add.
        //
        // Returns:
        //     The Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry for the entity.
        //     The entry provides access to change tracking information and operations for the
        //     entity.
        public virtual EntityEntry Add([NotNullAttribute] object entity);
        //
        // Summary:
        //     Begins tracking the given entity, and any other reachable entities that are not
        //     already being tracked, in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state such that they will be inserted into the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //     Use Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry.State to set the
        //     state of only a single entity.
        //
        // Parameters:
        //   entity:
        //     The entity to add.
        //
        // Type parameters:
        //   TEntity:
        //     The type of the entity.
        //
        // Returns:
        //     The Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry`1 for the entity.
        //     The entry provides access to change tracking information and operations for the
        //     entity.
        public virtual EntityEntry<TEntity> Add<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        //
        // Summary:
        //     Begins tracking the given entity, and any other reachable entities that are not
        //     already being tracked, in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state such that they will be inserted into the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //     Use Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry.State to set the
        //     state of only a single entity.
        //     This method is async only to allow special value generators, such as the one
        //     used by 'Microsoft.EntityFrameworkCore.Metadata.SqlServerValueGenerationStrategy.SequenceHiLo',
        //     to access the database asynchronously. For all other cases the non async method
        //     should be used.
        //
        // Parameters:
        //   entity:
        //     The entity to add.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the task to
        //     complete.
        //
        // Returns:
        //     A task that represents the asynchronous Add operation. The task result contains
        //     the Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry for the entity.
        //     The entry provides access to change tracking information and operations for the
        //     entity.
        [AsyncStateMachine(typeof(< AddAsync > d__72))]
        public virtual Task<EntityEntry> AddAsync([NotNullAttribute] object entity, CancellationToken cancellationToken = default);
        //
        // Summary:
        //     Begins tracking the given entity, and any other reachable entities that are not
        //     already being tracked, in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state such that they will be inserted into the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //     This method is async only to allow special value generators, such as the one
        //     used by 'Microsoft.EntityFrameworkCore.Metadata.SqlServerValueGenerationStrategy.SequenceHiLo',
        //     to access the database asynchronously. For all other cases the non async method
        //     should be used.
        //
        // Parameters:
        //   entity:
        //     The entity to add.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the task to
        //     complete.
        //
        // Type parameters:
        //   TEntity:
        //     The type of the entity.
        //
        // Returns:
        //     A task that represents the asynchronous Add operation. The task result contains
        //     the Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry`1 for the entity.
        //     The entry provides access to change tracking information and operations for the
        //     entity.
        [AsyncStateMachine(typeof(DbContext.< AddAsync > d__66<>))]
        public virtual Task<EntityEntry<TEntity>> AddAsync<TEntity>([NotNullAttribute] TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        //
        // Summary:
        //     Begins tracking the given entities, and any other reachable entities that are
        //     not already being tracked, in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state such that they will be inserted into the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //
        // Parameters:
        //   entities:
        //     The entities to add.
        public virtual void AddRange([NotNullAttribute] IEnumerable<object> entities);
        //
        // Summary:
        //     Begins tracking the given entities, and any other reachable entities that are
        //     not already being tracked, in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state such that they will be inserted into the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //
        // Parameters:
        //   entities:
        //     The entities to add.
        public virtual void AddRange([NotNullAttribute] params object[] entities);
        //
        // Summary:
        //     Begins tracking the given entity, and any other reachable entities that are not
        //     already being tracked, in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state such that they will be inserted into the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //     This method is async only to allow special value generators, such as the one
        //     used by 'Microsoft.EntityFrameworkCore.Metadata.SqlServerValueGenerationStrategy.SequenceHiLo',
        //     to access the database asynchronously. For all other cases the non async method
        //     should be used.
        //
        // Parameters:
        //   entities:
        //     The entities to add.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the task to
        //     complete.
        //
        // Returns:
        //     A task that represents the asynchronous operation.
        [AsyncStateMachine(typeof(< AddRangeAsync > d__84))]
        public virtual Task AddRangeAsync([NotNullAttribute] IEnumerable<object> entities, CancellationToken cancellationToken = default);
        //
        // Summary:
        //     Begins tracking the given entity, and any other reachable entities that are not
        //     already being tracked, in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state such that they will be inserted into the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //     This method is async only to allow special value generators, such as the one
        //     used by 'Microsoft.EntityFrameworkCore.Metadata.SqlServerValueGenerationStrategy.SequenceHiLo',
        //     to access the database asynchronously. For all other cases the non async method
        //     should be used.
        //
        // Parameters:
        //   entities:
        //     The entities to add.
        //
        // Returns:
        //     A task that represents the asynchronous operation.
        public virtual Task AddRangeAsync([NotNullAttribute] params object[] entities);
        //
        // Summary:
        //     Begins tracking the given entity in the Microsoft.EntityFrameworkCore.EntityState.Unchanged
        //     state such that no operation will be performed when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //     A recursive search of the navigation properties will be performed to find reachable
        //     entities that are not already being tracked by the context. These entities will
        //     also begin to be tracked by the context. If a reachable entity has its primary
        //     key value set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Unchanged
        //     state. If the primary key value is not set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state. An entity is considered to have its primary key value set if the primary
        //     key property is set to anything other than the CLR default for the property type.
        //     Use Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry.State to set the
        //     state of only a single entity.
        //
        // Parameters:
        //   entity:
        //     The entity to attach.
        //
        // Type parameters:
        //   TEntity:
        //     The type of the entity.
        //
        // Returns:
        //     The Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry`1 for the entity.
        //     The entry provides access to change tracking information and operations for the
        //     entity.
        public virtual EntityEntry<TEntity> Attach<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        //
        // Summary:
        //     Begins tracking the given entity in the Microsoft.EntityFrameworkCore.EntityState.Unchanged
        //     state such that no operation will be performed when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //     A recursive search of the navigation properties will be performed to find reachable
        //     entities that are not already being tracked by the context. These entities will
        //     also begin to be tracked by the context. If a reachable entity has its primary
        //     key value set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Unchanged
        //     state. If the primary key value is not set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state. An entity is considered to have its primary key value set if the primary
        //     key property is set to anything other than the CLR default for the property type.
        //     Use Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry.State to set the
        //     state of only a single entity.
        //
        // Parameters:
        //   entity:
        //     The entity to attach.
        //
        // Returns:
        //     The Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry for the entity.
        //     The entry provides access to change tracking information and operations for the
        //     entity.
        public virtual EntityEntry Attach([NotNullAttribute] object entity);
        //
        // Summary:
        //     Begins tracking the given entities in the Microsoft.EntityFrameworkCore.EntityState.Unchanged
        //     state such that no operation will be performed when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //     A recursive search of the navigation properties will be performed to find reachable
        //     entities that are not already being tracked by the context. These entities will
        //     also begin to be tracked by the context. If a reachable entity has its primary
        //     key value set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Unchanged
        //     state. If the primary key value is not set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state. An entity is considered to have its primary key value set if the primary
        //     key property is set to anything other than the CLR default for the property type.
        //
        // Parameters:
        //   entities:
        //     The entities to attach.
        public virtual void AttachRange([NotNullAttribute] params object[] entities);
        //
        // Summary:
        //     Begins tracking the given entities in the Microsoft.EntityFrameworkCore.EntityState.Unchanged
        //     state such that no operation will be performed when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //     A recursive search of the navigation properties will be performed to find reachable
        //     entities that are not already being tracked by the context. These entities will
        //     also begin to be tracked by the context. If a reachable entity has its primary
        //     key value set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Unchanged
        //     state. If the primary key value is not set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state. An entity is considered to have its primary key value set if the primary
        //     key property is set to anything other than the CLR default for the property type.
        //
        // Parameters:
        //   entities:
        //     The entities to attach.
        public virtual void AttachRange([NotNullAttribute] IEnumerable<object> entities);
        //
        // Summary:
        //     Releases the allocated resources for this context.
        public virtual void Dispose();
        //
        // Summary:
        //     Gets an Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry`1 for the given
        //     entity. The entry provides access to change tracking information and operations
        //     for the entity.
        //
        // Parameters:
        //   entity:
        //     The entity to get the entry for.
        //
        // Type parameters:
        //   TEntity:
        //     The type of the entity.
        //
        // Returns:
        //     The entry for the given entity.
        public virtual EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        //
        // Summary:
        //     Gets an Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry for the given
        //     entity. The entry provides access to change tracking information and operations
        //     for the entity.
        //     This method may be called on an entity that is not tracked. You can then set
        //     the Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry.State property on
        //     the returned entry to have the context begin tracking the entity in the specified
        //     state.
        //
        // Parameters:
        //   entity:
        //     The entity to get the entry for.
        //
        // Returns:
        //     The entry for the given entity.
        public virtual EntityEntry Entry([NotNullAttribute] object entity);
        //
        // Summary:
        //     Determines whether the specified object is equal to the current object.
        //
        // Parameters:
        //   obj:
        //     The object to compare with the current object.
        //
        // Returns:
        //     true if the specified object is equal to the current object; otherwise, false.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj);
        //
        // Summary:
        //     Finds an entity with the given primary key values. If an entity with the given
        //     primary key values is being tracked by the context, then it is returned immediately
        //     without making a request to the database. Otherwise, a query is made to the database
        //     for an entity with the given primary key values and this entity, if found, is
        //     attached to the context and returned. If no entity is found, then null is returned.
        //
        // Parameters:
        //   entityType:
        //     The type of entity to find.
        //
        //   keyValues:
        //     The values of the primary key for the entity to be found.
        //
        // Returns:
        //     The entity found, or null.
        public virtual object Find([NotNullAttribute] Type entityType, [CanBeNullAttribute] params object[] keyValues);
        //
        // Summary:
        //     Finds an entity with the given primary key values. If an entity with the given
        //     primary key values is being tracked by the context, then it is returned immediately
        //     without making a request to the database. Otherwise, a query is made to the database
        //     for an entity with the given primary key values and this entity, if found, is
        //     attached to the context and returned. If no entity is found, then null is returned.
        //
        // Parameters:
        //   keyValues:
        //     The values of the primary key for the entity to be found.
        //
        // Type parameters:
        //   TEntity:
        //     The type of entity to find.
        //
        // Returns:
        //     The entity found, or null.
        public virtual TEntity Find<TEntity>([CanBeNullAttribute] params object[] keyValues) where TEntity : class;
        //
        // Summary:
        //     Finds an entity with the given primary key values. If an entity with the given
        //     primary key values is being tracked by the context, then it is returned immediately
        //     without making a request to the database. Otherwise, a query is made to the database
        //     for an entity with the given primary key values and this entity, if found, is
        //     attached to the context and returned. If no entity is found, then null is returned.
        //
        // Parameters:
        //   keyValues:
        //     The values of the primary key for the entity to be found.
        //
        // Type parameters:
        //   TEntity:
        //     The type of entity to find.
        //
        // Returns:
        //     The entity found, or null.
        public virtual Task<TEntity> FindAsync<TEntity>([CanBeNullAttribute] params object[] keyValues) where TEntity : class;
        //
        // Summary:
        //     Finds an entity with the given primary key values. If an entity with the given
        //     primary key values is being tracked by the context, then it is returned immediately
        //     without making a request to the database. Otherwise, a query is made to the database
        //     for an entity with the given primary key values and this entity, if found, is
        //     attached to the context and returned. If no entity is found, then null is returned.
        //
        // Parameters:
        //   entityType:
        //     The type of entity to find.
        //
        //   keyValues:
        //     The values of the primary key for the entity to be found.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the task to
        //     complete.
        //
        // Returns:
        //     The entity found, or null.
        public virtual Task<object> FindAsync([NotNullAttribute] Type entityType, [CanBeNullAttribute] object[] keyValues, CancellationToken cancellationToken);
        //
        // Summary:
        //     Finds an entity with the given primary key values. If an entity with the given
        //     primary key values is being tracked by the context, then it is returned immediately
        //     without making a request to the database. Otherwise, a query is made to the database
        //     for an entity with the given primary key values and this entity, if found, is
        //     attached to the context and returned. If no entity is found, then null is returned.
        //
        // Parameters:
        //   keyValues:
        //     The values of the primary key for the entity to be found.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the task to
        //     complete.
        //
        // Type parameters:
        //   TEntity:
        //     The type of entity to find.
        //
        // Returns:
        //     The entity found, or null.
        public virtual Task<TEntity> FindAsync<TEntity>([CanBeNullAttribute] object[] keyValues, CancellationToken cancellationToken) where TEntity : class;
        //
        // Summary:
        //     Finds an entity with the given primary key values. If an entity with the given
        //     primary key values is being tracked by the context, then it is returned immediately
        //     without making a request to the database. Otherwise, a query is made to the database
        //     for an entity with the given primary key values and this entity, if found, is
        //     attached to the context and returned. If no entity is found, then null is returned.
        //
        // Parameters:
        //   entityType:
        //     The type of entity to find.
        //
        //   keyValues:
        //     The values of the primary key for the entity to be found.
        //
        // Returns:
        //     The entity found, or null.
        public virtual Task<object> FindAsync([NotNullAttribute] Type entityType, [CanBeNullAttribute] params object[] keyValues);
        //
        // Summary:
        //     Serves as the default hash function.
        //
        // Returns:
        //     A hash code for the current object.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode();
        //
        // Summary:
        //     Creates a Microsoft.EntityFrameworkCore.DbQuery`1 that can be used to query instances
        //     of TQuery.
        //
        // Type parameters:
        //   TQuery:
        //     The type of query for which a DbQuery should be returned.
        //
        // Returns:
        //     A DbQuery for the given query type.
        public virtual DbQuery<TQuery> Query<TQuery>() where TQuery : class;
        //
        // Summary:
        //     Begins tracking the given entity in the Microsoft.EntityFrameworkCore.EntityState.Deleted
        //     state such that it will be removed from the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //
        // Parameters:
        //   entity:
        //     The entity to remove.
        //
        // Returns:
        //     The Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry for the entity.
        //     The entry provides access to change tracking information and operations for the
        //     entity.
        //
        // Remarks:
        //     If the entity is already tracked in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state then the context will stop tracking the entity (rather than marking it
        //     as Microsoft.EntityFrameworkCore.EntityState.Deleted) since the entity was previously
        //     added to the context and does not exist in the database.
        //     Any other reachable entities that are not already being tracked will be tracked
        //     in the same way that they would be if Microsoft.EntityFrameworkCore.DbContext.Attach(System.Object)
        //     was called before calling this method. This allows any cascading actions to be
        //     applied when Microsoft.EntityFrameworkCore.DbContext.SaveChanges is called.
        //     Use Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry.State to set the
        //     state of only a single entity.
        public virtual EntityEntry Remove([NotNullAttribute] object entity);
        //
        // Summary:
        //     Begins tracking the given entity in the Microsoft.EntityFrameworkCore.EntityState.Deleted
        //     state such that it will be removed from the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //
        // Parameters:
        //   entity:
        //     The entity to remove.
        //
        // Type parameters:
        //   TEntity:
        //     The type of the entity.
        //
        // Returns:
        //     The Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry`1 for the entity.
        //     The entry provides access to change tracking information and operations for the
        //     entity.
        //
        // Remarks:
        //     If the entity is already tracked in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state then the context will stop tracking the entity (rather than marking it
        //     as Microsoft.EntityFrameworkCore.EntityState.Deleted) since the entity was previously
        //     added to the context and does not exist in the database.
        //     Any other reachable entities that are not already being tracked will be tracked
        //     in the same way that they would be if Microsoft.EntityFrameworkCore.DbContext.Attach``1(``0)
        //     was called before calling this method. This allows any cascading actions to be
        //     applied when Microsoft.EntityFrameworkCore.DbContext.SaveChanges is called.
        //     Use Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry.State to set the
        //     state of only a single entity.
        public virtual EntityEntry<TEntity> Remove<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        //
        // Summary:
        //     Begins tracking the given entity in the Microsoft.EntityFrameworkCore.EntityState.Deleted
        //     state such that it will be removed from the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //
        // Parameters:
        //   entities:
        //     The entities to remove.
        //
        // Remarks:
        //     If any of the entities are already tracked in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state then the context will stop tracking those entities (rather than marking
        //     them as Microsoft.EntityFrameworkCore.EntityState.Deleted) since those entities
        //     were previously added to the context and do not exist in the database.
        //     Any other reachable entities that are not already being tracked will be tracked
        //     in the same way that they would be if Microsoft.EntityFrameworkCore.DbContext.AttachRange(System.Collections.Generic.IEnumerable{System.Object})
        //     was called before calling this method. This allows any cascading actions to be
        //     applied when Microsoft.EntityFrameworkCore.DbContext.SaveChanges is called.
        public virtual void RemoveRange([NotNullAttribute] IEnumerable<object> entities);
        //
        // Summary:
        //     Begins tracking the given entity in the Microsoft.EntityFrameworkCore.EntityState.Deleted
        //     state such that it will be removed from the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //
        // Parameters:
        //   entities:
        //     The entities to remove.
        //
        // Remarks:
        //     If any of the entities are already tracked in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state then the context will stop tracking those entities (rather than marking
        //     them as Microsoft.EntityFrameworkCore.EntityState.Deleted) since those entities
        //     were previously added to the context and do not exist in the database.
        //     Any other reachable entities that are not already being tracked will be tracked
        //     in the same way that they would be if Microsoft.EntityFrameworkCore.DbContext.AttachRange(System.Object[])
        //     was called before calling this method. This allows any cascading actions to be
        //     applied when Microsoft.EntityFrameworkCore.DbContext.SaveChanges is called.
        public virtual void RemoveRange([NotNullAttribute] params object[] entities);
        //
        // Summary:
        //     Saves all changes made in this context to the database.
        //
        // Parameters:
        //   acceptAllChangesOnSuccess:
        //     Indicates whether Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges
        //     is called after the changes have been sent successfully to the database.
        //
        // Returns:
        //     The number of state entries written to the database.
        //
        // Exceptions:
        //   T:Microsoft.EntityFrameworkCore.DbUpdateException:
        //     An error is encountered while saving to the database.
        //
        //   T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException:
        //     A concurrency violation is encountered while saving to the database. A concurrency
        //     violation occurs when an unexpected number of rows are affected during save.
        //     This is usually because the data in the database has been modified since it was
        //     loaded into memory.
        //
        // Remarks:
        //     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
        //     to discover any changes to entity instances before saving to the underlying database.
        //     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
        public virtual int SaveChanges(bool acceptAllChangesOnSuccess);
        //
        // Summary:
        //     Saves all changes made in this context to the database.
        //
        // Returns:
        //     The number of state entries written to the database.
        //
        // Exceptions:
        //   T:Microsoft.EntityFrameworkCore.DbUpdateException:
        //     An error is encountered while saving to the database.
        //
        //   T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException:
        //     A concurrency violation is encountered while saving to the database. A concurrency
        //     violation occurs when an unexpected number of rows are affected during save.
        //     This is usually because the data in the database has been modified since it was
        //     loaded into memory.
        //
        // Remarks:
        //     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
        //     to discover any changes to entity instances before saving to the underlying database.
        //     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
        public virtual int SaveChanges();
        //
        // Summary:
        //     Asynchronously saves all changes made in this context to the database.
        //
        // Parameters:
        //   acceptAllChangesOnSuccess:
        //     Indicates whether Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges
        //     is called after the changes have been sent successfully to the database.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the task to
        //     complete.
        //
        // Returns:
        //     A task that represents the asynchronous save operation. The task result contains
        //     the number of state entries written to the database.
        //
        // Exceptions:
        //   T:Microsoft.EntityFrameworkCore.DbUpdateException:
        //     An error is encountered while saving to the database.
        //
        //   T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException:
        //     A concurrency violation is encountered while saving to the database. A concurrency
        //     violation occurs when an unexpected number of rows are affected during save.
        //     This is usually because the data in the database has been modified since it was
        //     loaded into memory.
        //
        // Remarks:
        //     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
        //     to discover any changes to entity instances before saving to the underlying database.
        //     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
        //     Multiple active operations on the same context instance are not supported. Use
        //     'await' to ensure that any asynchronous operations have completed before calling
        //     another method on this context.
        [AsyncStateMachine(typeof(< SaveChangesAsync > d__53))]
        public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        //
        // Summary:
        //     Asynchronously saves all changes made in this context to the database.
        //
        // Parameters:
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the task to
        //     complete.
        //
        // Returns:
        //     A task that represents the asynchronous save operation. The task result contains
        //     the number of state entries written to the database.
        //
        // Exceptions:
        //   T:Microsoft.EntityFrameworkCore.DbUpdateException:
        //     An error is encountered while saving to the database.
        //
        //   T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException:
        //     A concurrency violation is encountered while saving to the database. A concurrency
        //     violation occurs when an unexpected number of rows are affected during save.
        //     This is usually because the data in the database has been modified since it was
        //     loaded into memory.
        //
        // Remarks:
        //     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
        //     to discover any changes to entity instances before saving to the underlying database.
        //     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
        //     Multiple active operations on the same context instance are not supported. Use
        //     'await' to ensure that any asynchronous operations have completed before calling
        //     another method on this context.
        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        //
        // Summary:
        //     Creates a Microsoft.EntityFrameworkCore.DbSet`1 that can be used to query and
        //     save instances of TEntity.
        //
        // Type parameters:
        //   TEntity:
        //     The type of entity for which a set should be returned.
        //
        // Returns:
        //     A set for the given entity type.
        public virtual DbSet<TEntity> Set<TEntity>() where TEntity : class;
        //
        // Summary:
        //     Returns a string that represents the current object.
        //
        // Returns:
        //     A string that represents the current object.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString();
        //
        // Summary:
        //     Begins tracking the given entity in the Microsoft.EntityFrameworkCore.EntityState.Modified
        //     state such that it will be updated in the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //     All properties of the entity will be marked as modified. To mark only some properties
        //     as modified, use Microsoft.EntityFrameworkCore.DbContext.Attach(System.Object)
        //     to begin tracking the entity in the Microsoft.EntityFrameworkCore.EntityState.Unchanged
        //     state and then use the returned Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry
        //     to mark the desired properties as modified.
        //     A recursive search of the navigation properties will be performed to find reachable
        //     entities that are not already being tracked by the context. These entities will
        //     also begin to be tracked by the context. If a reachable entity has its primary
        //     key value set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Modified
        //     state. If the primary key value is not set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state. An entity is considered to have its primary key value set if the primary
        //     key property is set to anything other than the CLR default for the property type.
        //     Use Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry.State to set the
        //     state of only a single entity.
        //
        // Parameters:
        //   entity:
        //     The entity to update.
        //
        // Returns:
        //     The Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry for the entity.
        //     The entry provides access to change tracking information and operations for the
        //     entity.
        public virtual EntityEntry Update([NotNullAttribute] object entity);
        //
        // Summary:
        //     Begins tracking the given entity in the Microsoft.EntityFrameworkCore.EntityState.Modified
        //     state such that it will be updated in the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //     All properties of the entity will be marked as modified. To mark only some properties
        //     as modified, use Microsoft.EntityFrameworkCore.DbContext.Attach``1(``0) to begin
        //     tracking the entity in the Microsoft.EntityFrameworkCore.EntityState.Unchanged
        //     state and then use the returned Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry
        //     to mark the desired properties as modified.
        //     A recursive search of the navigation properties will be performed to find reachable
        //     entities that are not already being tracked by the context. These entities will
        //     also begin to be tracked by the context. If a reachable entity has its primary
        //     key value set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Modified
        //     state. If the primary key value is not set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state. An entity is considered to have its primary key value set if the primary
        //     key property is set to anything other than the CLR default for the property type.
        //     Use Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry.State to set the
        //     state of only a single entity.
        //
        // Parameters:
        //   entity:
        //     The entity to update.
        //
        // Type parameters:
        //   TEntity:
        //     The type of the entity.
        //
        // Returns:
        //     The Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry`1 for the entity.
        //     The entry provides access to change tracking information and operations for the
        //     entity.
        public virtual EntityEntry<TEntity> Update<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        //
        // Summary:
        //     Begins tracking the given entities in the Microsoft.EntityFrameworkCore.EntityState.Modified
        //     state such that they will be updated in the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //     All properties of each entity will be marked as modified. To mark only some properties
        //     as modified, use Microsoft.EntityFrameworkCore.DbContext.Attach(System.Object)
        //     to begin tracking each entity in the Microsoft.EntityFrameworkCore.EntityState.Unchanged
        //     state and then use the returned Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry
        //     to mark the desired properties as modified.
        //     A recursive search of the navigation properties will be performed to find reachable
        //     entities that are not already being tracked by the context. These entities will
        //     also begin to be tracked by the context. If a reachable entity has its primary
        //     key value set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Modified
        //     state. If the primary key value is not set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state. An entity is considered to have its primary key value set if the primary
        //     key property is set to anything other than the CLR default for the property type.
        //
        // Parameters:
        //   entities:
        //     The entities to update.
        public virtual void UpdateRange([NotNullAttribute] params object[] entities);
        //
        // Summary:
        //     Begins tracking the given entities in the Microsoft.EntityFrameworkCore.EntityState.Modified
        //     state such that they will be updated in the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        //     is called.
        //     All properties of each entity will be marked as modified. To mark only some properties
        //     as modified, use Microsoft.EntityFrameworkCore.DbContext.Attach(System.Object)
        //     to begin tracking each entity in the Microsoft.EntityFrameworkCore.EntityState.Unchanged
        //     state and then use the returned Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry
        //     to mark the desired properties as modified.
        //     A recursive search of the navigation properties will be performed to find reachable
        //     entities that are not already being tracked by the context. These entities will
        //     also begin to be tracked by the context. If a reachable entity has its primary
        //     key value set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Modified
        //     state. If the primary key value is not set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Added
        //     state. An entity is considered to have its primary key value set if the primary
        //     key property is set to anything other than the CLR default for the property type.
        //
        // Parameters:
        //   entities:
        //     The entities to update.
        void UpdateRange([NotNullAttribute] IEnumerable<object> entities);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
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
