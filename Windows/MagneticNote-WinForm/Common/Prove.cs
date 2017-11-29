using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class Prove
    {
        public static bool ProveEmail(String email)
        {
            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            Match m = r.Match(email);
            return m.Success;
        }

        public static bool ProveAccount(String account)
        {
            if (account.Length < 6)
                return false;

            String strExp = @"^[A-Za-z0-9]+$";
            Regex r = new Regex(strExp);
            Match m = r.Match(account);
            return m.Success;
        }
    }
}
