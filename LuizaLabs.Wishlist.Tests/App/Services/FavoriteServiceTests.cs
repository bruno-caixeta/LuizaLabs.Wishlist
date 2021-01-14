using FluentAssertions;
using LuizaLabs.Wishlist.App.ResponseModels;
using LuizaLabs.Wishlist.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace LuizaLabs.Wishlist.Tests.App.Services
{
    public class FavoriteServiceTests
    {
        [Fact]
        public void InsertFavoriteAsync_ReturnCorrectlyOnSuccess()
        {
            var fixture = new FavoriteServiceFixture();
            var viewModel = fixture.faker.CreateFakerFavoriteViewModel();
            var product = fixture.faker.CreateFakerProduct();

            fixture.mockFavoriteRepository.Setup(m => m.Insert(It.IsAny<Favorite>())).ReturnsAsync(1);
            fixture.mockProductRepository.Setup(p => p.GetProduct(It.IsAny<Guid>())).ReturnsAsync(product);

            var receivedResult = fixture.SUT.InsertFavoriteAsync(viewModel).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Favorite>(new Favorite(viewModel));

            receivedResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void InsertFavoriteAsync_ProductDoesNotExist()
        {
            var fixture = new FavoriteServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.NotFound);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var viewModel = fixture.faker.CreateFakerFavoriteViewModel();

            fixture.mockFavoriteRepository.Setup(m => m.Insert(It.IsAny<Favorite>())).ReturnsAsync(1);
            fixture.mockProductRepository.Setup(p => p.GetProduct(It.IsAny<Guid>())).ReturnsAsync(value: null);
            fixture.messages.ProductDoesNotExist = errorInfo.message;
            fixture.messages.ProductDoesNotExistDescription = errorInfo.details;

            var receivedResult = fixture.SUT.InsertFavoriteAsync(viewModel).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Favorite>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void InsertfavoriteAsync_ReturnNoFavoriteChanged()
        {
            var fixture = new FavoriteServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.Conflict);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var viewModel = fixture.faker.CreateFakerFavoriteViewModel();
            var product = fixture.faker.CreateFakerProduct();

            fixture.mockFavoriteRepository.Setup(m => m.Insert(It.IsAny<Favorite>())).ReturnsAsync(0);
            fixture.mockProductRepository.Setup(p => p.GetProduct(It.IsAny<Guid>())).ReturnsAsync(product);
            fixture.messages.NoFavoriteChanged = errorInfo.message;
            fixture.messages.NoFavoriteChangedDescription = errorInfo.details;

            var receivedResult = fixture.SUT.InsertFavoriteAsync(viewModel).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void InsertClientAsync_ThrowsExceptionProductAlreadyInFavorites()
        {
            var fixture = new FavoriteServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.Conflict);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var exception = new Exception("Error", new Exception { HResult = -2146233088 });
            var viewModel = fixture.faker.CreateFakerFavoriteViewModel();
            var product = fixture.faker.CreateFakerProduct();

            fixture.mockFavoriteRepository.Setup(m => m.Insert(It.IsAny<Favorite>())).Throws(exception);
            fixture.mockProductRepository.Setup(p => p.GetProduct(It.IsAny<Guid>())).ReturnsAsync(product);
            fixture.messages.ProductAlreadyInFavorites = errorInfo.message;
            fixture.messages.ProductAlreadyInFavoritesDescription = errorInfo.details;

            var receivedResult = fixture.SUT.InsertFavoriteAsync(viewModel).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void InsertClientAsync_ThrowsExceptionInternalError()
        {
            var fixture = new FavoriteServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.InternalServerError);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var exception = new Exception("Error");
            exception.HResult = 0;
            var viewModel = fixture.faker.CreateFakerFavoriteViewModel();
            var product = fixture.faker.CreateFakerProduct();

            fixture.mockFavoriteRepository.Setup(m => m.Insert(It.IsAny<Favorite>())).Throws(exception);
            fixture.mockProductRepository.Setup(p => p.GetProduct(It.IsAny<Guid>())).ReturnsAsync(product);
            fixture.messages.InternalError = errorInfo.message;
            fixture.messages.InternalErrorDescription = errorInfo.details;

            var receivedResult = fixture.SUT.InsertFavoriteAsync(viewModel).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void DeleteFavoriteAsync_ReturnCorrectlyOnSuccess()
        {
            var fixture = new FavoriteServiceFixture();
            var viewModel = fixture.faker.CreateFakerFavoriteViewModel();
            var favorite = new Favorite(viewModel);

            fixture.mockFavoriteRepository.Setup(m => m.Delete(It.IsAny<Favorite>())).ReturnsAsync(1);
            fixture.mockFavoriteRepository.Setup(m => m.GetFavorite(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(favorite);

            var receivedResult = fixture.SUT.DeleteFavoriteAsync(viewModel.ClientId, viewModel.ProductId).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Favorite>(new Favorite(viewModel));

            receivedResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void DeleteFavoriteAsync_NoFavoriteFound()
        {
            var fixture = new FavoriteServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.NotFound);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var viewModel = fixture.faker.CreateFakerFavoriteViewModel();

            fixture.mockFavoriteRepository.Setup(m => m.Delete(It.IsAny<Favorite>())).ReturnsAsync(1);
            fixture.mockFavoriteRepository.Setup(p => p.GetFavorite(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(value: null);
            fixture.messages.NoFavoriteFound = errorInfo.message;
            fixture.messages.NoFavoriteFoundDescription = errorInfo.details;

            var receivedResult = fixture.SUT.DeleteFavoriteAsync(viewModel.ClientId, viewModel.ProductId).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Favorite>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void DeleteFavoriteAsync_NoFavoriteChanged()
        {
            var fixture = new FavoriteServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.Conflict);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var viewModel = fixture.faker.CreateFakerFavoriteViewModel();
            var favorite = new Favorite(viewModel);

            fixture.mockFavoriteRepository.Setup(m => m.Delete(It.IsAny<Favorite>())).ReturnsAsync(0);
            fixture.mockFavoriteRepository.Setup(p => p.GetFavorite(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(favorite);
            fixture.messages.NoFavoriteChanged = errorInfo.message;
            fixture.messages.NoFavoriteChangedDescription = errorInfo.details;

            var receivedResult = fixture.SUT.DeleteFavoriteAsync(viewModel.ClientId, viewModel.ProductId).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Favorite>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void DeleteFavoriteAsync_ThrowsExceptionInternalError()
        {
            var fixture = new FavoriteServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.InternalServerError);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var viewModel = fixture.faker.CreateFakerFavoriteViewModel();
            var favorite = new Favorite(viewModel);
            var exception = new Exception("Error");

            fixture.mockFavoriteRepository.Setup(m => m.Delete(It.IsAny<Favorite>())).Throws(exception);
            fixture.mockFavoriteRepository.Setup(p => p.GetFavorite(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(favorite);
            fixture.messages.InternalError = errorInfo.message;
            fixture.messages.InternalErrorDescription = errorInfo.details;

            var receivedResult = fixture.SUT.DeleteFavoriteAsync(viewModel.ClientId, viewModel.ProductId).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Favorite>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void GetAllClientFavoritesAsync_ReturnCorrectlyOnSuccess()
        {
            var fixture = new FavoriteServiceFixture();
            var favorites = fixture.faker.CreateFakerFavoriteList();
            var product = fixture.faker.CreateFakerProduct();            

            fixture.mockFavoriteRepository.Setup(m => m.GetAllClientFavorites(It.IsAny<Guid>())).ReturnsAsync(favorites);
            fixture.mockProductRepository.Setup(m => m.GetProduct(It.IsAny<Guid>())).ReturnsAsync(product);

            foreach (var favorite in favorites)
            {
                favorite.Product = product;
            }

            var receivedResult = fixture.SUT.GetAllClientFavoritesAsync(Guid.NewGuid()).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<List<Favorite>>(favorites);

            receivedResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetAllClientFavoritesAsync_NoFavoritesFound()
        {
            var fixture = new FavoriteServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.NotFound);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var favorites = new List<Favorite>();
            var product = fixture.faker.CreateFakerProduct();


            fixture.mockFavoriteRepository.Setup(m => m.GetAllClientFavorites(It.IsAny<Guid>())).ReturnsAsync(favorites);
            fixture.mockProductRepository.Setup(m => m.GetProduct(It.IsAny<Guid>())).ReturnsAsync(product);
            fixture.messages.NoFavoritesFound = errorInfo.message;
            fixture.messages.NoFavoritesFoundDescription = errorInfo.details;

            var receivedResult = fixture.SUT.GetAllClientFavoritesAsync(Guid.NewGuid()).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<List<Favorite>>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void GetAllClientFavoritesAsync_ThrowsExceptionInternalError()
        {
            var fixture = new FavoriteServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.InternalServerError);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var favorites = new List<Favorite>();
            var product = fixture.faker.CreateFakerProduct();
            var exception = new Exception("Error");


            fixture.mockFavoriteRepository.Setup(m => m.GetAllClientFavorites(It.IsAny<Guid>())).Throws(exception);
            fixture.messages.InternalError = errorInfo.message;
            fixture.messages.InternalErrorDescription = errorInfo.details;

            var receivedResult = fixture.SUT.GetAllClientFavoritesAsync(Guid.NewGuid()).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<List<Favorite>>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }
    }
}
