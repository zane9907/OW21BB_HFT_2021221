using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using OW21BB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OW21BB_HFT_2021221.WPFClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Hospital> Hospitals { get; set; }
        public RestCollection<Patient> Patients { get; set; }

        private Patient selectedPatient;

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {

                if (value != null)
                {
                    selectedPatient = selectedPatient.GetCopy(value);

                    OnPropertyChanged();
                    (DeletePatientCommand as RelayCommand).NotifyCanExecuteChanged(); 
                }
            }
        }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ICommand CreatePatientCommand { get; set; }
        public ICommand DeletePatientCommand { get; set; }
        public ICommand UpdatePatientCommand { get; set; }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                //Hospitals = new RestCollection<Hospital>("http://localhost:41147/", "hospital");
                Patients = new RestCollection<Patient>("http://localhost:41147/", "patient","hub");

                CreatePatientCommand = new RelayCommand(() =>
                {
                    Patients.Add(new Patient()
                    {
                        Name = SelectedPatient.Name
                    });
                });

                DeletePatientCommand = new RelayCommand(() =>
                {                    
                    Patients.Delete(SelectedPatient.PatientID);
                },
                () =>
                {
                    return SelectedPatient.Name != null;
                }
                );

                UpdatePatientCommand = new RelayCommand(() =>
                {
                    Patients.Update(SelectedPatient); 
                },
                () =>
                {
                    return SelectedPatient != null;
                });

                selectedPatient = new Patient();
            }
        }
    }
}
