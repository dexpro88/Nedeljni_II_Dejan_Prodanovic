//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nedeljni_II_Dejan_Prodanovic.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class vwClinicManager
    {
        public int ManagerID { get; set; }
        public Nullable<int> MaxNumberOfDoctors { get; set; }
        public Nullable<int> MinNumberOdRooms { get; set; }
        public Nullable<int> NumberOfOmissions { get; set; }
        public string ManagerFloor { get; set; }
        public Nullable<int> UserID { get; set; }
        public string IDCardNumber { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Username { get; set; }
    }
}