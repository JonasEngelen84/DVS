﻿using DVS.Domain.Commands.EmployeeClothesSize;
using DVS.Domain.Models;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.EmployeeClothesSize
{
    public class CreateEmployeeClothesSizeCommand(DVSDbContextFactory contextFactory) : ICreateEmployeeClothesSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Domain.Models.EmployeeClothesSize employeeClothesSize)
        {
            using DVSDbContext context = _contextFactory.Create();

            EmployeeClothesSizeDTO employeeClothesSizeDTO = new()
            {
                GuidID = employeeClothesSize.GuidID,
                EmployeeGuidID = employeeClothesSize.EmployeeGuidID,
                ClothesSizeGuidID = employeeClothesSize.ClothesSizeGuidID,
                Quantity = employeeClothesSize.Quantity,
                Comment = employeeClothesSize.Comment
            };

            context.EmployeeClothesSizes.Add(employeeClothesSizeDTO);
            await context.SaveChangesAsync();
        }
    }
}
