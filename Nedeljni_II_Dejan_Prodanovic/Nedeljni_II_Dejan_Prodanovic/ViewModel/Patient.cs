using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_II_Dejan_Prodanovic.ViewModel
{
    class Patient
    {
        public int PatientID { get; set; }
        public string HealthCardNumber { get; set; }
        public Nullable<System.DateTime> HealthAssuranceExpiryDate { get; set; }
        public string DoctorUniqueNumber { get; set; }
        public Nullable<int> DoctorId { get; set; }
        public Nullable<int> UserID { get; set; }
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
