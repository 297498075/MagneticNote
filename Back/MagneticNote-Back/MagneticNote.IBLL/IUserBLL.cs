using MagneticNote.Model.Data;
using System;

namespace MagneticNote.IBLL
{
    public interface IUserBLL : IBaseBLL<User>
    {
        User SelectByEmail(String Email);
        User SelectByEmail(String Email, String Password);
        User SelectByAccount(String Account);
        User SelectByAccount(String Account, String Password);  
    }
}
