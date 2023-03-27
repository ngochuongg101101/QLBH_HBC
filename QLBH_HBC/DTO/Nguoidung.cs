using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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
        public Nguoidung(DataRow Row) => this.Hoten = Row["HOTEN"].ToString();
        private string Username { get; }
        public string Password { get; private set; }
        public string Usergroup { get; private set; }
        public string Hoten { get; private set; }
    }
}
