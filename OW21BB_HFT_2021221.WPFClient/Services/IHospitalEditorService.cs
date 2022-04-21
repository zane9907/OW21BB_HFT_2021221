using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.WPFClient.Services
{
    public interface IEditorService<T> where T : class
    {
        void Edit(T t);
    }
}
