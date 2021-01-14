using FluentAssertions;
using LuizaLabs.Wishlist.App.Interfaces.Wrappers;
using LuizaLabs.Wishlist.App.ResponseModels;
using LuizaLabs.Wishlist.App.Services;
using LuizaLabs.Wishlist.Domain.Repositories.Interfaces;
using LuizaLabs.Wishlist.Tests.Base;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LuizaLabs.Wishlist.Tests.App.Services
{
    public class FavoriteServiceFixture : BaseFixture<FavoriteService>
    {
        public Mock<IFavoriteRepository> mockFavoriteRepository { get; set; }
        public Mock<IProductRepository> mockProductRepository { get; set; }
        public FavoriteResponseMessages messages { get; set; }

        public FavoriteServiceFixture()
        {
            faker = new FakerProvider();
            mockLogger = new Mock<ILoggerWrapper<FavoriteService>>();
            mockFavoriteRepository = new Mock<IFavoriteRepository>();
            mockProductRepository = new Mock<IProductRepository>();
            messages = faker.CreateFakerFavoriteResponseMessages();
            SUT = new FavoriteService(mockLogger.Object, mockFavoriteRepository.Object, mockProductRepository.Object, messages);
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
