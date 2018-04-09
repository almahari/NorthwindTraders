using Dapper;
using Microsoft.EntityFrameworkCore;
using NorthwindTraders.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindTraders.Application.Reports.Commands
{
    public class EmployeesWithManagersQuery : IEmployeesWithManagersQuery
    {
        private readonly NorthwindContext _context;

        public EmployeesWithManagersQuery(NorthwindContext context)
        {
            _context = context;
        }

        public IEnumerable<EmployeeManagerModel> Execute()
        {
            var sql = @"
SELECT e.EmployeeId as EmployeeId, e.FirstName as EmployeeFirstName, e.LastName as EmployeeLastName, e.Title as EmployeeTitle,
   m.EmployeeId as ManagerId, m.FirstName as ManagerFirstName, m.LastName as ManagerLastName, m.Title as ManagerTitle
FROM employees AS e
JOIN employees AS m ON e.ReportsTo = m.EmployeeID
WHERE e.ReportsTo is not null";

            return _context.Database.GetDbConnection().Query<EmployeeManagerModel>(sql);
        }
    }
}
