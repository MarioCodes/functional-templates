using crud.Configuration;
using crud.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace crud_memory_ddbb.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        public IUserService _service;

        private Mock<ApplicationDbContext> _dbContextMock;
        private Mock<UserConfig> _userConfigMock;
        private Mock<IOptions<UserConfig>> _iOptionsMock;

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var context = new Mock<ApplicationDbContext>(optionsBuilder.Options);

            _dbContextMock = new Mock<ApplicationDbContext>();
            _userConfigMock = new Mock<UserConfig>();
            _iOptionsMock = new Mock<IOptions<UserConfig>>();
            _service = new UserService(context.Object, _iOptionsMock.Object);
        }

        [Test]
        public async Task UserService_GetUsers_ReturnsAllUsers()
        {
            // given 


            _iOptionsMock.Setup(s => s.Value).Returns(_userConfigMock.Object);

            // when
            string test = await _service.GetSomething();

            // then 
            Assert.That(test, Is.Not.Null);
            Assert.That(test, Is.Not.Empty);
            Assert.That(test, Is.EqualTo("this is a return value"));
        }
    }
}
