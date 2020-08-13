using Nedeljni_II_Dejan_Prodanovic.Model;
using Nedeljni_II_Dejan_Prodanovic.Service;
using Nedeljni_II_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_II_Dejan_Prodanovic.ViewModel
{
    class ClinicMaintenanceMainViewModel:ViewModelBase
    {
        ClinicMaintenanceMain view;
        IUserService userService;
        IClinicAminService adminService;
        IClinicMaintenaceService maintenaceService;
        IClinicManagerService managerService;
        IDoctorService doctorService;

        public ClinicMaintenanceMainViewModel(ClinicMaintenanceMain addClinicDoctor, 
            vwClinicMaintenace maintenace)
        {
            view = addClinicDoctor;


            adminService = new ClinicAminService();
            userService = new UserService();
            maintenaceService = new ClinicMaintenaceService();
            managerService = new ClinicManagerService();
            doctorService = new DoctorService();
            //User = new tblUser();
            //Doctor = new tblClinicDoctor();

            //ManagerList = managerService.GetvwManagers();
            //ManagerListToPresent = new List<string>();
            //CreateManagerDictionary();

            

        }
    }
}
