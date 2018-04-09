using NorthwindTraders.Application.Employees.Commands;
using NorthwindTraders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Employees
{
    public class ChangeEmployeesManagerCommandTests : TestBase
    {
        [Fact]
        public async Task ShouldPass()
        {
            // Prepare
            var context = GetAndInitDbContext();
            var command = new ChangeEmployeesManagerCommand(context);

            // Execute
            await command.Execute(new ChangeEmployeeManagerModel
            {
                EmployeeID = 1,
                ManagerID = 2
            });

            // Assert
            Assert.Contains(context.Employees.ToList(), e => e.EmployeeId == 1
                && e.ReportsTo == 2);
        }

        [Fact]
        public async Task ShouldFailForNonExistingManager()
        {
            // Prepare
            var context = GetAndInitDbContext();
            var command = new ChangeEmployeesManagerCommand(context);

            // Execute
            await Assert.ThrowsAsync<ArgumentException>(async () => await command.Execute(new ChangeEmployeeManagerModel
            {
                EmployeeID = 6,
                ManagerID = 2
            }));
        }

        [Fact]
        public async Task ShouldNotHaveManagerOfHimself()
        {
            // Prepare
            var context = GetAndInitDbContext();
            var command = new ChangeEmployeesManagerCommand(context);

            // Execute
            await Assert.ThrowsAsync<ArgumentException>(async () => await command.Execute(new ChangeEmployeeManagerModel
            {
                EmployeeID = 1,
                ManagerID = 1
            }));
        }

        private NorthwindTraders.Persistance.NorthwindContext GetAndInitDbContext()
        {
            //UseSqlite();

            var context = GetDbContext();
            var employee = new Employee
            {
                EmployeeId = 1,
                FirstName = "",
                LastName = ""
            };
            var manager = new Employee
            {
                EmployeeId = 2,
                FirstName = "",
                LastName = ""
            };

            context.Employees.Add(employee);
            context.Employees.Add(manager);
            context.SaveChanges();
            return context;
        }
    }
}
