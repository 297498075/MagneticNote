using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticNote.Model
{
    public class NoteBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int BookGroupId { get; set; }
    }
}
