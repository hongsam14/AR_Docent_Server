using AR_Docent.website.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AR_Docent.website.Pages.Products
{
    public class IndexModel : PageModel
    {
        private static readonly string connectionString = "Server = tcp:ar-docent-server.database.windows.net,1433;Initial Catalog = AR_Docent_Data; Persist Security Info=False;User ID = admin_; Password=1q2w3e4r!; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";
        public List<ProductInfo> ProductInfos { get; private set; } = new List<ProductInfo>();
        public void OnGet()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string _sqlQuery = "SELECT * FROM product";
                    using (SqlCommand cmd = new SqlCommand(_sqlQuery, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductInfo productInfo = new ProductInfo();
                                productInfo.id = reader.GetInt32(0);
                                productInfo.title = reader.GetString(1);
                                productInfo.name = reader.GetString(2);
                                productInfo.created_at = reader.GetDateTime(3).ToString();
                                productInfo.content = reader.GetString(4);

                                ProductInfos.Add(productInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
