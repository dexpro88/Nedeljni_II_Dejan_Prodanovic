using Nedeljni_II_Dejan_Prodanovic.Command;
using Nedeljni_II_Dejan_Prodanovic.Model;
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
    class ReportsViewModel:ViewModelBase
    {
        Reports view;
         

        public ReportsViewModel(Reports reportsView,
            vwClinicMaintenace maintenaceLogedIn)
        {
            view = reportsView;


           

            
            Maintenace = maintenaceLogedIn;
            List<Report> allReports = ReadReportsFromFile();
             Reports = GetReportsOfMaintenance(allReports);

            //foreach (var item in Reports)
            //{
            //    MessageBox.Show(item.NumberOfHours);
            //}
         

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

        private List<Report> reports;
        public List<Report> Reports
        {
            get
            {
                return reports;
            }
            set
            {
                reports = value;
                OnPropertyChanged("Reports");
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

        private ICommand back;
        public ICommand Back
        {
            get
            {
                if (back == null)
                {
                    back = new RelayCommand(param => BackExecute(), param => CanBackExecute());
                }
                return back;
            }
        }

        private void BackExecute()
        {
            try
            {
                ClinicMaintenanceMain adminView =
                    new ClinicMaintenanceMain(Maintenace);
                adminView.Show();
                view.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanBackExecute()
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
