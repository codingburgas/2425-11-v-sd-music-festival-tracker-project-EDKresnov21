using ExampleWebApplication.DAL.Data;
using ExampleWebApplication.DAL.Models;
using Microsoft.Data.SqlClient;

namespace ExampleWebApplication.DAL.Repositories;

public class GroupRepository : TicketManagementDatabaseContext
{
    private TicketManagementDatabaseContext _context = new TicketManagementDatabaseContext();
    
    // CREATE 
    public void CreateGroup(Group newGroup)
    {
        _context.Groups.Add(newGroup);

        SqlCommand command = new SqlCommand($"INSERT INTO [Groups](Id,[Name],[NumberOfMusicians],[Song]) VALUES ({newGroup.Id},\'{newGroup.Name}\',\'{newGroup.NumberOfMusicians}\',\'{newGroup.Song}\')", _context.Connection);
        
        SqlDataAdapter adapter = new SqlDataAdapter();
        
        adapter.InsertCommand = command;
        
        adapter.InsertCommand.ExecuteNonQuery();
    }
    
    // UPDATE
    public void UpdateGroup(Group updatedGroup)
    {
        Group group = _context.Groups.Where(x=>x.Id == updatedGroup.Id).FirstOrDefault();
        
        group.Name = updatedGroup.Name;
        group.NumberOfMusicians = updatedGroup.NumberOfMusicians;
        group.Song = updatedGroup.Song;
        
        _context.Groups.Remove(group);
        _context.Groups.Add(updatedGroup);
        
        SqlCommand command = new SqlCommand($"UPDATE [Groups] SET [Id] = {updatedGroup.Id}, [Name] = {updatedGroup.Name}, [NumberOfMusicians] = {updatedGroup.NumberOfMusicians}, [Song] = {updatedGroup.Song} WHERE [Id] = {updatedGroup.Id}");
        
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        
        adapter.UpdateCommand.ExecuteNonQuery();
    }
    
    // DELETE
    public void DeleteGroup(Group deletedGroup)
    {
        _context.Groups.Remove(deletedGroup);
        
        SqlCommand command = new SqlCommand($"DELETE FROM [Groups] WHERE [Id] = {deletedGroup.Id}");
        
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        
        adapter.DeleteCommand.ExecuteNonQuery();
    }
}