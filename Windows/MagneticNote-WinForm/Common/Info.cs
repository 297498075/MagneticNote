﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MagneticNote.Model;

namespace Common
{
    public class Info
    {
        public static User user { get; set; }

        public static IList<NoteBook> defaultNoteBook { get; set; }

        public static Dictionary<String,Image> Image { get; set; }

        public static Dictionary<int,Note> noteList { get; set; }

        public static Dictionary<int,NoteBook> noteBookList { get; set; }

        public static Dictionary<int,BookGroup> bookGroupList { get; set; }
    }
}
