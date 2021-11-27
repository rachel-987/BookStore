﻿using BulkyBook.DataAccess.Repository.IRepository;
using System;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            CategoryRepository = new CategoryRepository(dbContext);
        }
        public ICategoryRepository CategoryRepository
        {
            get;
            private set;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}