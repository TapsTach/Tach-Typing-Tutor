using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    public struct Theme
    {
        public string Color { get; set;}
        public string Name { get; set; }
    }

    public struct MusicFile
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
