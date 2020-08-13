using Nedeljni_II_Dejan_Prodanovic.Command;
using Nedeljni_II_Dejan_Prodanovic.Model;
using Nedeljni_II_Dejan_Prodanovic.Service;
using Nedeljni_II_Dejan_Prodanovic.Validation;
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
    class AddReportViewModel:ViewModelBase
    {
        AddReport view;

        IUserService userService;
        IClinicAminService adminService;


        public AddReportViewModel(AddReport addReportView, 
            vwClinicMaintenace maintenaceLogedIn)
        {
            view = addReportView;


            adminService = new ClinicAminService();
            userService = new UserService();


            Report = new Report();
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

        private Report report;
        public Report Report
        {
            get
            {
                return report;
            }
            set
            {
                report = value;
                OnPropertyChanged("Report");
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
                int pomVar;
                if (!Int32.TryParse(Report.NumberOfHours,out pomVar))
                {
                    string str2 = "Invalid input for number of hours\nInput should be nuber betwee 4 and 8";
                    MessageBox.Show(str2);
                    return;
                }
                if (pomVar<4||pomVar>8)
                {
                    string str2 = "Invalid input for number of hours\nInput should be nuber betwee 4 and 8";
                    MessageBox.Show(str2);
                    return;
                }
                Report.MaintenanceId = Maintenace.ClinicMaintenaceID.ToString();
                DateTime today = DateTime.Today;
                string todayDate = string.Format("{0}/{1}/{2}", today.Day, today.Month, today.Year);

                Report.Date = todayDate;

                List<Report> allReports = ReadReportsFromFile();
                List<Report> reportsOfThisUser = GetReportsOfMaintenance(allReports);

                if (ReportForThisDateExists(reportsOfThisUser))
                {
                    MessageBox.Show("You already created report for today");
                    return;
                }

                AddReport(Report);

                MessageBox.Show("You added report");

                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute(object parameter)
        {

            if (String.IsNullOrEmpty(Report.Description)
                || String.IsNullOrEmpty(Report.NumberOfHours))
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
        private void AddReport(Report reportForFile)
        {
            using (StreamWriter sw = File.AppendText(@"..\..\WorkHistory.txt"))
            {
                string str1 = string.Format("{0},{1},{2},{3}",reportForFile.MaintenanceId,
                    reportForFile.NumberOfHours,reportForFile.Description,report.Date);
                sw.WriteLine(str1);
            }
        }

        private List<Report> GetReportsOfMaintenance(List<Report>reports)
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

        private bool ReportForThisDateExists(List<Report> reports)
        {
            DateTime today = DateTime.Today;
            string todayDate = string.Format("{0}/{1}/{2}", today.Day, today.Month, today.Year);
            foreach (var item in reports)
            {
                if (item.Date.Equals(todayDate))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
