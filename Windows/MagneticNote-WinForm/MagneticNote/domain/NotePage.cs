using BLL;
using Common;
using Model.MagneticNote;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MagneticNote.domain
{
    public partial class NotePage : Form
    {
        private NoteBookBLL noteBookBLL = new NoteBookBLL();
        private NoteBLL noteBLL = new NoteBLL();
        public Home homePage ;
        private Note note;

        public NotePage(int noteId)
        {
            InitializeComponent();

            if (noteId != 0)
            {
                RefreshValue(noteId);
            }
            else
            {       
                note = noteBLL.CreateNote(Info.defaultNoteBook.Id);
                
                RefreshValue(note.Id);
            }
        }

        private void RefreshValue(int noteId)
        {
            note = noteBLL.SelectObjectById(noteId) as Model.MagneticNote.Note;

            if (note != null)
            {
                text_title.Text = note.Title;
                text_content.Text = note.Content;
            }

            ToolStripMenuItem_CreateDate.Text = note.CreateDate.ToShortDateString();
            
            var list = from value in noteBookBLL.SelectAllObject()
                       select value.Name;
            ToolStripMenuItem_NoteBook.Items.AddRange(list.ToArray());
            ToolStripMenuItem_NoteBook.SelectedText = Info.defaultNoteBook.Name;
        }

        private void NotePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            note.Title = text_title.Text.Trim();
            note.Content = text_content.Text.Trim();
            if (ToolStripMenuItem_NoteBook.SelectedIndex != -1)
            {
                var noteBookId = from value in noteBookBLL.SelectAllObject()
                                 where value.Name == ToolStripMenuItem_NoteBook.Items[ToolStripMenuItem_NoteBook.SelectedIndex].ToString()
                                 select value.Id;
                note.NoteBookId = noteBookId.First<int>();

                if (String.IsNullOrEmpty(note.Title) && String.IsNullOrEmpty(note.Content))
                {
                    noteBLL.DeleteObjectById(note.Id);
                    return;
                }

                noteBLL.UpdateObject(note);
                homePage.RefreshValue();
            }
        }
    }
}
