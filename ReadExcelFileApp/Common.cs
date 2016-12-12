using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReadExcelFileApp
{
    public class Common
    {
        public async void UpdateSummaries(string pattern,RecordModel record)
        {
            var client = new HttpClient();
            JSONHelper jsHelper = new JSONHelper();

            var tempy = client.GetAsync("https://rockmacloud.firebaseio.com/blockSummaries/" + pattern + "/.json");
            var countRes = tempy.Result.Content.ReadAsStringAsync();
            var countStr = countRes.Result.ToString();

            //Check if summary record exist
            if(countStr=="null")
            {
                //If no summary record then create new record
                string jsonToPost = jsHelper.GetBlockJSON(record);
                            HttpResponseMessage error;

                using (HttpClient httpClient = new HttpClient())
                {
                    StringContent httpConent = new StringContent(jsonToPost, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage responseMessage = null;
                    try
                    {
                        responseMessage = await httpClient.PostAsync("https://rockmacloud.firebaseio.com/blockSummaries/"+pattern+"/.json", httpConent);

                    }
                    catch (Exception ex)
                    {
                        if (responseMessage == null)
                        {
                            responseMessage = new HttpResponseMessage();
                        }
                        responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                        //responseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);

                        //lblCounter.Text = "There was an error connecting to the server!  Please check your connection and try again.";
                        //lblCounter.Refresh();
                        //lblFeedback.Text = "";
                        //lblFeedback.Refresh();
                    }
                    error = responseMessage;
                    if (error != null)
                    {
                    }

                }
            }
            else
            {
                //If record exists increment number
                var result = JsonConvert.DeserializeObject<BlockModel.Rootobject>(countStr);
                //Get total hole number and increment
                result.totalHoles = result.totalHoles + 1;

                var jsonToPost = JsonConvert.SerializeObject(result);

                //Post updated json
                Task<string> postRead;
                using (var stringContent = new StringContent(jsonToPost, System.Text.Encoding.UTF8, "application/json"))
                {
                    var post = client.PutAsync("https://rockmacloud.firebaseio.com/blockSummaries/" + pattern + "/.json", stringContent);

                    postRead = post.Result.Content.ReadAsStringAsync();
                    var temp = postRead.Result.ToString();
                }
            }
        }

        public bool CheckHeader(string name, int position)
        {
            if (position == 0)
            {
                if (name == "Pattern")
                { return true; }
                else { return false; }
            }
            else if (position == 1)
            {
                if (name == "Hole")
                { return true; }
                else { return false; }
            }
            else if (position == 2)
            {
                if (name == "Reason")
                { return true; }
                else { return false; }
            }
            else if (position == 3)
            {
                if (name == "DrillRig")
                { return true; }
                else { return false; }
            }
            else if (position == 4)
            {
                if (name == "DrillBitDiameter")
                { return true; }
                else { return false; }
            }
            else if (position == 5)
            {
                if (name == "Operator")
                { return true; }
                else { return false; }
            }
            else if (position == 6)
            {
                if (name == "DryWet")
                { return true; }
                else { return false; }
            }
            else if (position == 7)
            {
                if (name == "ShiftDate")
                { return true; }
                else { return false; }
            }
            else if (position == 8)
            {
                if (name == "Shift")
                { return true; }
                else { return false; }
            }
            else if (position == 9)
            {
                if (name == "ShiftCrew")
                { return true; }
                else { return false; }
            }
            else if (position == 10)
            {
                if (name == "ActualX")
                { return true; }
                else { return false; }
            }
            else if (position == 11)
            {
                if (name == "ActualY")
                { return true; }
                else { return false; }
            }
            else if (position == 12)
            {
                if (name == "ActualZ")
                { return true; }
                else { return false; }
            }
            else if (position == 13)
            {
                if (name == "ActualDepth")
                { return true; }
                else { return false; }
            }
            else if (position == 14)
            {
                if (name == "PlannedX")
                { return true; }
                else { return false; }
            }
            else if (position == 15)
            {
                if (name == "PlannedY")
                { return true; }
                else { return false; }
            }
            else if (position == 16)
            {
                if (name == "PlannedZ")
                { return true; }
                else { return false; }
            }
            else if (position == 17)
            {
                if (name == "PlannedDepth")
                { return true; }
                else { return false; }
            }
            else if (position == 18)
            {
                if (name == "TimeDrilled")
                { return true; }
                else { return false; }
            }
            else if (position == 19)
            {
                if (name == "TotalDrillTime")
                { return true; }
                else { return false; }
            }
            return false;
        }

    }
}
