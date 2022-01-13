using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Repository.Entities
{
    public class PasswordDTO
    {
        public string Password_Id { get; set; }
        public string Salt { get; set; }

        public string UserName_FKId { get; set; }

        public User User { get; set; }
    }
}
