#region Imports

using Application.ServiceInterfaces.IUserServices;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService) :
            base(options)
        {
            _currentUserService = currentUserService;
            //this.ChangeTracker.LazyLoadingEnabled = false;
        }

        // Required DBSets
        public virtual DbSet<Customer> Customers { get; set; }

        // Utility DBSets
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserToRole> UserToRoles { get; set; }
        public virtual DbSet<ApiLog> ApiLogs { get; set; }
        public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<SystemSetting> SystemSettings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseInMemoryDatabase(databaseName: "DemoDb");
        //}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseModel>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = getZeroOrCurrentUserID();
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.ModifiedBy = getZeroOrCurrentUserID();
                        entry.Entity.ModifiedDate = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = getZeroOrCurrentUserID();
                        entry.Entity.ModifiedDate = DateTime.Now;
                        break;
                }

            return base.SaveChangesAsync(cancellationToken);
        }

        private int getZeroOrCurrentUserID()
        {
            return _currentUserService?.UserId ?? 0;
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseModel>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = getZeroOrCurrentUserID();
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.ModifiedBy = getZeroOrCurrentUserID();
                        entry.Entity.ModifiedDate = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = getZeroOrCurrentUserID();
                        entry.Entity.ModifiedDate = DateTime.Now;
                        break;
                }

            return base.SaveChanges();
        }

        public DataSet ExecuteSqlStoredProcedure(string sqlQueryToExecuteStoredProcedure, List<SqlParameter> parameters)
        {
            var dbConnection = Database.GetDbConnection();
            var dataset = new DataSet();
            var sqlDataAdapter = new SqlDataAdapter(sqlQueryToExecuteStoredProcedure, dbConnection.ConnectionString);
            sqlDataAdapter.SelectCommand.Parameters.Clear();
            if (parameters?.Count > 0)
            {
                sqlDataAdapter.SelectCommand.Parameters.AddRange(parameters.ToArray());
            }
            sqlDataAdapter.SelectCommand.CommandTimeout = 100;
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDataAdapter.Fill(dataset);
            return dataset;
        }
    }
}