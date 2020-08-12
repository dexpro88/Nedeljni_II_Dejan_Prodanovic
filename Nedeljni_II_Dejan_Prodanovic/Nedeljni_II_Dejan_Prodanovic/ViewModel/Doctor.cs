using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_II_Dejan_Prodanovic.ViewModel
{
    class Doctor
    {
        public string UniqueNumber { get; set; }
        public string AccountNumber { get; set; }
        public string DoctorShift { get; set; }
        public Nullable<bool> RecievesPatients { get; set; }
        public string Sector { get; set; }
        public Nullable<int> ManagerID { get; set; }
        public Nullable<int> UserID { get; set; }
        public int DoctorID { get; set; }
        public string IDCardNumber { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Username { get; set; }
        public string ManagerFullName { get; set; }
        public string ManagerIDCardNumber { get; set; }

    }
}
