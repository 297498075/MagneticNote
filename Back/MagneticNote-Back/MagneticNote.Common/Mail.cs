using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Mail
    {
        public static bool Send(String email, String info)
        {
            MailMessage msg = new MailMessage();

            msg.Subject = "确认链接";   //标题
            msg.Body = info;    //内容
            msg.From = new MailAddress(Info.EmailUserName);  //邮件来自哪
            msg.To.Add(email);
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient(); //允许传输协议
            client.Host = Info.Host;   //发件方服务器地址
            client.Port = 25;  //发件方端口
            NetworkCredential credential = new NetworkCredential();
            credential.UserName = Info.EmailUserName;
            credential.Password = Info.EmailPassword;
            client.Credentials = credential;  //说明证书要给代理证书credential

            //Attachment att = new Attachment(email);
            //msg.Attachments.Add(att);

            client.Send(msg);

            return true;
        }

        public static String GetView(String name, String str)
        {
            String val = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=gb18030'><meta name='referrer' content='origin'><meta name='renderer' content='webkit'><title>邮箱有效性验证 - QQ邮箱</title><link rel='stylesheet' href='https://cdn.bootcss.com/bootstrap/3.3.7/css/bootstrap.min.css' integrity='sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u' crossorigin='anonymous'></head><body><div class='mailcontainer' id='qqmail_mailcontainer'><section class='main'><header class='Row'><div class='col-xs-2'><img class='logo' src='http://"
                         + Info.ResourceAddress
                         + "/image/Log.png' width='68' height='68'></div><div class='title col-xs-8'><h1>邮箱验证</h1></div></header><h4>此邮件由系统自动生成，请勿回复。</h4><section class='content row'><p class='left-near'><a href='#' target='_blank'>"
                         + name
                         + "</a>，你好！</p><p>你于<span style='border-bottom:1px dashed #ccc;' t='5' times=' 23:01'>"
                         + DateTime.Now
                         + "</span> 注册了MagneticNote账号，请点击以下按钮完成验证邮箱操作：</p><a href='http://" 
                         + Info.Address
                         + "/Register/Validate/"
                         + str
                         + "' class='btn btn-lg btn-success'>验证邮箱</a><p>若按钮无法点击，请点击以下链接，或将地址复制到浏览器打开：</p><p><a href='http://"
                         + Info.Address
                         + "/Register/Validate/"
                         + str
                         + "' target='_blank'>http://"
                         + Info.Address 
                         + "/Register/Validate/"
                         + str
                         + "</a></p><p>验证邮件<span class='bold'>24小时有效</span>。若已失效，请在<a href='http://"
                         + Info.Address
                         + "' target='_blank'>官网</a>页面重新发送验证邮件</p><p class='left-near'>此致</p><p class='left-near'><span class='bold'>MagneticNote(note.snkdev.top)</span></p></section><footer><p><a href='http://"
                         + Info.Address
                         + "/Login' target='_blank'>登录MagneticNote</a> |<a href='http://"
                         + Info.Address
                         + "/About' target='_blank'>关于我们</a> |<a href='http://"
                         + Info.Address
                         + "/Agreement' target='_blank'>用户协议</a></p><p>客服QQ：<span style='border-bottom:1px dashed #ccc;z-index:1' t='7' onclick='return false;' data='2801385973'>无</span> &nbsp;&nbsp;免费咨询电话：无 &nbsp;&nbsp;客服邮箱：<a href='mailto:chen29749@outlook.com' target='_blank'>chen29749@outl<wbr>ook.com</a></p></footer></section></div></body></html>";

            return val;
        }
    }
}
