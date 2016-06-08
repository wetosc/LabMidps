using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLSpecial
{
    public class User:SQLObject
    {
        public string name { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }
        public bool canViewExtra { get; set; }
        public bool canEdit { get; set; }
        public User(int right)
        {
            switch (right)
            {
                case 1:
                    isAdmin = false;
                    canEdit = false;
                    canViewExtra = false;
                    break;
                case 2:
                    isAdmin = false;
                    canEdit = false;
                    canViewExtra = true;
                    break;
                case 3:
                    isAdmin = false;
                    canEdit = true;
                    canViewExtra = true;
                    break;
                case 10:
                    isAdmin = true;
                    canEdit = true;
                    canViewExtra = true;
                    break;
            }
        }

        public User()
        {
            isAdmin = false;
            canEdit = false;
            canViewExtra = false;
        }



        #region SQLObject
        public string select()
        {
            return "use Users; SELECT * FROM Users";
        }
        public string select2(int id)
        {
            return "";
        }
        public string insertString()
        {
            string p1 = "UserName,Password, IsAdmin, CanViewExtra, CanEdit", p2 = String.Format("'{0}','{1}','{2}','{3}','{4}'", this.name, this.password, this.isAdmin?1:0, this.canViewExtra ? 1 : 0, this.canEdit ? 1 : 0 );
            return String.Format("use Users; INSERT INTO Users ({0}) output INSERTED.ID VALUES ({1});", p1, p2);
        }
        public string deleteString(int n)
        {
            return String.Format("use Users; DELETE TOP({0}) FROM Users;", n);
        }
        public string deleteOneString(int id)
        {
            return String.Format("use Users; DELETE FROM Users WHERE id ='{0}';", id);
        }
        public string updateFormat()
        {
            return "use Users; UPDATE Users SET Password = '{0}', IsAdmin = '{1}', CanViewExtra = '{2}', CanEdit = '{3}'  WHERE id = '{4}'";
        }
        #endregion

    }
}
