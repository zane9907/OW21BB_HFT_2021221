using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Repository
{
    public interface IRepository<T> where T : class
    {
        void Create(T t);
        T Read(int id);
        IQueryable<T> ReadAll();
        void Update(T t);
        void Delete(T t);
    }
}
