using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticNote.Model
{
    public class BookGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }

    public class BookGroupValue
    {
        public BookGroup BookGroup { get; set; }
    }

    public class BookGroupValues
    {
        public IList<BookGroup> BookGroupList { get; set; }
    }
}
