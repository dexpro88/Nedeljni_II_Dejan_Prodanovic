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
        vwClinicManager GetManagerByUserId(int userId);
    }
}
