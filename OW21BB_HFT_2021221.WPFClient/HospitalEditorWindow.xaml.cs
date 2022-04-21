using OW21BB_HFT_2021221.Models;
using OW21BB_HFT_2021221.WPFClient.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OW21BB_HFT_2021221.WPFClient
{
    /// <summary>
    /// Interaction logic for HospitalEditorWindow.xaml
    /// </summary>
    public partial class HospitalEditorWindow : Window
    {
        

        public HospitalEditorWindow(Hospital hospital)
        {
            InitializeComponent();
            var vm = new HospitalEditorViewModel();
            vm.Setup(hospital);
            this.DataContext = vm;
        }
    }
}
