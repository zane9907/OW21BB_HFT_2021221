using OW21BB_HFT_2021221.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected HospitalDbContext hpctx;

        public Repository(HospitalDbContext hpctx)
        {
            this.hpctx = hpctx;
        }

        public abstract void Create(T t);


        public abstract void Delete(T t);


        public abstract T Get(int id);
        

        public virtual IQueryable<T> GetAll()
        {
            return hpctx.Set<T>();
        }

        public abstract void Update(T t);
        
    }
}
