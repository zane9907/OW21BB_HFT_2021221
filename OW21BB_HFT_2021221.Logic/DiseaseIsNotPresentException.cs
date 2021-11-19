using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Logic
{
    public class DiseaseIsNotPresentException : Exception
    {
        public DiseaseIsNotPresentException(string diseaseName) :base($"{diseaseName} is currently not present among the patients!")
        {

        }
    }
}
