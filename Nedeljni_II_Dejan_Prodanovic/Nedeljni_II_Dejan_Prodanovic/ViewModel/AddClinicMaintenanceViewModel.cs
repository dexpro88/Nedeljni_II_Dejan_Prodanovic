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
    class AddClinicMaintenanceViewModel:ViewModelBase
    {
        AddClinicMaintenance view;
        IUserService userService;
        IClinicAminService adminService;
        IClinicMaintenace maintenaceService;

        public AddClinicMaintenanceViewModel(AddClinicMaintenance addClinicMaintenance)
        {
            view = addClinicMaintenance;


            adminService = new ClinicAminService();
            userService = new UserService();
            maintenaceService = new ClinicMaintenaceService();

            User = new tblUser();

        }

        public bool adminCreated = false;

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

        private string hasRightToExpandClinic = "yes";
        public string HasRightToExpandClinic
        {
            get { return hasRightToExpandClinic; }
            set
            {
                hasRightToExpandClinic = value;
                OnPropertyChanged("HasRightToExpandClinic");
            }
        }

        private string takesCareOfIvalidAccess = "yes";
        public string TakesCareOfIvalidAccess
        {
            get { return takesCareOfIvalidAccess; }
            set
            {
                takesCareOfIvalidAccess = value;
                OnPropertyChanged("TakesCareOfIvalidAccess");
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


                if (!ValidationClass.IsIDCardNumberValid(User.IDCardNumber))
                {
                    MessageBox.Show("IDCardNumber is not valid");
                    return;
                }



                tblUser userInDb = userService.GetUserByUserName(User.Username);

                if (userInDb != null)
                {
                    string str1 = string.Format("User with this username exists\n" +
                        "Enter another username");
                    MessageBox.Show(str1);
                    return;
                }

                userInDb = userService.GetUserByIdCardNumber(User.IDCardNumber);

                if (userInDb != null)
                {
                    string str1 = string.Format("User with this IDCardNumber exists\n" +
                        "Enter another IDCardNumber");
                    MessageBox.Show(str1);
                    return;
                }
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password;

                string encryptedString = EncryptionHelper.Encrypt(password);
                User.Gender = Gender;
                User.DateOfBirth = DateOfBirth;


                User.Password = encryptedString;
                User = userService.AddUser(User);

                tblClinicMaintenace newMaintenace = new tblClinicMaintenace();

                if (HasRightToExpandClinic.Equals("yes"))
                {
                    newMaintenace.CanChooseClinicExpansionPermission = true;
                }
                else
                {
                    newMaintenace.CanChooseClinicExpansionPermission = false;
                }

                if (TakesCareOfIvalidAccess.Equals("yes"))
                {
                    newMaintenace.CanChooseInvalidAccess = true;
                }
                else
                {
                    newMaintenace.CanChooseInvalidAccess = false;
                }

                newMaintenace.UserID = User.UserID;

                maintenaceService.AddMaintenace(newMaintenace);
                string str = string.Format("You added Clinic Maintenace");
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

            if (String.IsNullOrEmpty(User.FullName) || String.IsNullOrEmpty(User.IDCardNumber)
                || String.IsNullOrEmpty(User.Nationality)
                || String.IsNullOrEmpty(User.Username) || parameter as PasswordBox == null
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
