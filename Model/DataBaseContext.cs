using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{

    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<Question> Question { get; set; }
        public DbSet<Article> Article { get; set; }


        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            foreach (var ent in this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                // For each changed record, get the audit record entries and add them
                //IAuditableEntity entity = ent.Entity as IAuditableEntity;
                //string OriginalValue = ent.OriginalValues.GetValue<object>("Id") == null ? null : ent.OriginalValues.GetValue<object>("Id").ToString();
                string nm = ent.Entity.ToString();
            }
            return base.SaveChanges();
        }

    }
}
