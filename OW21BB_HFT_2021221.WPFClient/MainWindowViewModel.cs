using OW21BB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.WPFClient
{
    public class MainWindowViewModel
    {
        public RestCollection<Hospital> Hospitals { get; set; }

        public MainWindowViewModel()
        {
            Hospitals = new RestCollection<Hospital>("http://localhost:41147", "hospital");
        }
    }
}
