using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.IO;

namespace ExtractReport
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        #region Query
        const string qry = @"Select P.Product_Title, SD.Quantity,  (SELECT SUM(Quantity * UnitPrice - Discount) FROM 
                Sales_Details WHERE Sales_Details.ProductID = P.ProductID) As Total_Amount

                FROM Sales AS S, Sales_Details as SD, Products AS P
                WHERE P.ProductID = SD.ProductID ";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            message.Text = "";
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string extention = format.SelectedValue;

            string whereClause = "";

            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime(); 

            if (strtDate.Text.ToString() == "" || EndDate.Text.ToString() == "")
            {
                if (strtDate.Text.ToString() != "")
                {
                    message.Text = @"if start date is selected.
                                        end date must also be selected ";
                    return;
                }
                if (EndDate.Text.ToString() == "")
                {
                    message.Text = @"if end date is selected.
                                        start date must also be selected ";
                    return;
                }
                
            }
            else
            {
                startDate = DateTime.Parse(strtDate.Text.ToString());
                endDate = DateTime.Parse(EndDate.Text.ToString());
                //startDate = DateTime.Parse(strtDate.Text.ToString()).ToShortDateString();
                //endDate = DateTime.Parse(EndDate.Text.ToString()).ToShortDateString();
                whereClause = string.Format(" and SD.saleDate between '{0}' and  '{1}' " , startDate.ToShortDateString()
                    , endDate.ToShortDateString());
            }

            if (startDate > endDate)
            {

                message.Text = "End Date Must be Greater than start Date";
                return;
            }
            
            //db operation
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            StringBuilder sb = new StringBuilder();
            try
            {

                string tempQry = qry + whereClause;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlDataAdapter da = new SqlDataAdapter(tempQry, con);
                    DataSet ds = new DataSet();

                    da.Fill(ds);
                    ds.Tables[0].TableName = "Output";

                    //string productTitle = "";
                    string seperator = extention == ".csv" ? "|" : ",";

                    if (ds.Tables["Output"].Rows.Count <=0)
                    {
                        message.Text = "No Record Found";
                        sb.Append("No Record found" );
                    }
                    else  if (Product_Title.Checked || Quantity.Checked  || Total_Amount.Checked)
                    {
                        if(Product_Title.Checked)
                            sb.Append("Product Title"+ seperator);

                        if (Quantity.Checked)
                            sb.Append("Quantity" + seperator);

                        if (Total_Amount.Checked)
                            sb.Append("Total Amount" + seperator);

                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("\r\n");

                        foreach (DataRow dataRow in ds.Tables["Output"].Rows)
                        {
                            //productTitle = dataRow["Product_Title"].ToString();
                            if (Product_Title.Checked)
                                sb.Append(dataRow["Product_Title"].ToString() + seperator);

                            if (Quantity.Checked)
                                sb.Append(dataRow["Quantity"].ToString() + seperator);

                            if (Total_Amount.Checked)
                                sb.Append(dataRow["Total_Amount"].ToString() + seperator);

                            sb.Remove(sb.Length - 1, 1);
                            sb.Append("\r\n");
                        }

                    }
                }
                string filePath = @"C:\Users\Admin\OneDrive\Desktop\ExportedDataTest\data" + extention;
                StreamWriter file = new StreamWriter(filePath);
                file.WriteLine(sb.ToString());
                file.Close();
                message.Text = "Success";
            }
            catch (Exception ex)
            {
                message.Text = "Failure " + ex.Message.ToString() + " / " + ex.InnerException.ToString();
            }
        }
    }
}
/*
 notes get make a sql class for db operation 
 concatinaion of query col based on frontend options 
 seperate teh ADO file from this file for better
 add date in query and filter and make a check for start and end data
*/
