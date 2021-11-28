using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Linq;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Update(Product product)
        {
            var productFromDb = dbContext.Products.FirstOrDefault(p => p.ID == product.ID);
            if (productFromDb != null)
            {
                productFromDb.Title = product.Title;
                productFromDb.ISBN = product.ISBN;
                productFromDb.Price = product.Price;
                productFromDb.Price50Book = product.Price50Book;
                productFromDb.Price100Book = product.Price100Book;
                productFromDb.ListPrice = product.ListPrice;
                productFromDb.Description = product.Description;
                productFromDb.CategoryID = product.CategoryID;
                productFromDb.Author = product.Author;
                productFromDb.CoverTypeID = product.CoverTypeID;
                if (product.ImageURL!=null)
                {
                    productFromDb.ImageURL = product.ImageURL;
                }
            } 
        }
    }
}