﻿namespace NorthwindTraders.Application.Employees.Commands.ChangeEmployeesManager
{
    public interface IChangeEmployeesManagerCommand
    {
        void Execute(ChangeEmployeeManagerModel model);
    }
}