﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models.Repositories
{
    internal interface IRepository<T>
    {
        T FindById(int id);
        void Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        HashSet<T> GetAll();
        HashSet<T> FindAll(string filter);
        HashSet<T> GetAllPaging(int index = 1, int pageSize = 10);
        HashSet<T> FindAllPaging(string filter, int index =1, int pageSize = 10);
    }
}
