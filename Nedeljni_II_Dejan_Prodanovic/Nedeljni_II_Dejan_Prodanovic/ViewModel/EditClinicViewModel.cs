using Nedeljni_II_Dejan_Prodanovic.Command;
using Nedeljni_II_Dejan_Prodanovic.Model;
using Nedeljni_II_Dejan_Prodanovic.Service;
using Nedeljni_II_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Dejan_Prodanovic.ViewModel
{
    class EditClinicViewModel:ViewModelBase
    {
        EditClinic view;

        IUserService userService;
        IClinicService clinicService;
        IClinicAminService adminService;
        int oldAmbulanceNumber;
        int oldInvalidsNumber;

        public EditClinicViewModel(EditClinic editClinic)
        {
            view = editClinic;
            

            clinicService = new ClinicService();
            userService = new UserService();
            adminService = new ClinicAminService();

            Clinic = clinicService.GetClinic();
            oldAmbulanceNumber = (int) Clinic.NumberOfAccessPointsForAmbulance;
            oldInvalidsNumber = (int) Clinic.NumberOfAccessPointsForInvalids;
        }

        private tblClinicAdmin admin;
        public tblClinicAdmin Admin
        {
            get
            {
                return admin;
            }
            set
            {
                admin = value;
                OnPropertyChanged("Admin");
            }
        }

        private tblClinicInstitution clinic;
        public tblClinicInstitution Clinic
        {
            get
            {
                return clinic;
            }
            set
            {
                clinic = value;
                OnPropertyChanged("Clinic");
            }
        }

        



        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(SaveExecute, CanSaveExecute);
                }
                return save;
            }
        }

        private void SaveExecute(object parameter)
        {
            try
            {
               

                if (Clinic.NumberOfAccessPointsForAmbulance == null)
                {
                    string str1 = string.Format("Invalid input for number of access points " +
                        "for ambulance\n" +
                        "Enter integer number");
                    MessageBox.Show(str1);
                    return;
                }

                if (Clinic.NumberOfAccessPointsForAmbulance < oldAmbulanceNumber)
                {
                    string str1 = string.Format("Invalid input for number of access points " +
                        "for ambulance\n" +
                        "You can only input number bigger than it is already");
                    MessageBox.Show(str1);
                    return;
                }

                if (Clinic.NumberOfAccessPointsForInvalids == null)
                {
                    string str1 = string.Format("Invalid input for number of access points " +
                        "for invalids\n" +
                        "Enter integer number");
                    MessageBox.Show(str1);
                    return;
                }
                if (Clinic.NumberOfAccessPointsForInvalids < oldInvalidsNumber)
                {
                    string str1 = string.Format("Invalid input for number of access points " +
                        "for invalids\n" +
                        "You can only input number bigger than it is already");
                    MessageBox.Show(str1);
                    return;
                }




                clinicService.EditClinic(Clinic);
                string str = string.Format("You edited clinic {0}", Clinic.InstitutionName);
                MessageBox.Show(str);

                
                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute(object parameter)
        {

            if (String.IsNullOrEmpty(Clinic.InstitutionOwner))               
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                view.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCloseExecute()
        {
            return true;
        }

        private bool CanChooseHasTerraceExecute(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
