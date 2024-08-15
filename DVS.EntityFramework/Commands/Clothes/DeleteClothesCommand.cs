﻿using DVS.Domain.Commands.Clothes;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Clothes
{
    public class DeleteClothesCommand(DVSDbContextFactory contextFactory) : IDeleteClothesCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Guid guidID)
        {
            using DVSDbContext context = _contextFactory.Create();

            ClothesDTO clothesDTO = new()
            {
                GuidID = guidID
            };

            context.Clothes.Remove(clothesDTO);
            await context.SaveChangesAsync();
        }
    }
}
