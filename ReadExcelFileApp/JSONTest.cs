using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadExcelFileApp
{

    public class Rootobject
    {
        public Blockdate blockDate { get; set; }
        public bool completed { get; set; }
        public int totalHoles { get; set; }
    }

    public class Blockdate
    {
        public int date { get; set; }
        public int day { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
        public int month { get; set; }
        public int seconds { get; set; }
        public long time { get; set; }
        public int timezoneOffset { get; set; }
        public int year { get; set; }
    }

}
