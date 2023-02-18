using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace om.Pages.clients
{
    public class createModel : PageModel
    {
        public clientInfo clientInfo = new clientInfo();
        public String errormessage = "";
        public String successmessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];


            if (clientInfo.name.Length == 0 ||
                clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 ||
                clientInfo.address.Length == 0)
            {
                errormessage = "all the field are required";
                return;

            }
            //save the new client into database

            try
            {
                String connectionString = "Data Source=DESKTOP-UB3F8HU;Initial Catalog=world;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO CLIENTS"+"(name,email,phone,address)values"+"(@name,@email,@phone,@address);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                     
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errormessage = ex.Message;
                return;
            }

            clientInfo.name = "";
            clientInfo.email ="";
            clientInfo.phone ="";
            clientInfo.address ="";
            successmessage = "new client added";
            Response.Redirect("/clients/Index");
        }
    }
}
