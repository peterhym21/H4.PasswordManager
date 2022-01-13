using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Repository.Entities
{
    public class UserDTO
    {
        public string UserName_Id { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
