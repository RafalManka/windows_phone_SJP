using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlownikJezykaSlaskiego
{
    class Story
    {
        public string title { get; set; }
        public string content { get; set; }
        public List<Word> words { get; set; }
    }
}
