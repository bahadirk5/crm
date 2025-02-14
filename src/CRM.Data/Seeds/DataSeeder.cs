using CRM.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.Data.Seeds;

public static class DataSeeder
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        var seedDate = new DateTime(2025, 1, 1);

        // Departments
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "IT", CreatedDate = seedDate },
            new Department { Id = 2, Name = "HR", CreatedDate = seedDate },
            new Department { Id = 3, Name = "Finance", CreatedDate = seedDate }
        );

        // Positions
        modelBuilder.Entity<Position>().HasData(
            new Position 
            { 
                Id = 1, 
                Title = "CEO", 
                Level = 1, 
                DepartmentId = 1,
                CreatedDate = seedDate 
            },
            new Position 
            { 
                Id = 2, 
                Title = "IT Director", 
                Level = 2, 
                DepartmentId = 1,
                CreatedDate = seedDate 
            },
            new Position 
            { 
                Id = 3, 
                Title = "HR Manager", 
                Level = 3, 
                DepartmentId = 2,
                CreatedDate = seedDate 
            },
            new Position 
            { 
                Id = 4, 
                Title = "Developer", 
                Level = 4, 
                DepartmentId = 1,
                CreatedDate = seedDate 
            }
        );
    }
}