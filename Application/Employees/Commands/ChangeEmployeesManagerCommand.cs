using NorthwindTraders.Persistance;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindTraders.Application.Employees.Commands
{
    public class ChangeEmployeesManagerCommand : IChangeEmployeesManagerCommand
    {
        private readonly NorthwindContext _context;

        public ChangeEmployeesManagerCommand(NorthwindContext context)
        {
            _context = context;
        }

        public async Task Execute(ChangeEmployeeManagerModel model)
        {
            if (model.EmployeeID == model.ManagerID)
            {
                throw new ArgumentException("Cannot manage himself", nameof(model));
            }

            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == model.EmployeeID);
            var managerExists = _context.Employees.Any(e => e.EmployeeId == model.ManagerID);
            if (employee == null || !managerExists)
            {
                throw new ArgumentException("Employee and manager needs to exists!", nameof(model));
            }

            employee.ReportsTo = model.ManagerID;
            _context.SaveChanges();
        }
    }

    public class ChangeEmployeeManagerModel
    {
        public int EmployeeID { get; set; }

        public int ManagerID { get; set; }
    }

    public interface IChangeEmployeesManagerCommand
    {
        Task Execute(ChangeEmployeeManagerModel model);
    }
}
