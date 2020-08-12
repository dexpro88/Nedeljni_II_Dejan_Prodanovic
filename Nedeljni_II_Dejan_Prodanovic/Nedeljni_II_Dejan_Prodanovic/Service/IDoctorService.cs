using Nedeljni_II_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_II_Dejan_Prodanovic.Service
{
    interface IDoctorService
    {
        List<tblClinicDoctor> GetDoctors();
        List<vwClinicDoctor> GetvwDoctors();
        tblClinicDoctor AddDoctor(tblClinicDoctor doctor);
        //void DeleteManager(int managerId);
        //vwClinicManager GetManagerByUserId(int userId);
    }
}
