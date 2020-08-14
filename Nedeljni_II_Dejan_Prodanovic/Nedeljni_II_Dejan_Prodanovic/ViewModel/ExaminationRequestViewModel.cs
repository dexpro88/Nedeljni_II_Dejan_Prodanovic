using Nedeljni_II_Dejan_Prodanovic.Command;
using Nedeljni_II_Dejan_Prodanovic.Model;
using Nedeljni_II_Dejan_Prodanovic.Service;
using Nedeljni_II_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Dejan_Prodanovic.ViewModel
{
    class ExaminationRequestViewModel:ViewModelBase
    {
        private string _buttonLabel;
        private int currentProgress;
        private BackgroundWorker worker = new BackgroundWorker();
        IDoctorService doctorService;
        IClinicManagerService managerService;
        ExaminationReport main;

        public ExaminationRequestViewModel(ExaminationReport reportWindow,
            vwClinicPatient patientLogedIn)
        {
            main = reportWindow;
            worker.DoWork += DoWork;
            worker.ProgressChanged += ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerCompleted += RunWorkerCompleted;
            CurrentProgress = 0;
            doctorService = new DoctorService();
            Patient = patientLogedIn;
            PrintExecute();
            doctors = doctorService.GetDoctors();

            managerService = new ClinicManagerService();
            managers = managerService.GetManagers();

        }

        List<tblClinicDoctor> doctors;
        List<tblClinicManager> managers;
        #region Properties

        private vwClinicPatient patient;
        public vwClinicPatient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }

        /// <summary>
        /// number of coppies that we want to print to a file
        /// it si binded to a textbox
        /// </summary>
        private string numberOfCopies;
        public string NumberOfCopies
        {
            get
            {
                return numberOfCopies;
            }
            set
            {
                numberOfCopies = value;
                OnPropertyChanged("NumberOfCopies");
            }
        }
        /// <summary>
        /// percentage of files in which text has already been written
        /// it is binded to a label lblPercentage
        /// </summary>
        private string percentage;
        public string Percentage
        {
            get
            {
                return percentage;
            }
            set
            {
                percentage = value;
                OnPropertyChanged("Percentage");
            }
        }

        /// <summary>
        /// current progress of writing to files
        /// </summary>
        public int CurrentProgress
        {
            get { return currentProgress; }
            private set
            {
                if (currentProgress != value)
                {
                    currentProgress = value;
                    OnPropertyChanged("CurrentProgress");
                }
            }
        }

        public string ButtonLabel
        {
            get { return _buttonLabel; }
            private set
            {
                if (_buttonLabel != value)
                {
                    _buttonLabel = value;
                    OnPropertyChanged("ButtonLabel");
                }
            }
        }

        #endregion  

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentProgress = e.ProgressPercentage;
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            int elapsedTime = 0;

            CurrentProgress = 0;

          

            Random rnd = new Random();
            int numberOfMs = rnd.Next(2000,5001);
            int helpVar = numberOfMs;

            while (numberOfMs > 0)
            {
               
                worker.ReportProgress(CurrentProgress);

                numberOfMs -= 1000;

                elapsedTime += 1000;



                Thread.Sleep(1000);

                 

                //we count current percentage of files in which text is alredy written
                double curProgDouble = (elapsedTime / (double)helpVar) * 100;

                if (curProgDouble>100)
                {
                    curProgDouble = 100;
                }
                CurrentProgress = (int)curProgDouble;

                
                
                Percentage = ((int)curProgDouble).ToString() + "%";

              

               
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

            }
            int rndNum = rnd.Next(0, 101);
            bool hasSimptoms;

            if (rndNum < 50)
            {
                hasSimptoms = true;

            }
            else
            {
                hasSimptoms = false;
            }

            if (hasSimptoms)
            {
                string str1 = string.Format("You have simptoms\n" +
                    "You will have systematic  examination");
                MessageBox.Show(str1);
            }
            else
            {
                MessageBox.Show("You don't have simptoms");
                return;
            }

            bool doctorExists = false;
            foreach (var doctor in doctors)
            {
                if (doctor.RecievesPatients!=null)
                {
                    if ((bool)doctor.RecievesPatients)
                    {
                        doctorExists = true;
                    }
                }
                
            }

            if (!doctorExists)
            {
                MessageBox.Show("There is no doctor who recieves patients");
                foreach (var man in managers)
                {
                    managerService.PunishManager(man.ManagerID);
                }
               
                return;
            }
            CurrentProgress = 0;
            int elapsedTime1 = 0;
            int numberOfMs1 = rnd.Next(2000, 7001);
            int helpVar1 = numberOfMs1;

            while (numberOfMs1 > 0)
            {

                worker.ReportProgress(CurrentProgress);

                numberOfMs1 -= 1000;

                elapsedTime1 += 1000;

                Thread.Sleep(1000);

                //we count current percentage of files in which text is alredy written
                double curProgDouble = (elapsedTime1 / (double)helpVar1) * 100;

                if (curProgDouble > 100)
                {
                    curProgDouble = 100;
                }
                CurrentProgress = (int)curProgDouble;



                Percentage = ((int)curProgDouble).ToString() + "%";


            }

            SaveResultsToFile();
            MessageBox.Show("Your results are saved");

        }

        /// <summary>
        /// method that is executed when BackgroundWorker is completed or canceled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            { }
            else if (e.Error != null)
            {
                Console.WriteLine("Printer exception " + e.Error.ToString());
            }

            else
            {
            }
                //MessageBox.Show("" + e.Result);

        }


        private void PrintExecute()
        {
            try
            {



               
               

                 
                    DoStuff();
                 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        
        /// <summary>
        /// command that is executed when user press button Prekid stampanja
        /// </summary>
        private ICommand stopPrinting;
        public ICommand StopPrinting
        {
            get
            {
                if (stopPrinting == null)
                {
                    stopPrinting = new RelayCommand(param => StopPrintingExecute(), param => CanStopPrintingxecute());
                }
                return stopPrinting;
            }
        }

        private void StopPrintingExecute()
        {
            try
            {

                if (worker.IsBusy) worker.CancelAsync();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanStopPrintingxecute()
        {
            

            if (!worker.IsBusy)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private void DoStuff()
        {
            
            
            worker.RunWorkerAsync();

        }

        private void SaveResultsToFile()
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            DateTime today = DateTime.Today;

            TimeSpan span = today - (DateTime)this.Patient.DateOfBirth; ;
             
            int years = (zeroTime + span).Year - 1;

            using (StreamWriter sw = File.AppendText(@"..\..\AtRiskPatients.txt"))
            {
                
                string str1 = string.Format("{0},{1}", Patient.FullName, years);
                sw.WriteLine(str1);
            }
        }
    }
}
