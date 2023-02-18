using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

namespace om.Pages.clients
{
    public class EditModel : PageModel
    {
        public clientInfo clientInfo = new clientInfo();
        public String errormessage = "";
        public String successmessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=DESKTOP-UB3F8HU;Initial Catalog=world;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients where id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                            

                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                            }



                        }
                    }   }
            }
            catch(Exception ex)
            {
                errormessage = ex.Message;
            }
        }
        public void OnPost()
        {
           clientInfo.id= Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];
           
            if (clientInfo.id.Length == 0 || clientInfo.name.Length == 0 ||
              clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 ||
              clientInfo.address.Length == 0)
            {
                errormessage = "all the field are required";
                return;

            }
            try
            {
                
                String connectionString = "Data Source=DESKTOP-UB3F8HU;Initial Catalog=world;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients " +
                    "SET name = @name, email = @email, phone = @phone, address = @address "+
                    "WHERE id=@id ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                          command.Parameters.AddWithValue("@email", clientInfo.email);
                          command.Parameters.AddWithValue("@phone", clientInfo.phone);
                          command.Parameters.AddWithValue("@address", clientInfo.address);
                          command.Parameters.AddWithValue("@id", clientInfo.id);

                        command.ExecuteNonQuery();
                    }
                    
                }
            }
            catch(Exception ex)
            {
                errormessage = ex.Message;
                return;
            }
            Response.Redirect("/clients/Index");
        }
    }
}
