using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Net.Http;
using System.Net;


namespace ReadExcelFileApp
{
    public partial class Form1 : Form
    {
        JSONHelper jsHelper = new JSONHelper();
        Common cm = new Common();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string fileExt = string.Empty;
            int recordCount = 0;
            OpenFileDialog file = new OpenFileDialog();//open dialog to choose file
            file.Filter = "Excel Files|*.xls;*.xlsx";
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)//if there is a file choosen by the user
            {
                lblFeedback.Text = "Reading file...";
                lblFeedback.Refresh();
                filePath = file.FileName;//get the path of the file
                fileExt = Path.GetExtension(filePath);//get the file extension
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        dtExcel = ReadExcel(filePath, fileExt);//read excel file
                        dataGridView1.Visible = false;
                        //dataGridView1.DataSource = dtExcel;
                        recordCount = dtExcel.Rows.Count;
                        lblFeedback.Text = "";
                        ProcessDT(dtExcel);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);//custom messageBox to show error
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();//to close the window(Form1)
        }

        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)//compare the extension of the file
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';";//for below excel 2007
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';";//for above excel 2007
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet1$]", con);//here we read data from sheet1
                    oleAdpt.Fill(dtexcel);//fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            return dtexcel;
        }

        public void ProcessDT(DataTable dt)
        {
            int count = 0;
            int recordCount = dt.Rows.Count;
            bool headersOK = false;
            List<RecordModel> recordList = new List<RecordModel>();

            //Check data header
            foreach (DataColumn column in dt.Columns)
            {
                headersOK= cm.CheckHeader(column.ColumnName, count);
                count++;
            }

            if(headersOK)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var record = new RecordModel();
                    record.Pattern = row["Pattern"].ToString();
                    record.Hole = row["Hole"].ToString();
                    record.Reason = row["Reason"].ToString();
                    record.DrillRig = row["DrillRig"].ToString();
                    record.DrillBitDiameter = row["DrillBitDiameter"].ToString();
                    record.Operator = row["Operator"].ToString();
                    record.DryWet = row["DryWet"].ToString();
                    record.ShiftDate = row["ShiftDate"].ToString();
                    record.Shift = row["Shift"].ToString();
                    record.ShiftCrew = row["ShiftCrew"].ToString();
                    record.ActualX = row["ActualX"].ToString();
                    record.ActualY = row["ActualY"].ToString();
                    record.ActualZ = row["ActualZ"].ToString();
                    record.ActualDepth = row["ActualDepth"].ToString();
                    record.PlannedX = row["PlannedX"].ToString();
                    record.PlannedY = row["PlannedY"].ToString();
                    record.PlannedZ = row["PlannedZ"].ToString();
                    record.PlannedDepth = row["PlannedDepth"].ToString();
                    record.TimeDrilled = row["TimeDrilled"].ToString();
                    record.TotalDrillTime = row["TotalDrillTime"].ToString();

                    StoreData(record, count.ToString(), recordCount.ToString());

                    //Update block Summary
                    cm.UpdateSummaries(record.Pattern,record);

                    lblCounter.Text = "Sending record " + count.ToString() + " of " + recordCount + " to cloud";
                    lblCounter.Refresh();
                    count++;
                }

                lblCounter.Text = "DONE!  (" + count + " records stored in cloud)";
                lblCounter.Refresh();
            }
            else
            {
                lblCounter.Text = "There seems to be a problem with the file.  Please select a different file.";
                lblCounter.Refresh();
            }
        }

        public async void StoreData(RecordModel drillR, string count, string recordCount)
        {
            string jsonToPost = jsHelper.GetHoleJSON(drillR);
            HttpResponseMessage error;

            using (HttpClient httpClient = new HttpClient())
            {
                StringContent httpConent = new StringContent(jsonToPost, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = null;
                try
                {
                    responseMessage = await httpClient.PostAsync("https://rockmacloud.firebaseio.com/block/.json", httpConent);

                }
                catch (Exception ex)
                {
                    if (responseMessage == null)
                    {
                        responseMessage = new HttpResponseMessage();
                    }
                    responseMessage.StatusCode = HttpStatusCode.InternalServerError;

                    lblCounter.Text = "There was an error connecting to the server!  Please check your connection and try again.";
                    lblCounter.Refresh();
                    lblFeedback.Text = "";
                    lblFeedback.Refresh();
                }
                error = responseMessage;
                if (error != null)
                {
                }

            }
        }
    }
}
