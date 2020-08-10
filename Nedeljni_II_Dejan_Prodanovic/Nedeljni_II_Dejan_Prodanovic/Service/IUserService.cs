using Nedeljni_II_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_II_Dejan_Prodanovic.Service
{
    interface IUserService
    {
        tblUser AddUser(tblUser user);
        tblUser GetUserByUserName(string username);
        tblUser GetUserByIdCardNumber(string IdCardNumber);
        tblUser GetUserByUserNameAndPassword(string username, string password);

    }
}
