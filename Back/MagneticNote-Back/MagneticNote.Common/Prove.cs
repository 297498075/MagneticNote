/***********************************************
'类 名 称:Prove
'命名空间:Common
'创建时间:2017-06-13 09:01
'作    者:陈相林
'描    述:检查文本格式
'修改时间:2017-06-20 21:53
'修 改 人:陈相林
'版 本 号:v1.0.0
'**********************************************/

using System;
using System.Text.RegularExpressions;

namespace Common
{
    public class Prove
    {
        //检查Email格式
        public static bool ProveEmail(String email)
        {
            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            Match m = r.Match(email);
            return m.Success;
        }

        //检查账户格式
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
