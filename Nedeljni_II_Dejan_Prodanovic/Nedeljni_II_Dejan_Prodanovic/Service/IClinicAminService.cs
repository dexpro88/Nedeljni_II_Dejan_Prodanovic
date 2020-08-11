using Nedeljni_II_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_II_Dejan_Prodanovic.Service
{
    interface IClinicAminService
    {
        tblClinicAdmin AddAdmin(tblClinicAdmin admin);
        tblClinicAdmin GetAdminByUserId(int userId);
        List<tblClinicAdmin> GetAdmins();
        void SetHasCreatedClinic(tblClinicAdmin admin);
    }
}
