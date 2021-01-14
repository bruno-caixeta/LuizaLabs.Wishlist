using FluentAssertions;
using LuizaLabs.Wishlist.App.Interfaces.Wrappers;
using LuizaLabs.Wishlist.App.ResponseModels;
using LuizaLabs.Wishlist.App.Services;
using LuizaLabs.Wishlist.Domain.Entities;
using LuizaLabs.Wishlist.Domain.Repositories.Interfaces;
using LuizaLabs.Wishlist.Tests.Base;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LuizaLabs.Wishlist.Tests.App.Services
{
    public class ClientServiceFixture : BaseFixture<ClientService>
    {
        public Mock<IClientRepository> mockRepository { get; set; }
        public ClientResponseMessages messages { get; set; }

        public ClientServiceFixture()
        {
            faker = new FakerProvider();
            mockLogger = new Mock<ILoggerWrapper<ClientService>>();
            mockRepository = new Mock<IClientRepository>();
            messages = faker.CreateFakerClientResponseMessages();
            SUT = new ClientService(mockLogger.Object, mockRepository.Object, messages);
        }

        [Fact]
        public void FixtureConstructor_OK()
        {
            var fixture = new ClientServiceFixture();

            fixture.Should().NotBeNull();
            fixture.mockLogger.Should().NotBeNull();
            fixture.mockRepository.Should().NotBeNull();
            fixture.SUT.Should().NotBeNull();
            fixture.faker.Should().NotBeNull();
        }
    }
}
