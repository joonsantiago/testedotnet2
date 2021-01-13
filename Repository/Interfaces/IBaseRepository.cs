using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorys.Interfaces
{
    public interface IBaseRepository<T> where T: class
    {
        void Save(T item);
        List<T> GetList();
        T GetById(int id);
    }
}
