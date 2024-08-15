﻿using DVS.Domain.Commands.Employee;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Employee
{
    public class CreateEmployeeCommand(DVSDbContextFactory contextFactory) : ICreateEmployeeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Domain.Models.Employee employee)
        {
            using DVSDbContext context = _contextFactory.Create();

            EmployeeDTO employeeDTO = new()
            {
                GuidID = employee.GuidID,
                ID = employee.ID,
                Lastname = employee.Lastname,
                Firstname = employee.Firstname,
                Comment = employee.Comment,
                Clothes = employee.Clothes
            };

            context.Employees.Add(employeeDTO);
            await context.SaveChangesAsync();
        }
    }
}
