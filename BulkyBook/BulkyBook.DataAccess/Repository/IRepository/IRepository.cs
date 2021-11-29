using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    /// <summary>
    /// T is a class. Such as Category
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string includeProperties = null);

        IEnumerable<T> GetAll(string includeProperties = null);

        void Add(T entity);

        //Update isn't common for all -> you need write seperate

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);
    }
}