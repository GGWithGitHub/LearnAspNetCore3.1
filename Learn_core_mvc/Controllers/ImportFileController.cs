using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class ImportFileController : Controller
    {
        public IActionResult ImportCSV()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostCSV(IFormFile postedFile)
        {
            if (postedFile != null && postedFile.Length > 0)
            {
                DataTable dt = new DataTable();

                //You can create columns in datatable if uploaded file does not contain 
                //dt.Columns.AddRange(new DataColumn[3] { 
                //    new DataColumn("FName", typeof(string)),
                //    new DataColumn("LName", typeof(string)),
                //    new DataColumn("Email",typeof(string)) 
                //});

                using (var stream = new MemoryStream())
                {
                    postedFile.CopyTo(stream);
                    stream.Position = 0;

                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        var csvData = reader.ReadToEnd();
                        bool firstRow = true;
                        foreach (string row in csvData.Split('\n'))
                        {
                            if (!string.IsNullOrEmpty(row))
                            {
                                //Fetching columns name from uploaded file and create columns in datatable
                                if (firstRow)
                                {
                                    foreach (string cell in row.Split(','))
                                    {
                                        dt.Columns.Add(cell.Trim());
                                    }
                                    firstRow = false;
                                }
                                else
                                {
                                    dt.Rows.Add();
                                    int i = 0;
                                    foreach (string cell in row.Split(','))
                                    {
                                        dt.Rows[dt.Rows.Count - 1][i] = cell.Trim();
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (DataRow row in dt.Rows)
                {
                    var fname = row["FName"].ToString();
                    var lname = row["LName"].ToString();
                    var email = row["Email"].ToString();
                }

                return Json(new { success = true, errMsg = "File uploaded." });
            }
            else
            {
                // Handle case when no file is uploaded
                return Json(new { success = false, errMsg = "No file uploaded." });
            }
        }
    }
}
