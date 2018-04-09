using System.Collections.Generic;

namespace NorthwindTraders.Application.Reports.Commands
{
    public interface IEmployeesWithManagersQuery
    {
        IEnumerable<EmployeeManagerModel> Execute();
    }
}