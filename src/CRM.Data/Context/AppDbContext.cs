using Microsoft.EntityFrameworkCore;
using CRM.Data.Common;
using CRM.Data.Entities;
using Microsoft.EntityFrameworkCore.Design;
using CRM.Data.Seeds;

namespace CRM.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=OrganizeCRM;User Id=sa;Password=Password123;TrustServerCertificate=True;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<RequestType> RequestTypes { get; set; }
    public DbSet<RequestApproval> RequestApprovals { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Employee ilişkileri
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Manager)
            .WithMany(e => e.DirectReports)
            .HasForeignKey(e => e.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Position)
            .WithMany(p => p.Employees)
            .HasForeignKey(e => e.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

        // Position - Department ilişkisi
        modelBuilder.Entity<Position>()
            .HasOne(p => p.Department)
            .WithMany(d => d.Positions)
            .HasForeignKey(p => p.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Position - Permission ilişkisi
        modelBuilder.Entity<Position>()
            .HasMany(p => p.Permissions)
            .WithMany(p => p.Positions)
            .UsingEntity(j => j.ToTable("PositionPermissions"));

        // Request ilişkileri
        modelBuilder.Entity<Request>()
            .HasOne(r => r.RequestType)
            .WithMany(rt => rt.Requests)
            .HasForeignKey(r => r.RequestTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Request>()
            .HasOne(r => r.RequestOwner)
            .WithMany()
            .HasForeignKey(r => r.RequestOwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RequestApproval>()
            .HasOne(ra => ra.Request)
            .WithMany(r => r.Approvals)
            .HasForeignKey(ra => ra.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RequestApproval>()
            .HasOne(ra => ra.Approver)
            .WithMany()
            .HasForeignKey(ra => ra.ApproverId)
            .OnDelete(DeleteBehavior.Restrict);

        // RequestTypePosition ilişkileri
        modelBuilder.Entity<RequestTypePosition>()
            .HasKey(rtp => new { rtp.RequestTypeId, rtp.PositionId });

        modelBuilder.Entity<RequestTypePosition>()
            .HasOne(rtp => rtp.RequestType)
            .WithMany(rt => rt.RequestTypePositions)
            .HasForeignKey(rtp => rtp.RequestTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RequestTypePosition>()
            .HasOne(rtp => rtp.Position)
            .WithMany(p => p.RequestTypePositions)
            .HasForeignKey(rtp => rtp.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(BaseEntity.CreatedDate))
                    .HasDefaultValueSql("GETDATE()");

                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(BaseEntity.IsDeleted))
                    .HasDefaultValue(false);
            }
        }

       modelBuilder.SeedData();
    }
}