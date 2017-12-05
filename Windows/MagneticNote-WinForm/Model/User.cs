using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticNote.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Account { get; set; }
    }

    public class UserValue
    {
        public User User { get; set; }
    }

    public class UserValues
    {
        public IList<User> User { get; set; }
    }
}
