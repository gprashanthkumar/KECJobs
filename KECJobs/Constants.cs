using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web;

namespace KECJobs
{
    public static class Constants
    {

        public static string EncodeTo64(string _data)
        {
            string returnValue = string.Empty;
            try
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_data);
                returnValue = System.Convert.ToBase64String(plainTextBytes);

            }
            catch (Exception)
            {
            }

            return returnValue;
        }

        public static string DecodeFrom64(string encodedData)
        {
            string returnValue = string.Empty;
            try
            {
                byte[] encodedDataAsBytes = Convert.FromBase64String(encodedData);
                returnValue = System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);

            }
            catch (Exception)
            {
                //string msg = "Some Error Occured when processing the request.";
            }
            return returnValue;
        }



        public static DataTable ToDataTable<T>(List<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            System.Reflection.PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (var i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);

            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static void ExporttoExcel(DataTable dtable)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
            //am getting my grid's column headers
            //int columnscount = GridView_Result.Columns.Count;
            int columnscount = dtable.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(dtable.Columns[j].ColumnName);
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in dtable.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < dtable.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        public static class SessionVariables
        {
            public const string UserID = "UserID";

            //public const string FullName = "FullName";
            public const string Name = "Name";
            public const string EmailId = "EmailId";

            public const string RoleId = "RoleId";
            public const string RoleName = "RoleName";
            public const string isAdmin = "isAdmin";
            public const string isJobsEditor = "isJobsEditor";
            public const string isRegistrationEditor = "isRegistrationEditor";
            public const string isReferenceEditor = "isReferenceEditor";
            public const string isSkillDevelopmentEditor = "isSkillDevelopmentEditor";
            public const string isGuest = "isGuest";





            //public const string UserName = "UserName";

        }

    }
}