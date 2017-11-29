﻿using BLL;
using Common;
using Model.MagneticNote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagneticNote.domain
{
    public partial class NoteBookPage : Form
    {
        public NoteBookBLL noteBookBLL = new NoteBookBLL();
        public Home homePage { get; set; }
        public int bookGroupId { get; set; }

        public NoteBookPage()
        {
            InitializeComponent();
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            String name = textBox_NoteBookName.Text;
            if (noteBookBLL.SelectObjectByName(name) == null)
            {
                noteBookBLL.AddObject(new NoteBook() { Name = name, BookGroupId= bookGroupId});
                this.Dispose();
                homePage.Enabled = true;
                homePage.RefreshValue();
            }
            else
            {
                MessageBox.Show("已有此笔记本，请换一个名字","提示",MessageBoxButtons.OK);
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            homePage.Enabled = true;
            homePage.RefreshValue();
        }

        private void textBox_NoteBookName_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox_NoteBookName.Text.Trim().Count<char>() > 3)
            {
                button_Ok.Enabled = true;
            }
        }
    }
}
