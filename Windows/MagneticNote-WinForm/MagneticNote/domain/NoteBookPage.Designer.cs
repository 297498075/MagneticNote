namespace MagneticNote.domain
{
    partial class NoteBookPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoteBookPage));
            this.button_Ok = new System.Windows.Forms.Button();
            this.textBox_NoteBookName = new System.Windows.Forms.TextBox();
            this.label_NoteBook_Name = new System.Windows.Forms.Label();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Ok
            // 
            this.button_Ok.Enabled = false;
            this.button_Ok.Location = new System.Drawing.Point(221, 116);
            this.button_Ok.Name = "button_Ok";
            this.button_Ok.Size = new System.Drawing.Size(80, 25);
            this.button_Ok.TabIndex = 0;
            this.button_Ok.Text = "确认(O)";
            this.button_Ok.UseVisualStyleBackColor = true;
            this.button_Ok.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // textBox_NoteBookName
            // 
            this.textBox_NoteBookName.Location = new System.Drawing.Point(20, 65);
            this.textBox_NoteBookName.Name = "textBox_NoteBookName";
            this.textBox_NoteBookName.Size = new System.Drawing.Size(388, 25);
            this.textBox_NoteBookName.TabIndex = 1;
            this.textBox_NoteBookName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_NoteBookName_KeyDown);
            // 
            // label_NoteBook_Name
            // 
            this.label_NoteBook_Name.AutoSize = true;
            this.label_NoteBook_Name.Location = new System.Drawing.Point(20, 30);
            this.label_NoteBook_Name.Name = "label_NoteBook_Name";
            this.label_NoteBook_Name.Size = new System.Drawing.Size(90, 15);
            this.label_NoteBook_Name.TabIndex = 2;
            this.label_NoteBook_Name.Text = "笔记本名称:";
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(328, 116);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(80, 25);
            this.button_cancel.TabIndex = 3;
            this.button_cancel.Text = "取消(C)";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // NoteBookPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(432, 153);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.label_NoteBook_Name);
            this.Controls.Add(this.textBox_NoteBookName);
            this.Controls.Add(this.button_Ok);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NoteBookPage";
            this.Text = "新建笔记本";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Ok;
        private System.Windows.Forms.TextBox textBox_NoteBookName;
        private System.Windows.Forms.Label label_NoteBook_Name;
        private System.Windows.Forms.Button button_cancel;
    }
}