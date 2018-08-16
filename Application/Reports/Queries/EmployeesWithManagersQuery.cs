using Dapper;
using Microsoft.EntityFrameworkCore;
using NorthwindTraders.Persistance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthwindTraders.Application.Reports.Queries
{
    public class EmployeesWithManagersQuery : IEmployeesWithManagersQuery
    {
        private readonly NorthwindContext _context;

        public EmployeesWithManagersQuery(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeManagerModel>> Execute()
        {
            // SQL provided by the client.
            var sql = @"
SELECT e.EmployeeId as EmployeeId, e.FirstName as EmployeeFirstName, e.LastName as EmployeeLastName, e.Title as EmployeeTitle,
	   m.EmployeeId as ManagerId, m.FirstName as ManagerFirstName, m.LastName as ManagetLastName, m.Title as ManagerTitle
FROM employees AS e
JOIN employees AS m ON e.ReportsTo = m.EmployeeID
WHERE e.ReportsTo is not null";

            // New EF Core 2.1 feature. Allows us to directly map view to a model.
            return await _context.Database.GetDbConnection()
                .QueryAsync<EmployeeManagerModel>(sql);
        }
    }
}
