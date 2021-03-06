﻿using Microsoft.EntityFrameworkCore;
using SqliteApp.Daos;
using SqliteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqliteApp.Database
{
    public class ProductsRepository : IProductsRepository
    {

        private readonly DatabaseContext _databaseContext;

        public ProductsRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }

        public Boolean InsertProductAsync(Product product)
        {
            try
            {
                var tracking = _databaseContext.Products.Add(product);
                _databaseContext.SaveChanges();

                var isAdded = tracking.State == EntityState.Added;
                return isAdded;
            }
            catch (Exception Error)
            {
                Console.WriteLine("Erro: " + Error.ToString());
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _databaseContext.Products.FindAsync(id);
                var tracking = _databaseContext.Remove(product);

                await _databaseContext.SaveChangesAsync();
                var isDeleted = tracking.State == EntityState.Deleted;
                return isDeleted;
            }
            catch (Exception Error)
            {
                Console.WriteLine("Erro: " + Error.ToString());
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            try
            {
                var tracking = _databaseContext.Update(product);
                await _databaseContext.SaveChangesAsync();

                var isModified = tracking.State == EntityState.Modified;
                return isModified;
            }
            catch (Exception Error)
            {
                Console.WriteLine("Erro: " + Error.ToString());
                return false;
            }
        }

        public IList<Product> GetProductsAsync()
        {
            try
            {
                var products =  _databaseContext.Products.ToList();
                return products;
            }
            catch (Exception Error)
            {
                Console.WriteLine("Erro: " + Error.ToString());
                return null;
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _databaseContext.Products.FindAsync(id);
                return product;
            }
            catch (Exception Error)
            {
                Console.WriteLine("Erro: " + Error.ToString());
                return null;
            }
        }

        #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IEnumerable<Product>> QueryProductsAsync(Func<Product, bool> predicate)
        #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            try
            {
                var products = _databaseContext.Products.Where(predicate);
                return products.ToList();
            }
            catch (Exception Error)
            {
                Console.WriteLine("Erro: " + Error.ToString());
                return null;
            }
        }
    }
}
