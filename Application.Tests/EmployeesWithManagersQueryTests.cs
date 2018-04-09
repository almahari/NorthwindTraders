using NorthwindTraders.Application.Reports.Commands;
using NorthwindTraders.Persistance;
using System.Linq;
using Xunit;

namespace Application.Tests
{
    public class EmployeesWithManagersQueryTests : TestBase
    {
        [Fact]
        public void ShouldReturnReport()
        {
            UseSqlite();
            var context = GetDbContext();
            NorthwindInitializer.Initialize(context);

            var query = new EmployeesWithManagersQuery(context);
            var result = query.Execute();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.All(result, e =>
            {
                Assert.NotEmpty(e.EmployeeId);
                Assert.NotEmpty(e.ManagerId);
            });
        }
    }
}
