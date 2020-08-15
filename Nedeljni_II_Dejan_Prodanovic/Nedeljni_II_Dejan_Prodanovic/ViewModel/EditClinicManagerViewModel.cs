using Nedeljni_II_Dejan_Prodanovic.Command;
using Nedeljni_II_Dejan_Prodanovic.Model;
using Nedeljni_II_Dejan_Prodanovic.Service;
using Nedeljni_II_Dejan_Prodanovic.Utility;
using Nedeljni_II_Dejan_Prodanovic.Validation;
using Nedeljni_II_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_II_Dejan_Prodanovic.ViewModel
{
    class EditClinicManagerViewModel:ViewModelBase
    {
        EditClinicManager view;
        IUserService userService;
        IClinicAminService adminService;
        IClinicManagerService managerService;
        IClinicService clinicService;
        string oldUserName;
        string oldIdCardNumber;

        public EditClinicManagerViewModel(EditClinicManager eEditClinicManager,
            vwClinicManager managerToEdit)
        {
            view = eEditClinicManager;


            adminService = new ClinicAminService();
            userService = new UserService();
            managerService = new ClinicManagerService();
            clinicService = new ClinicService();

            ClinicManager = managerToEdit;

            oldUserName = managerToEdit.Username;
            oldIdCardNumber = managerToEdit.IDCardNumber;

            FloorList = new List<string>();
            tblClinicInstitution clinic = clinicService.GetClinic();
            for (int i = 0; i < clinic.NumberOdFloors; i++)
            {
                int f = i + 1;
                FloorList.Add(f.ToString());
            }
        }

        public bool adminCreated = false;

        private List<string> floorList;
        public List<string> FloorList
        {
            get
            {
                return floorList;
            }
            set
            {
                floorList = value;
                OnPropertyChanged("FloorList");
            }
        }

        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }
        private vwClinicManager clinicManager;
        public vwClinicManager ClinicManager
        {
            get
            {
                return clinicManager;
            }
            set
            {
                clinicManager = value;
                OnPropertyChanged("ClinicManager");
            }
        }
        private tblClinicManager manager;
        public tblClinicManager Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }


        private string gender = "male";
        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private string floor;
        public string Floor
        {
            get { return floor; }
            set
            {
                floor = value;
                OnPropertyChanged("Floor");
            }
        }

        private DateTime dateOfBirth = DateTime.Now;
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; OnPropertyChanged("DateOfBirth"); }
        }

        private tblClinicMaintenace maintenace;
        public tblClinicMaintenace Maintenace
        {
            get
            {
                return maintenace;
            }
            set
            {
                maintenace = value;
                OnPropertyChanged("Maintenace");
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


                if (!ValidationClass.IsIDCardNumberValid(ClinicManager.IDCardNumber))
                {
                    MessageBox.Show("IDCardNumber is not valid");
                    return;
                }



                tblUser userInDb = userService.GetUserByUserName(ClinicManager.Username);

                if (userInDb != null&& !userInDb.Username.Equals(oldUserName))
                {
                    string str1 = string.Format("User with this username exists\n" +
                        "Enter another username");
                    MessageBox.Show(str1);
                    return;
                }

                userInDb = userService.GetUserByIdCardNumber(ClinicManager.IDCardNumber);

                if (userInDb != null && !userInDb.IDCardNumber.Equals(oldIdCardNumber))
                {
                    string str1 = string.Format("User with this IDCardNumber exists\n" +
                        "Enter another IDCardNumber");
                    MessageBox.Show(str1);
                    return;
                }
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password;

                string encryptedString = EncryptionHelper.Encrypt(password);
                ClinicManager.Gender = Gender;
                ClinicManager.DateOfBirth = DateOfBirth;


                
                
                if (ClinicManager.MaxNumberOfDoctors == null)
                {
                    string str1 = string.Format("Invalid input for MaxNumberOfDoctors " +
                        "\n" +
                        "Enter integer number");
                    MessageBox.Show(str1);
                    return;
                }


                if (ClinicManager.MaxNumberOfDoctors == null)
                {
                    string str1 = string.Format("Invalid input for MinNumberOdRooms " +
                        "\n" +
                        "Enter integer number");
                    MessageBox.Show(str1);
                    return;
                }


                //tblClinicManager newManager = new tblClinicManager();

                ClinicManager.ManagerFloor = Floor;

                managerService.EditManager(ClinicManager);

                string str = string.Format("You edited Clinic Manager");
                MessageBox.Show(str);

                adminCreated = true;
                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute(object parameter)
        {

            if (String.IsNullOrEmpty(ClinicManager.FullName) || String.IsNullOrEmpty(ClinicManager.IDCardNumber)
                || String.IsNullOrEmpty(ClinicManager.Nationality)
                || String.IsNullOrEmpty(ClinicManager.Username) || parameter as PasswordBox == null
                 || String.IsNullOrEmpty((parameter as PasswordBox).Password))
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


        private ICommand chooseGender;
        public ICommand ChooseGender
        {
            get
            {
                if (chooseGender == null)
                {
                    chooseGender = new RelayCommand(ChooseGenderExecute, CanChooseGenderExecute);
                }
                return chooseGender;
            }
        }

        private void ChooseGenderExecute(object parameter)
        {
            Gender = (string)parameter;
        }

        private bool CanChooseGenderExecute(object parameter)
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



        private ICommand chooseHasRightToExpandClinic;
        public ICommand ChooseHasRightToExpandClinic
        {
            get
            {
                if (chooseHasRightToExpandClinic == null)
                {
                    chooseHasRightToExpandClinic = new RelayCommand(ChooseHasRightToExpandClinicExecute,
                        CanChooseHasRightToExpandClinicExecute);
                }
                return chooseGender;
            }
        }

        private void ChooseHasRightToExpandClinicExecute(object parameter)
        {
            Gender = (string)parameter;
        }

        private bool CanChooseHasRightToExpandClinicExecute(object parameter)
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
