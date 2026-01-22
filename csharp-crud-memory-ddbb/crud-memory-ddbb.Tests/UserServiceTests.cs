using crud.Configuration;
using crud.Models;
using crud.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace crud_memory_ddbb.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService _service;

        private Mock<UserConfig> _userConfigMock;
        private Mock<IOptions<UserConfig>> _iOptionsMock;
        private DbContextOptions<AppDbContext> _optionsStub;

        [SetUp]
        public void SetUp()
        {
            _userConfigMock = new Mock<UserConfig>();
            _iOptionsMock = new Mock<IOptions<UserConfig>>();

            _optionsStub = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "usersTestingDatabase")
                .Options;

            _iOptionsMock.Setup(s => s.Value).Returns(_userConfigMock.Object);
        }

        [Test]
        public async Task UserService_GetUsers_ReturnsAllUsers()
        {
            // we need to insert seed data for each test
            using (var context = new AppDbContext(_optionsStub))
            {
                // given
                context.Users.Add(new User { Id = 1, Email = "test-email-1@gmail.com", Name = "Test-User-1" });
                context.Users.Add(new User { Id = 2, Email = "test-email-2@gmail.com", Name = "Test-User-2" });
                context.Users.Add(new User { Id = 3, Email = "test-email-3@gmail.com", Name = "Test-User-3" });
                context.SaveChanges();
                _service = new UserService(context, _iOptionsMock.Object);

                // when
                var users = await _service.GetUsers();

                // then
                users.Result.Should().NotBeNullOrEmpty();
                users.Result.Should().HaveCount(3);
            }
        }
    }
}
