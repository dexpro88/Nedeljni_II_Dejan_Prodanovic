using Nedeljni_II_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_II_Dejan_Prodanovic.Service
{
    interface IClinicService
    {
        tblClinicInstitution AddClinic(tblClinicInstitution clinic);
        tblClinicInstitution GetClinic();
        void EditClinic(tblClinicInstitution clinic);
    }
}
