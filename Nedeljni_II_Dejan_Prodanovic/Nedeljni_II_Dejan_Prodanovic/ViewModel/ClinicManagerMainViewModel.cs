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
    class ClinicManagerMainViewModel:ViewModelBase
    {
        ClinicManagerMain view;

        IUserService userService;
        IClinicService clinicService;
        IClinicAminService adminService;

        public ClinicManagerMainViewModel(ClinicManagerMain clinicManagerMain,
            vwClinicManager managerLogedIn)
        {
            view = clinicManagerMain;
            Manager = managerLogedIn;

            clinicService = new ClinicService();
            userService = new UserService();
            adminService = new ClinicAminService();

            

        }
        private vwClinicManager manager;
        public vwClinicManager Manager
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

        private ICommand showClinicDoctors;
        public ICommand ShowClinicDoctors
        {
            get
            {
                if (showClinicDoctors == null)
                {
                    showClinicDoctors = new RelayCommand(param => ShowClinicDoctorsExecute(),
                        param => CanShowClinicDoctorsExecute());
                }
                return showClinicDoctors;
            }
        }

        private void ShowClinicDoctorsExecute()
        {
            try
            {

                ClinicDoctors clinicDoctors = new ClinicDoctors(Manager);
                clinicDoctors.Show();
                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanShowClinicDoctorsExecute()
        {

            return true;
        }

      

        private ICommand logout;
        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => LogoutExecute(), param => CanLogoutExecute());
                }
                return logout;
            }
        }

        private void LogoutExecute()
        {
            try
            {
                LoginView loginView = new LoginView();
                loginView.Show();
                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanLogoutExecute()
        {
            return true;
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
    }
}
