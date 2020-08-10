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
    class AddClinicAdministratorViewModel:ViewModelBase
    {
        AddClinicAdministrator view;
         
        IUserService userService;
       

        public AddClinicAdministratorViewModel(AddClinicAdministrator addAdminView)
        {
            view = addAdminView;

            AdministratorTypesList = new List<string>() { "Team", "System", "Local" };
            //adminService = new AdminService();
            userService = new UserService();
           

            User = new tblUser();
            Admin = new tblClinicAdmin();
        }

        private string selctedType;
        public string SelctedType
        {
            get
            {
                return selctedType;
            }
            set
            {
                selctedType = value;
                OnPropertyChanged("SelctedType");
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

                

                //tblUser userInDb = userService.GetUserByUserName(User.Username);

                //if (userInDb != null)
                //{
                //    string str1 = string.Format("User with this username exists\n" +
                //        "Enter another username");
                //    MessageBox.Show(str1);
                //    return;
                //}

                //userInDb = userService.GetUserByJMBG(User.JMBG);

                //if (userInDb != null)
                //{
                //    string str1 = string.Format("User with this JMBG exists\n" +
                //        "Enter another JMBG");
                //    MessageBox.Show(str1);
                //    return;
                //}
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password;

                string encryptedString = EncryptionHelper.Encrypt(password);
                User.Gender = Gender;
 
                User.Password = encryptedString;
                User = userService.AddUser(User);

                //DateTime today = DateTime.Now;
                //Admin.ExpiryDate = today.AddDays(7);
                //Admin.AdministratorType = SelctedType;
                //Admin.UserID = User.UserID;

                //adminService.AddAdmin(Admin);
                string str = string.Format("You added new admin of type {0}", SelctedType);
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

            //if (String.IsNullOrEmpty(User.FirstName) || String.IsNullOrEmpty(User.LastName)
            //    || String.IsNullOrEmpty(User.JMBG) || String.IsNullOrEmpty(User.Residence)
            //    || String.IsNullOrEmpty(User.Username) || parameter as PasswordBox == null
            //    || SelctedType == null || String.IsNullOrEmpty((parameter as PasswordBox).Password))
            //{
            //    return false;
            //}
            //else
            //{
                return true;
            //}
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

       
    }
}
