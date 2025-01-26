using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using namespace MusicFest.DAL.Data.TicketManagementDatabaseContext

namespace YourProject.Tests
{
    public class GroupRepositoryTests
    {
        private readonly DbContextOptions<TicketManagementDatabaseContext> _options;

        public GroupRepositoryTests()
        {
            // Set up in-memory database options
            _options = new DbContextOptionsBuilder<TicketManagementDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task GetAllGroups_ReturnsAllGroups()
        {
            // Arrange
            using (var context = new TicketManagementDatabaseContext(_options))
            {
                context.Groups.Add(new Group { Id = 1, Name = "Group 1" });
                context.Groups.Add(new Group { Id = 2, Name = "Group 2" });
                context.SaveChanges();
            }

            // Act
            List<Group> groups;
            using (var context = new TicketManagementDatabaseContext(_options))
            {
                var repository = new GroupRepository(context);
                groups = await repository.GetAllGroupsAsync(); // Assuming you have this method
            }

            // Assert
            Assert.Equal(2, groups.Count);
            Assert.Contains(groups, g => g.Name == "Group 1");
            Assert.Contains(groups, g => g.Name == "Group 2");
        }
    }
}