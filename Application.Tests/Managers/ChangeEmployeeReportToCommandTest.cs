﻿using NorthwindTraders.Application.Managers.Commands;
using NorthwindTraders.Domain;
using System;
using System.Linq;
using Xunit;

namespace Application.Tests.Managers
{
    public class ChangeEmployeeReportToCommandTest : TestBase
    {
        [Fact]
        public void ShouldMoveEmployeeUnderManager()
        {
            using (var context = InitAndGetDbContext())
            {
                // Prepare
                var command = new ChangeEmployeeReportToCommand(context);

                // Execute
                int reportTo = 2;
                command.Execute(new EmployeeUnderManagerModel
                {
                    EmployeeId = 1,
                    ManagerId = reportTo
                });

                // Asses
                Assert.Single(context.Employees
                    .Where(e => e.EmployeeId == 1 && e.ReportsTo == reportTo));
            }
        }

        [Fact]
        public void ShouldFailForNonExistingManager()
        {
            using (var context = InitAndGetDbContext())
            {
                // Prepare
                var command = new ChangeEmployeeReportToCommand(context);

                // Execute and asses
                int reportTo = 3;
                Assert.Throws<ArgumentException>(() => command.Execute(new EmployeeUnderManagerModel
                {
                    EmployeeId = 1,
                    ManagerId = reportTo
                }));
            }
        }
        
        [Fact]
        public void ShouldNotBeManagerOfItself()
        {
            using (var context = InitAndGetDbContext())
            {
                // Prepare
                var command = new ChangeEmployeeReportToCommand(context);

                // Execute and asses
                int reportTo = 1;
                Assert.Throws<ArgumentException>(() => command.Execute(new EmployeeUnderManagerModel
                {
                    EmployeeId = 1,
                    ManagerId = reportTo
                }));
            }
        }

        private NorthwindTraders.Persistance.NorthwindContext InitAndGetDbContext()
        {
            //UseSqlite();

            var context = GetDbContext();

            context.Employees.Add(new Employee
            {
                EmployeeId = 1,
                FirstName = "",
                LastName = ""
            });
            context.Employees.Add(new Employee
            {
                EmployeeId = 2,
                FirstName = "",
                LastName = ""
            });

            context.SaveChanges();
            return context;
        }
    }
}
