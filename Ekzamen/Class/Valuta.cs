using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekzamen.Class
{
    //<item>
    //<title>AUD</title>
    //<pubDate>19.07.18</pubDate>
    //<description>253.58</description>
    //<quant>1</quant>
    //<index>DOWN</index>
    //<change>-0.97</change>
    //<link/>
    //</item>
    public class Valuta
    {
        public string nazvanie { get; set; }
        public DateTime data { get; set; }
        public double cena { get; set; }
        public int kollvo { get; set; }
        public string izmenenie { get; set; }
        public double vel_Izmeneniya { get; set; }
    }
}
