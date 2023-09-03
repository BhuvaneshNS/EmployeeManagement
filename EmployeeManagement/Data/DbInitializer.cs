using EmployeeManagement.Models;

namespace EmployeeManagement.Data
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            EmployeeManagementDbContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<EmployeeManagementDbContext>();

            if (!context.Departments.Any())
            {
                context.Departments.AddRange(
                    new Department { Name = "HR" },
                    new Department { Name = "IT" },
                    new Department { Name = "Finance" }
                    );
            }
            context.SaveChanges();
        }
    }
}
