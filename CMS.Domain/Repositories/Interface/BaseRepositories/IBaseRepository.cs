﻿using CMS.Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CMS.Domain.Repositories.Interface.BaseRepositories
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        // Asenkron prog. için ilk yapılması gereken adım methodlarımızı "Task" olarak işaretlemektir.

        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression);
        Task<T> GetDefault(Expression<Func<T, bool>> expression);
        Task<bool> Any(Expression<Func<T, bool>> expression);

        Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector,
                                                         Expression<Func<T, bool>> expression,
                                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> selector,
                                                     Expression<Func<T, bool>> expression,
                                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                     Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    }
}
