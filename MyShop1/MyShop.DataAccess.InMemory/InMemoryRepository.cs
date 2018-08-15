﻿using MyShop.Core.Models;
using MyShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory 
{
    public class InMemoryRepository <T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        string className;
        List<T> items;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache["className"] as List<T>;
            if (items == null) items = new List<T>();
        }
        public void Commit()
        {
            cache["className"] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);

        }

        public void Update(T t)
        {
            T tToUpdate = items.Find(p => p.Id == t.Id);

            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else { throw new Exception(className + "not found"); }
        }

        public T Find(string Id)
        {
            T t = items.Find(p => p.Id == Id);

            if (t != null)
            {
                return t;
            }
            else { throw new Exception(className + "not found"); }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T tToDelete = items.Find(p => p.Id == Id);

            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else { throw new Exception(className + "not found"); }
        }
    }
}
