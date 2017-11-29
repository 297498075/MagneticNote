using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Common;
using System.Net.Mail;
using System.Net;
using Model.Entity;

namespace MagneticNote
{
    public partial class Default : Form
    {
        private UserBLL userBLL = new UserBLL();

        public Default()
        {
            InitializeComponent();
            String path = System.Environment.CurrentDirectory;

            webBrowser.Navigate(path + @"\html\Defualt.html");
        }
        private void menu_Login_Click(object sender, EventArgs e)
        {
            SetVisibleFalse();

            this.menu_Login.Location = new Point(613, 68);
            this.menu_Reset.Location = new Point(613, 491);
            this.label_Login_UserNameOrEmail.Visible = true;
            this.label_Login_Password.Visible = true;
            this.label_Login_Title.Visible = true;
            this.text_Login_Password.Visible = true;
            this.text_Login_UserNameOrEmail.Visible = true;
            this.button_Login.Visible = true;
        }
        private void menu_Register_Click(object sender, EventArgs e)
        {
            SetVisibleFalse();

            this.menu_Reset.Location = new Point(613, 491);
            this.menu_Login.Location = new Point(613, 462);
            this.label_Register_Email.Visible = true;
            this.label_Register_Password.Visible = true;
            this.label_Register_Title.Visible = true;
            this.text_Register_Email.Visible = true;
            this.text_Register_Password.Visible = true;
            this.button_Register.Visible = true;
            this.label_Register_Footer.Visible = true;
        }
        private void menu_Reset_Click(object sender, EventArgs e)
        {
            SetVisibleFalse();

            this.menu_Login.Location = new Point(613, 68);
            this.menu_Reset.Location = new Point(613, 97);

            this.label_Reset_Title.Visible = true;
            this.label_Reset_UserNameOrEmail.Visible = true;
            this.label_Reset_suggest.Visible = true;
            this.text_Reset_UserNameOrEmail.Visible = true;
            this.button_Reset.Visible = true;
        }
        private void SetVisibleFalse()
        {
            this.label_Login_UserNameOrEmail.Visible = false;
            this.label_Login_Password.Visible = false;
            this.label_Login_Title.Visible = false;
            this.text_Login_Password.Visible = false;
            this.text_Login_UserNameOrEmail.Visible = false;
            this.button_Login.Visible = false;

            this.label_Register_Email.Visible = false;
            this.label_Register_Password.Visible = false;
            this.label_Register_Title.Visible = false;
            this.text_Register_Email.Visible = false;
            this.text_Register_Password.Visible = false;
            this.button_Register.Visible = false;
            this.label_Register_Footer.Visible = false;

            this.label_Reset_suggest.Visible = false;
            this.label_Reset_Title.Visible = false;
            this.label_Reset_UserNameOrEmail.Visible = false;
            this.text_Reset_UserNameOrEmail.Visible = false;
            this.button_Reset.Visible = false;
        }
        private void button_Login_Click(object sender, EventArgs e)
        {
            String userNameOrEmail = text_Login_UserNameOrEmail.Text.Trim();
            String password = text_Login_Password.Text;
            bool bo = false;

            if (userNameOrEmail.Contains("@"))
            {
                bo = userBLL.Login(new User() { Email = userNameOrEmail, Password = password });
            }
            else
            {
                bo = userBLL.Login(new User() { Name = userNameOrEmail, Password = password });
            }

            if (bo == true)
            {
                this.Hide();
                
                new domain.Home().Show();
            }
            else
            {
                MessageBox.Show("账号或密码错误");
            }
        }
        private void button_Register_Click(object sender, EventArgs e)
        {
            String email = text_Register_Email.Text.Trim();
            String password = text_Register_Password.Text;

            if (userBLL.Register(new User() { Email = email, Password = password }))
            {
                MessageBox.Show("已将注册信息下发至您的邮箱，请注意查收！");
            }
            else
            {
                MessageBox.Show("注册失败");
            }
        }
        private void text_Login_Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Prove.ProveAccount(text_Login_UserNameOrEmail.Text.Trim()) || Prove.ProveEmail(text_Login_UserNameOrEmail.Text.Trim())) && text_Login_Password.Text.Length > 4)
            {
                button_Login.Enabled = true;
            }
            else
            {
                button_Login.Enabled = false;
            }
        }
        private void text_Login_UserNameOrEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Prove.ProveAccount(text_Login_UserNameOrEmail.Text.Trim()) || Prove.ProveEmail(text_Login_UserNameOrEmail.Text.Trim())) && text_Login_Password.Text.Length > 4)
            {
                button_Login.Enabled = true;
            }
            else
            {
                button_Login.Enabled = false;
            }
        }
        private void text_Register_Email_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Prove.ProveEmail(text_Register_Email.Text.Trim()) && text_Login_Password.Text.Length > 4)
            {
                button_Register.Enabled = true;
            }
            else
            {
                button_Register.Enabled = false;
            }
        }
        private void text_Register_Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Prove.ProveEmail(text_Register_Email.Text.Trim()) && text_Register_Password.Text.Length > 4)
            {
                button_Register.Enabled = true;
            }
            else
            {
                button_Register.Enabled = false;
            }
        }
        private void text_Reset_UserNameOrEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Prove.ProveEmail(text_Reset_UserNameOrEmail.Text.Trim()))
            {
                button_Reset.Enabled = true;
            }
            else
            {
                button_Reset.Enabled = false;
            }
        }
        private void button_Reset_Click(object sender, EventArgs e)
        {
            MailMessage msg = new MailMessage();

            msg.Subject = "重置密码";   //标题
            msg.Body = "测试";    //内容
            msg.From = new MailAddress("c297498075@163.com");  //邮件来自哪
            msg.To.Add(text_Reset_UserNameOrEmail.Text.Trim());
            SmtpClient client = new SmtpClient(); //允许传输协议
            client.Host = "smtp.163.com";   //发件方服务器地址
            client.Port = 25;  //发件方端口
            NetworkCredential credential = new NetworkCredential();
            credential.UserName = "c297498075@163.com";
            credential.Password = "c1972929";
            client.Credentials = credential;  //说明证书要给代理证书credential

           // Attachment att = new Attachment(this.text_Reset_UserNameOrEmail.Text);
           // msg.Attachments.Add(att);

            client.Send(msg);
        }
    }
}
