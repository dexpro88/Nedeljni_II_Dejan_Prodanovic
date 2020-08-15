using Nedeljni_II_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_II_Dejan_Prodanovic.Service
{
    interface IClinicManagerService
    {
        List<tblClinicManager> GetManagers();
        List<vwClinicManager> GetvwManagers();
        tblClinicManager AddManager(tblClinicManager manager);
        void DeleteManager(int managerId);
        void EditManager(vwClinicManager manager);
        vwClinicManager GetManagerByUserId(int userId);
        vwClinicManager GetManagerByManagerId(int managerId);
        List<tblClinicDoctor> GetDoctorsOfManager(int managerId);
        void PunishManager(int managerId);
    }
}
