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
    class ClinicAdminFirstLoginViewModel:ViewModelBase
    {
        ClinicAdminFirstLogin view;

        IUserService userService;
        IClinicService clinicService;
        IClinicAminService adminService;

        public ClinicAdminFirstLoginViewModel(ClinicAdminFirstLogin addAdminView,
            tblClinicAdmin adminLogedIn)
        {
            view = addAdminView;
            Admin = adminLogedIn;

            clinicService = new ClinicService();
            userService = new UserService();
            adminService = new ClinicAminService();

            Clinic = new tblClinicInstitution();

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

        private string hasTerrace = "yes";
        public string HasTerrace
        {
            get { return hasTerrace; }
            set
            {
                hasTerrace = value;
                OnPropertyChanged("HasTerrace");
            }
        }

        private string hasYard = "yes";
        public string HasYard
        {
            get { return hasYard; }
            set
            {
                hasYard = value;
                OnPropertyChanged("HasYard");
            }
        }

        private DateTime creationDate = DateTime.Now;
        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; OnPropertyChanged("CreationDate"); }
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
                if (Clinic.NumberOdFloors==null)
                {
                    string str1 = string.Format("Invalid input for number of floors\n" +
                        "Enter integer number");
                    MessageBox.Show(str1);
                    return;
                }

                if (Clinic.NumberOfRoomsPerFloor == null)
                {
                    string str1 = string.Format("Invalid input for number of rooms per floor\n" +
                        "Enter integer number");
                    MessageBox.Show(str1);
                    return;
                }

                if (Clinic.NumberOfAccessPointsForAmbulance == null)
                {
                    string str1 = string.Format("Invalid input for number of access points " +
                        "for ambulance\n" +
                        "Enter integer number");
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

                if (HasYard.Equals("yes"))
                {
                    Clinic.HasYard = true;
                }
                else
                {
                    Clinic.HasYard = false;
                }

                if (HasTerrace.Equals("yes"))
                {
                    Clinic.HasTerrace = true;
                }
                else
                {
                    Clinic.HasTerrace = false;
                }

                Clinic.CreationDate = CreationDate;


                clinicService.AddClinic(Clinic);
                string str = string.Format("You created clinic {0}",Clinic.InstitutionName);
                MessageBox.Show(str);

                adminService.SetHasCreatedClinic(Admin);
                ClinicAdminMain adminMain = new ClinicAdminMain(admin);
                adminMain.Show();
                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute(object parameter)
        {

            if (String.IsNullOrEmpty(Clinic.InstitutionName)  
               
                || String.IsNullOrEmpty(Clinic.InstitutionOwner)
                || String.IsNullOrEmpty(Clinic.InstitutionAddress))
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


        private ICommand chooseHasYard;
        public ICommand ChooseHasYard
        {
            get
            {
                if (chooseHasYard == null)
                {
                    chooseHasYard = new RelayCommand(ChooseHasYardExecute,
                        CanChooseHasYardExecute);
                }
                return chooseHasYard;
            }
        }

        private void ChooseHasYardExecute(object parameter)
        {
            HasYard = (string)parameter;
        }

        

        private bool CanChooseHasYardExecute(object parameter)
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

        private ICommand chooseHasTerrace;
        public ICommand ChooseHasTerrace
        {
            get
            {
                if (chooseHasTerrace == null)
                {
                    chooseHasTerrace = new RelayCommand(ChooseHasTerraceExecute,
                        CanChooseHasTerraceExecute);
                }
                return chooseHasTerrace;
            }
        }

        private void ChooseHasTerraceExecute(object parameter)
        {
            HasTerrace = (string)parameter;
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
