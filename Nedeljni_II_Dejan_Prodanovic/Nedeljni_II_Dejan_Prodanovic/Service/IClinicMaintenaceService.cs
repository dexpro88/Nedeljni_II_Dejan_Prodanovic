using Nedeljni_II_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_II_Dejan_Prodanovic.Service
{
    interface IClinicMaintenaceService
    {
        tblClinicMaintenace AddMaintenace(tblClinicMaintenace maintenace);
        tblClinicMaintenace GetMaintenace();
        List<tblClinicMaintenace> GetMaintenaces();
        List<vwClinicMaintenace> GetvwMaintenaces();
        void DeleteMaintenace(int maintenaceId);
        void EditMaintenace(vwClinicMaintenace maintenace);
        vwClinicMaintenace GetMaintenaceByUserId(int userId);
    }
}
