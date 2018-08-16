using Dapper;
using Microsoft.EntityFrameworkCore;
using NorthwindTraders.Application.Reports.Queries;
using NorthwindTraders.Persistance;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Reports
{
    public class EmployeesWithManagersQueryViewTests : TestBase
    {
        private const string EmployeesWithManagersViewSql = @"
CREATE VIEW viewEmployeesWithManagers(
        EmployeeFirstName, EmployeeLastName, EmployeeTitle,
        ManagerFirstName, ManagetLastName, ManagerTitle)
AS 
SELECT e.FirstName as EmployeeFirstName, e.LastName as EmployeeLastName, e.Title as EmployeeTitle,
        m.FirstName as ManagerFirstName, m.LastName as ManagetLastName, m.Title as ManagerTitle
FROM employees AS e
JOIN employees AS m ON e.ReportsTo = m.EmployeeID
WHERE e.ReportsTo is not null";

        [Fact]
        public async Task ShouldReturnReport()
        {
            UseSqlite();

            using (var context = GetDbContext())
            {
                // Arrange
                // This view only exists on DB, so we need to create it before querying.
                context.Database.GetDbConnection().Execute(EmployeesWithManagersViewSql);

                NorthwindInitializer.Initialize(context);
                var query = new EmployeesWithManagersViewQuery(context);

                // Act
                var result = await query.Execute();

                // Assert
                Assert.NotEmpty(result);
                Assert.Equal(8, result.Count());
                Assert.Contains(result, r => r.ManagerTitle == "Vice President, Sales");
                Assert.DoesNotContain(result, r => r.EmployeeTitle == "Vice President, Sales");
            }
        }
    }
}
