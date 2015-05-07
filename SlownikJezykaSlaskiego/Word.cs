using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlownikJezykaSlaskiego
{
    class Word
    {

        public Word(int order, string polish, string silesian)
        {
            this.order = order;
            this.polish = polish;
            this.silesian = silesian;
        }
        public int order { get; set; }
        public string polish { get; set; }
        public string silesian { get; set; }

    }
}
