using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Logic
{
    public class ListIsEmptyException : Exception
    {
        public ListIsEmptyException(string what) : base($"The list \"{what}\" is empty!")
        {

        }
    }
}
