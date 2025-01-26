using System.ComponentModel.DataAnnotations.Schema;

namespace MusicFest.DAL.Models;

// Database Table Tickets
[Table("Tickets")]
public class Ticket
{
    public int Id { get; set; }
    
    public bool Vip { get; set; }
    public string SeatNumber { get; set; }
    public Customer CustomerId {get; set;}
}