using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadExcelFileApp
{
    public class JSONHelper
    {
        public string GetHoleJSON(RecordModel record)
        {
            DateTime drillDT = DateTime.Parse(record.TimeDrilled);
            return "{\"holes\":{\"actualDepth\":"+record.ActualDepth+",\"actualX\":"+record.ActualX+ ",\"actualY\":"+record.ActualY+ ",\"actualZ\":"+record.ActualZ+ ",\"drillRig\":\""+record.DrillRig+ "\",\"holeDrilledTime\":{\"date\":0,\"day\":"+drillDT.Day+ ",\"hours\":"+drillDT.Hour+ ",\"minutes\":"+drillDT.Minute+ ",\"month\":"+drillDT.Month+ ",\"seconds\":"+drillDT.Second+ ",\"time\":\""+drillDT.ToLongTimeString()+ "\",\"timezoneOffset\":-120,\"year\":"+drillDT.Year+ "},\"holeNumber\":\""+record.Hole+ "\",\"operator\":\""+record.Operator+ "\",\"plannedDepth\":"+record.PlannedDepth+ ",\"plannedX\":"+record.PlannedX+ ",\"plannedY\":"+record.PlannedY+ ",\"plannedZ\":"+record.PlannedZ+ ",\"reason\":\""+record.Reason+"\"}}";
        }

        public string GetBlockJSON(RecordModel record)
        {
            return "{\""+record.Pattern+"\": {\"blockDate\": {\"date\": 2,\"day\": 3,\"hours\": 23,\"minutes\": 0,\"month\": 7,\"seconds\": 0,\"time\": 61428315600000,\"timezoneOffset\": -120,\"year\": 2016},\"completed\": true,\"totalHoles\": 1}}";
        }
    }
}
