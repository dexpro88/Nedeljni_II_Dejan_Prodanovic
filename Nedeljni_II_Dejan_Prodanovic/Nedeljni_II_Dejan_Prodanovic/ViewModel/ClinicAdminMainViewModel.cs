﻿using Nedeljni_II_Dejan_Prodanovic.Command;
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
    class ClinicAdminMainViewModel:ViewModelBase
    {
        ClinicAdminMain view;

        IUserService userService;
        IClinicService clinicService;
        IClinicAminService adminService;

        public ClinicAdminMainViewModel(ClinicAdminMain adminView,
            tblClinicAdmin adminLogedIn)
        {
            view = adminView;
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

        

      

        private DateTime creationDate = DateTime.Now;
        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; OnPropertyChanged("CreationDate"); }
        }



        private ICommand editClinic;
        public ICommand EditClinic
        {
            get
            {
                if (editClinic == null)
                {
                    editClinic = new RelayCommand(param => EditClinicExecute(),
                        param => CanEditClinicExecute());
                }
                return editClinic;
            }
        }

        private void EditClinicExecute()
        {
            try
            {
                
                EditClinic editClinic = new EditClinic();
                editClinic.ShowDialog();

               
                //PredifinedAccount newView = new PredifinedAccount();
                //newView.Show();
                //view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanEditClinicExecute()
        {

            return true;
        }


        private ICommand showClinicMaintenances;
        public ICommand ShowClinicMaintenances
        {
            get
            {
                if (showClinicMaintenances == null)
                {
                    showClinicMaintenances = new RelayCommand(param => ShowClinicMaintenancesExecute(),
                        param => CanShowClinicMaintenancesExecute());
                }
                return showClinicMaintenances;
            }
        }

        private void ShowClinicMaintenancesExecute()
        {
            try
            {
                   
                ClinicMaintenaces clinicMaintenaces = new ClinicMaintenaces(Admin);
                clinicMaintenaces.Show();
                view.Close();

                //PredifinedAccount newView = new PredifinedAccount();
                //newView.Show();
                //view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanShowClinicMaintenancesExecute()
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