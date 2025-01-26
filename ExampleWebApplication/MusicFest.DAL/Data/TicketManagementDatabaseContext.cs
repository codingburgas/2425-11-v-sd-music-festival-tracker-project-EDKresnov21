using Microsoft.Data.SqlClient;
using MusicFest.DAL.Models;

namespace MusicFest.DAL.Data;

public class TicketManagementDatabaseContext
{
    public SqlConnection Connection { get; } = null;
    public List<Ticket> Tickets { get; set; }
    public List<Customer> Customers { get; set; }
    public List<Group> Groups { get; set; }
    public TicketManagementDatabaseContext()
    {
        string connectionString = "Server=tcp:edkserver.database.windows.net,1433;Initial Catalog=MFEDK;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";

        if (Connection == null)
        {
            Connection = new SqlConnection(connectionString);
            
            Tickets = new List<Ticket>();
            Customers = new List<Customer>();
            Groups = new List<Group>();
            
            Connection.Open();
            
            ReadGroups();
            ReadCustomers();
            ReadTickets();
        }
    }

    public void ReadGroups()
    {
        SqlCommand command = new SqlCommand("SELECT * FROM [Groups]", Connection);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Groups.Add(new Group()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                NumberOfMusicians = Convert.ToString(reader["NumberOfMusicians"]),
                Song = Convert.ToString(reader["Song"]),
            });
        }
        
        reader.Close();
    }
    
    public void ReadCustomers()
    {
        SqlCommand command = new SqlCommand("SELECT * FROM [Customers]", Connection);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Customers.Add(new Customer()
            {
                Id = Convert.ToInt32(reader["Id"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                LastName = Convert.ToString(reader["LastName"]),
            });
        }
        
        reader.Close();
    }
    
    public void ReadTickets()
    {
        SqlCommand command = new SqlCommand("SELECT * FROM [Tickets]", Connection);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Tickets.Add(new Ticket()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Vip = Convert.ToBoolean(reader["VIP"]),
                SeatNumber = Convert.ToString(reader["SeatNumber"]),
                CustomerId = Customers.Where(x => x.Id == Convert.ToInt32(reader["CustomerId"])).FirstOrDefault(),
            });
        }
        
        reader.Close();
    }
}