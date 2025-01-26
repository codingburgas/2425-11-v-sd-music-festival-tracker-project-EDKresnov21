using System.ComponentModel.DataAnnotations.Schema;

namespace MusicFest.DAL.Models;

// Database Table Customers
[Table("Customers")]
public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}