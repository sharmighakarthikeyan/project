using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace om.Pages.clients
{
    public class IndexModel : PageModel
    {
        public List<clientInfo> listclients = new List<clientInfo>();

        public void OnGet()
        {
            try
            {
                 String connectionString = "Data Source=DESKTOP-UB3F8HU;Initial Catalog=world;Integrated Security=True";
                 using(SqlConnection connection=new SqlConnection(connectionString))
                 {
                     connection.Open();
                     String sql = "select * from clients";
                     using(SqlCommand command=new SqlCommand(sql,connection))
                     {
                         using(SqlDataReader reader=command.ExecuteReader())
                         {
                             while(reader.Read())
                             {
                                 clientInfo clientInfo = new clientInfo();

                                 clientInfo.id = "" + reader.GetInt32(0);
                                 clientInfo.name = reader.GetString(1);
                                 clientInfo.email =reader.GetString(2);
                                 clientInfo.phone=reader.GetString(3);
                                 clientInfo.address =reader.GetString(4);
                                 clientInfo.created_at =reader.GetDateTime(5).ToString();

                                 listclients.Add(clientInfo);
                             }
                         }
                     }
                 }
            
               



            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }
    }
}
public class clientInfo
{
    public String id;
    public String name;
    public String email;
    public String phone;
    public String address;
    public String created_at;
  




}