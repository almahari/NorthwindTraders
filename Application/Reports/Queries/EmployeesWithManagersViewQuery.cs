using Dapper;
using Microsoft.EntityFrameworkCore;
using NorthwindTraders.Persistance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthwindTraders.Application.Reports.Queries
{
    public class EmployeesWithManagersViewQuery : IEmployeesWithManagersViewQuery
    {
        private readonly NorthwindContext _context;

        public EmployeesWithManagersViewQuery(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeManagerModel>> Execute()
        {
            // Querying an view in an existing DB.
            var sql = "select * from viewEmployeesWithManagers";

            // New EF Core 2.1 feature. Allows us to directly map view to a model.
            return await _context.Database.GetDbConnection()
                .QueryAsync<EmployeeManagerModel>(sql);
        }
    }
}
