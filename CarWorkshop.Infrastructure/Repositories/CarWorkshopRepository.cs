﻿using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Repositories
{
    internal class CarWorkshopRepository : ICarWorkshopRepository
    {
        private readonly CarWorkshopDbContext _dbContext;

        public CarWorkshopRepository(CarWorkshopDbContext dbContext)
        {
            this._dbContext = dbContext;    
        }

        public Task Commit()
            => _dbContext.SaveChangesAsync();

        public async Task Create(Domain.Entities.CarWorkshop carWorkshop)
        {
            _dbContext.Add(carWorkshop);
            await _dbContext.SaveChangesAsync();
        }

		public async Task<IEnumerable<Domain.Entities.CarWorkshop>> GetAll()
            => await _dbContext.CarWorkshops.ToListAsync();

        public async Task<Domain.Entities.CarWorkshop> GetByEncodedName(string encodedName)
            => await _dbContext.CarWorkshops.FirstAsync(c => c.EncodedName == encodedName);

        public Task<Domain.Entities.CarWorkshop?> GetByName(string name)
            => _dbContext.CarWorkshops.FirstOrDefaultAsync(cw => cw.Name.ToLower() == name.ToLower());
	}
}
