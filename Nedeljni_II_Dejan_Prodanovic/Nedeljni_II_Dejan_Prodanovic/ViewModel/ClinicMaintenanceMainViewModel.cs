﻿using Nedeljni_II_Dejan_Prodanovic.Command;
using Nedeljni_II_Dejan_Prodanovic.Model;
using Nedeljni_II_Dejan_Prodanovic.Service;
using Nedeljni_II_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Dejan_Prodanovic.ViewModel
{
    class ClinicMaintenanceMainViewModel:ViewModelBase
    {
        ClinicMaintenanceMain view;
        IUserService userService;
        IClinicAminService adminService;
        IClinicMaintenaceService maintenaceService;
        IClinicManagerService managerService;
        IDoctorService doctorService;

        public ClinicMaintenanceMainViewModel(ClinicMaintenanceMain addClinicDoctor, 
            vwClinicMaintenace maintenaceLogedIn)
        {
            view = addClinicDoctor;


            adminService = new ClinicAminService();
            userService = new UserService();
            maintenaceService = new ClinicMaintenaceService();
            managerService = new ClinicManagerService();
            doctorService = new DoctorService();
            //User = new tblUser();
            //Doctor = new tblClinicDoctor();

            //ManagerList = managerService.GetvwManagers();
            //ManagerListToPresent = new List<string>();
            //CreateManagerDictionary();
            Maintenace = maintenaceLogedIn;

        }

        private vwClinicMaintenace maintenace;
        public vwClinicMaintenace Maintenace
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





        private ICommand addReport;
        public ICommand AddReport
        {
            get
            {
                if (addReport == null)
                {
                    addReport = new RelayCommand(param => AddReportExecute(),
                        param => CanAddReportExecute());
                }
                return addReport;
            }
        }

        private void AddReportExecute()
        {
            try
            {
                AddReport addReport = new AddReport(Maintenace);
                addReport.ShowDialog();


                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddReportExecute()
        {

            return true;
        }

        private ICommand showReports;
        public ICommand ShowReports
        {
            get
            {
                if (showReports == null)
                {
                    showReports = new RelayCommand(param => ShowReportsExecute(),
                        param => CanShowReportsExecute());
                }
                return showReports;
            }
        }

        private void ShowReportsExecute()
        {
            try
            {
                Reports reports = new Reports(Maintenace);
                reports.Show();
                view.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanShowReportsExecute()
        {

            return true;
        }

        private List<Report> ReadReportsFromFile()
        {
            try
            {
                List<Report> reportsFromFile = new List<Report>();
                using (StreamReader sr = new StreamReader(@"..\..\WorkHistory.txt"))
                {
                    string line;


                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] lineStr = line.Split(',');

                        Report report = new Report();
                        report.MaintenanceId = lineStr[0];
                        report.NumberOfHours = lineStr[1];
                        report.Description = lineStr[2];
                        report.Date = lineStr[3];

                        reportsFromFile.Add(report);
                    }
                }
                return reportsFromFile;
            }
            catch (Exception e)
            {

                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private List<Report> GetReportsOfMaintenance(List<Report> reports)
        {
            List<Report> returnList = new List<Report>();
            foreach (var report in reports)
            {
                if (report.MaintenanceId.Equals(Maintenace.ClinicMaintenaceID.ToString()))
                {
                    returnList.Add(report);
                }
            }
            return returnList;
        }
    }
}
