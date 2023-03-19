using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_HBC.DTO
{
    class Nguoidung
    {
        public Nguoidung(string username,string password, string usergroup,string hoten)
        {
            this.Username = username;
            this.Password = password;
            this.Usergroup = usergroup;
            this.Hoten = hoten;
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Usergroup { get; private set; }
        public string Hoten { get; private set; }
    }
}
