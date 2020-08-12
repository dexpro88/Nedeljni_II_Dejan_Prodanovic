using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_II_Dejan_Prodanovic.ViewModel
{
    class ClinicMaintenance
    {
        public int ClinicMaintenaceID { get; set; }
        public Nullable<bool> CanChooseInvalidAccess { get; set; }
        public Nullable<bool> CanChooseClinicExpansionPermission { get; set; }
        public Nullable<bool> CanChooseAmbulanceAccess { get; set; }
        public Nullable<int> UserID { get; set; }
        public string IDCardNumber { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Username { get; set; }
    }
}
