using System.ComponentModel.DataAnnotations.Schema;

namespace ExampleWebApplication.DAL.Models;

// Database Table Planes
[Table("Groups")]
public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NumberOfMusicians { get; set; }
    public string Song { get; set; }


    public static void Add(Group group)
    {
        throw new NotImplementedException();
    }
}