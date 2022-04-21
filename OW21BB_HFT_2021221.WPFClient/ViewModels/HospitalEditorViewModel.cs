using OW21BB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace OW21BB_HFT_2021221.WPFClient.ViewModels
{
    public class HospitalEditorViewModel
    {
        public Hospital hospital { get; set; }
        public void Setup(Hospital hospital)
        {
            this.hospital = hospital;
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        private SolidColorBrush backGroundColor;

        public SolidColorBrush BackGroundColor
        {
            get { return new SolidColorBrush(Color.FromRgb(153, 217, 234)); }
        }

        public HospitalEditorViewModel()
        {
            if (!IsInDesignMode)
            {

            }
        }
    }
}
