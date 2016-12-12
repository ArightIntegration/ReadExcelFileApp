using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadExcelFileApp
{
    public class RecordModel
    {
        public string Pattern { get; set; }
        public string Hole { get; set; }
        public string Reason { get; set; }
        public string DrillRig { get; set; }
        public string DrillBitDiameter { get; set; }
        public string Operator { get; set; }
        public string DryWet { get; set; }
        public string ShiftDate { get; set; }
        public string Shift { get; set; }
        public string ShiftCrew { get; set; }
        public string ActualX { get; set; }
        public string ActualY { get; set; }
        public string ActualZ { get; set; }
        public string ActualDepth { get; set; }
        public string PlannedX { get; set; }
        public string PlannedY { get; set; }
        public string PlannedZ { get; set; }
        public string PlannedDepth { get; set; }
        public string TimeDrilled { get; set; }
        public string TotalDrillTime { get; set; }
    }
}
