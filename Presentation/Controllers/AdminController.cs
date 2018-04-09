using Microsoft.AspNetCore.Mvc;
using NorthwindTraders.Application.Employees.Commands;
using NorthwindTraders.Application.Reports.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthwindTraders.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AdminController
    {
        [HttpPost]
        public async Task ChangeEmployeeManager(
            [FromServices] IChangeEmployeesManagerCommand command,
            [FromBody] ChangeEmployeeManagerModel model)
        {
            await command.Execute(model);
        }

        [HttpGet]
        public IEnumerable<EmployeeManagerModel> EmployeeManagerReport(
            [FromServices] IEmployeesWithManagersQuery query
            )
        {
            return query.Execute();
        }
    }
}
