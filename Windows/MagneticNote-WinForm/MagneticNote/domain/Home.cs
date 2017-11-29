using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model.MagneticNote;
using BLL;
using Common;
using System.Threading;

namespace MagneticNote.domain
{
    public partial class Home : Form
    {
        private BookGroupBLL bookGroupBLL = new BookGroupBLL();
        private NoteBookBLL noteBookBLL = new NoteBookBLL();
        private NoteBLL noteBLL = new NoteBLL();

        public Home()
        {
            InitializeComponent();
            RefreshValue();
            treeView_BookGroupAndNoteBook.ExpandAll();

        }
        public void RefreshValue()
        {
            treeView_BookGroupAndNoteBook.Nodes.Clear();
            listView_noteBook.Clear();
            ToolStripMenuItem_NoteBook.Items.Clear();
            var list = from value in noteBookBLL.SelectAllObject()
                       select value.Name;
            List<String> items = list.ToList<String>();
            ToolStripMenuItem_NoteBook.Items.AddRange(items.ToArray());
            ToolStripMenuItem_NoteBook.SelectedIndex = items.IndexOf(Info.defaultNoteBook.Name);
            Dictionary<String, Image> dictionary = new Dictionary<string, Image>();
            dictionary.Add("options_saving", Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "image\\options_saving.gif"));
            dictionary.Add("options_save", Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "image\\options_save.png"));
            Info.Image = dictionary;
            TryRefreshBookGroup();
           
        }

        private void TryRefreshBookGroup()
        {
            treeView_BookGroupAndNoteBook.Nodes.Clear();
            List<BookGroup> bookGroupList = bookGroupBLL.SelectObjectsByUserId(Info.user.Id);
            treeView_BookGroupAndNoteBook.ImageList = imageList_Group;
            this.Text += Info.user.Name;

            TreeNode rootNode = new TreeNode();
            rootNode.Text = "笔记本";
            rootNode.Name = "root";
            rootNode.ImageKey = "NoteBook.ico";
            rootNode.Tag = null;
            treeView_BookGroupAndNoteBook.Nodes.Add(rootNode);

            foreach (BookGroup value in bookGroupList)
            {
                TreeNode node = new TreeNode();
                node.Name = value.Id.ToString();
                node.Text = value.Name;
                node.Tag = value;
                node.NodeFont = new Font("宋体", 11);

                if (node.Text.Equals("默认分组"))
                {
                    foreach (NoteBook noteBook in noteBookBLL.SelectObjectByGroupId(Convert.ToInt32(node.Name)))
                    {
                        TreeNode cnode = new TreeNode();
                        int count = noteBookBLL.SelectCountById(noteBook.Id);
                        cnode.Name = noteBook.Id.ToString();
                        cnode.Text = noteBook.Name;
                        cnode.Tag = noteBook;
                        cnode.Text += count != 0 ? "(" + count + ")" : String.Empty;
                        cnode.NodeFont = new Font("宋体", 9);
                        rootNode.Nodes.Add(cnode);
                    }
                    TryRefreshNoteBook(value.NoteBook.First<NoteBook>());
                    continue;
                }
                else
                {
                    foreach (NoteBook noteBook in noteBookBLL.SelectObjectByGroupId(Convert.ToInt32(node.Name)))
                    {
                        TreeNode cnode = new TreeNode();
                        int count = noteBookBLL.SelectCountById(noteBook.Id);
                        cnode.Name = noteBook.Id.ToString();
                        cnode.Text = noteBook.Name;
                        cnode.Tag = noteBook;
                        cnode.Text += count != 0 ? "(" + count + ")" : String.Empty;
                        cnode.NodeFont = new Font("宋体", 9);
                        node.Nodes.Add(cnode);
                    }
                }
                TryRefreshNoteBook();
                rootNode.Nodes.Add(node);
            }
        }
        private void TryRefreshNoteBook()
        {
            List<Note> noteList = noteBLL.SelectAllObject();

            if (noteList != null && noteList.Count != 0)
            {
                listView_noteBook.Items.Clear();

                foreach (Note note in noteList)
                {
                    ListViewItem item = new ListViewItem();

                    item.Name = note.Id.ToString();
                    item.Text = note.Title;

                    listView_noteBook.Items.Add(item);
                }
                TryRefreshNote(noteList[0].Id);
            }
        }
        private void TryRefreshNoteBook(NoteBook noteBook)
        {
            if (noteBook == null) return;
            List<Note> noteList = noteBLL.SelectObjectByNoteBookId(noteBook.Id);

            if (noteList != null && noteList.Count != 0)
            {
                listView_noteBook.Items.Clear();

                foreach (Note note in noteList)
                {
                    ListViewItem item = new ListViewItem();

                    item.Name = note.Id.ToString();
                    item.Text = note.Title;

                    listView_noteBook.Items.Add(item);
                }
                TryRefreshNote(noteList[0].Id);
            }
        }
        private void TryRefreshNoteBook(BookGroup bookGroup)
        {
            if (bookGroup == null) return;
            List<Note> noteList = noteBLL.SelectObjectByBookGroupId(bookGroup.Id);

            if (noteList != null && noteList.Count != 0)
            {
                listView_noteBook.Items.Clear();

                foreach (Note note in noteList)
                {
                    ListViewItem item = new ListViewItem();

                    item.Name = note.Id.ToString();
                    item.Text = note.Title;

                    listView_noteBook.Items.Add(item);
                }
                TryRefreshNote(noteList[0].Id);
            }
        }
        private void TryRefreshNote(int noteId)
        {
            Note note = noteBLL.SelectObjectById(noteId) as Note;

            if (note != null)
            {
                text_title.Text = note.Title;
                text_content.Text = note.Content;
                ToolStripMenuItem_CreateDate.Text = note.CreateDate.ToShortDateString();

                ToolStripMenuItem_NoteBook.Text = String.Empty;
                ToolStripMenuItem_NoteBook.SelectedText = note.NoteBook.Name;
            }
            else
            {
                text_title.Text = String.Empty;
                text_content.Text = String.Empty;
                ToolStripMenuItem_NoteBook.Text = String.Empty;
                ToolStripMenuItem_NoteBook.SelectedText = Info.defaultNoteBook.Name;
            }
        }

        private void treeView_BookGroupAndNoteBook_MouseClick(object sender, MouseEventArgs e)
        {
            TreeView treeView = sender as TreeView;
            TreeNode node = treeView.GetNodeAt(e.X, e.Y) as TreeNode;
            if (node != null)
            {
                if (node.Nodes.Count == 0) //笔记本节点
                {
                    TryRefreshNoteBook((NoteBook)noteBookBLL.SelectObjectById(Convert.ToInt32(node.Name)));
                }
                else if (node.Name.Equals("root"))
                {
                    TryRefreshNoteBook();
                }
                else //分组节点
                {
                    TryRefreshNoteBook((BookGroup)bookGroupBLL.SelectObjectById(Convert.ToInt32(node.Name)));
                }
            }
        }
        private void listView_noteBook_MouseClick(object sender, MouseEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView.SelectedItems.Count > 0)
            {
                TryRefreshNote(Convert.ToInt32(listView.SelectedItems[0].Name));
            }
        }
        private void ToolStripMenuItem_CreateNote_Click(object sender, EventArgs e)
        {
            NotePage notePage = new NotePage(0);
            notePage.homePage = this;
            notePage.Show();
        }
        private void treeView_BookGroupAndNoteBook_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = treeView_BookGroupAndNoteBook.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    NoteBook noteBook = CurrentNode.Tag as NoteBook;
                    if (noteBook != null)
                    {
                        if (noteBook.BookGroupId == Info.defaultNoteBook.BookGroupId)
                            CurrentNode.ContextMenuStrip = contextMenuStrip_GroupNoteBook;
                        else
                            CurrentNode.ContextMenuStrip = contextMenuStrip_Group_NoteBook;
                    }
                    else if (CurrentNode.Tag as BookGroup != null)
                    {
                        CurrentNode.ContextMenuStrip = contextMenuStrip_Group;
                    }
                    else if (CurrentNode.Tag == null)
                    {
                        CurrentNode.ContextMenuStrip = contextMenuStrip_RootGroup;
                    }

                    treeView_BookGroupAndNoteBook.SelectedNode = CurrentNode;//选中这个节点
                }
            }
        }
        private void listView_noteBook_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                ListViewItem CurrentItem = listView_noteBook.GetItemAt(e.X, e.Y);
                if (CurrentItem != null)//判断你点的是不是一个节点
                {
                    listView_noteBook.ContextMenuStrip = contextMenuStrip_NoteBook;
                    listView_noteBook.SelectedItems.Clear();
                    CurrentItem.Selected = true;//选中这个节点
                }
                else
                {
                    listView_noteBook.ContextMenuStrip = null;
                }
            }
        }
        private void DToolStripMenuItem_BookGroup_DeleteBookGroup_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView_BookGroupAndNoteBook.SelectedNode;
            NoteBook noteBook = node.Tag as NoteBook;
            BookGroup bookGroup = node.Tag as BookGroup;
            if (node != null)
            {
                if (noteBook != null) //笔记本节点
                {
                    DialogResult dr = MessageBox.Show("确认要删除笔记本\"" + node.Text + "\"?", "确认操作", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        noteBookBLL.DeleteObjectById(Convert.ToInt32(node.Name));
                    }
                }
                else if (node.Name.Equals("root"))
                {

                }
                else //分组节点
                {
                    DialogResult dr = MessageBox.Show("确认要删除分组\"" + node.Text + "\"?", "确认操作", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        bookGroupBLL.DeleteObjectById(bookGroup.Id);
                    }
                }
            }
            RefreshValue();
        }
        private void BToolStripMenuItem_Top_CreateNoteBook_Click(object sender, EventArgs e)
        {
            NoteBookPage noteBookPage = new NoteBookPage();
            noteBookPage.homePage = this;
            NoteBook noteBook = treeView_BookGroupAndNoteBook.SelectedNode.Tag as NoteBook;
            BookGroup bookGroup = treeView_BookGroupAndNoteBook.SelectedNode.Tag as BookGroup;
            if (noteBook != null) noteBookPage.bookGroupId = noteBook.BookGroupId;
            if (bookGroup != null) noteBookPage.bookGroupId = bookGroup.Id;
            if (noteBookPage.bookGroupId == 0) noteBookPage.bookGroupId = Info.defaultNoteBook.BookGroupId;
            this.Enabled = false;
            noteBookPage.Show();
        }
        private void NToolStripMenuItem_CreateGroup_Click(object sender, EventArgs e)
        {
            List<NoteBook> list = new List<NoteBook>();
            NoteBook noteBook = (NoteBook)treeView_BookGroupAndNoteBook.SelectedNode.Tag;
            list.Add((NoteBook)noteBookBLL.SelectObjectById(noteBook.Id));
            BookGroup bookGroup = new BookGroup() { Name = "笔记本组", UserId = Info.user.Id };
            bookGroupBLL.AddObject(bookGroup);
            noteBook.BookGroupId = bookGroup.Id;
            noteBookBLL.UpdateObject(noteBook);
            RefreshValue();
        }
        private void NToolStripMenuItem_BookGroup_CreateNoteBook_Click(object sender, EventArgs e)
        {
            BToolStripMenuItem_Top_CreateNoteBook_Click(sender, e);
        }
        private void ToolStripMenuItem_BookGroup_CreateNoteBook_Click(object sender, EventArgs e)
        {
            BToolStripMenuItem_Top_CreateNoteBook_Click(sender, e);
        }
        private void DToolStripMenuItem_BookGroupDeleteNoteBook_Click(object sender, EventArgs e)
        {
            DToolStripMenuItem_BookGroup_DeleteBookGroup_Click(sender, e);
        }
        private void DToolStripMenuItem_BookGroup_DeleteNoteBook_Click(object sender, EventArgs e)
        {
            DToolStripMenuItem_BookGroup_DeleteBookGroup_Click(sender, e);
        }
        private void ToolStripMenuItem_RootGroup_CreateNoteBook_Click(object sender, EventArgs e)
        {
            BToolStripMenuItem_Top_CreateNoteBook_Click(sender, e);
        }

        private void ToolStripMenuItem_tool_synchronous_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_tool_synchronous.Image = Info.Image["options_saving"];
            new Thread(new ParameterizedThreadStart(RefreshImage)).Start(this);
            Thread.Sleep(3000);
            ToolStripMenuItem_tool_synchronous.Image = Info.Image["options_save"];

        }

        private void RefreshImage(Object obj)
        {
            Home homePage = obj as Home;
            homePage.TryRefreshBookGroup();
        }
    }
}
