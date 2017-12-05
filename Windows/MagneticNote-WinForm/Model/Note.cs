using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticNote.Model
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public int NoteBookId { get; set; }
    }

    public class NoteValue
    {
        public Note Note { get; set; }
    }

    public class NoteValues
    {
        public IList<Note> NoteList { get; set; }
    }
}
