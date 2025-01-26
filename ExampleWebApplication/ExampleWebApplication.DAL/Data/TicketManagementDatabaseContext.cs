using ExampleWebApplication.DAL.Models;
using Microsoft.Data.SqlClient;

namespace ExampleWebApplication.DAL.Data;

public class TicketManagementDatabaseContext
{
    public SqlConnection Connection { get; } = null;
    public List<Ticket> Tickets { get; set; }
    public List<Customer> Customers { get; set; }
    public List<Group> Groups { get; set; }
    public TicketManagementDatabaseContext()
    {
        string connectionString = "Server=localhost\\SQLEXPRESS;Database=TicketManagementDatabase;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

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
            Group.Add(new Group()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                NumberOfMusicians = Convert.ToString(reader["Make"]),
                Song = Convert.ToString(reader["Model"]),
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
                Age = Convert.ToInt32(reader["Age"]),
                Country = Convert.ToString(reader["Country"]),
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
                From = Convert.ToString(reader["From"]),
                To = Convert.ToString(reader["To"]),
                DepartureTime = Convert.ToDateTime(reader["DepartureTime"]),
                ArrivalTime = Convert.ToDateTime(reader["ArrivalTime"]),
                SeatNumber = Convert.ToString(reader["SeatNumber"]),
                
                GroupId = Groups.Where(x=> x.Id == Convert.ToInt32(reader["GroupId"])).FirstOrDefault(),
                
                CustomerId = Customers.Where(x => x.Id == Convert.ToInt32(reader["CustomerId"])).FirstOrDefault(),
            });
        }
        
        reader.Close();
    }
}