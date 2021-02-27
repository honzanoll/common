using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using honzanoll.Data.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace honzanoll.Data.NetCore.DALs
{
    public abstract class DatabaseContext : DbContext
    {
        #region Fields

        private readonly IHttpContextAccessor httpContextAccessor;

        #endregion

        #region Constructors

        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DatabaseContext(IHttpContextAccessor httpContextAccessor) : base()
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Public methods

        public override int SaveChanges()
        {
            AddAuditData();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddAuditData();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddAuditData();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AddAuditData();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #endregion

        #region Private methods

        private void AddAuditData()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is IAuditEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(httpContextAccessor?.HttpContext?.User?.Identity?.Name)
            ? httpContextAccessor.HttpContext.User.Identity.Name
            : "SYSTEM";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IAuditEntity)entity.Entity).Created = DateTime.Now;
                    ((IAuditEntity)entity.Entity).CreatedBy = currentUsername;
                }
                else if (entity.State == EntityState.Modified)
                {
                    ((IAuditEntity)entity.Entity).Updated = DateTime.Now;
                    ((IAuditEntity)entity.Entity).UpdatedBy = currentUsername;
                }
            }
        }

        #endregion
    }
}
