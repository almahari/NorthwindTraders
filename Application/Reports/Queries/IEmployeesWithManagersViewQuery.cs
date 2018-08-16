using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.Application.Reports.Queries
{
    public interface IEmployeesWithManagersViewQuery
    {
        Task<IEnumerable<EmployeeManagerModel>> Execute();
    }
}
