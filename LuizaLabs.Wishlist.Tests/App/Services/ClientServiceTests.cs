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
    public class ClientServiceTests
    {
        [Fact]
        public void InsertClientAsync_ReturnCorrectlyOnSuccess()
        {
            var fixture = new ClientServiceFixture();

            fixture.mockRepository.Setup(m => m.Insert(It.IsAny<Client>())).ReturnsAsync(1);

            var viewModel = fixture.faker.CreateFakerClientViewModel();

            var receivedResult = fixture.SUT.InsertClientAsync(viewModel).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(new Client(viewModel));

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt.Excluding(c => c.data.ClientId));
        }

        [Fact]
        public void InsertClientAsync_ReturnNoClientChanged()
        {
            var fixture = new ClientServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.Conflict);
            var errorInfoList = new List<ErrorInfo> { errorInfo };

            fixture.mockRepository.Setup(m => m.Insert(It.IsAny<Client>())).ReturnsAsync(0);
            fixture.messages.NoClientChanged = errorInfo.message;
            fixture.messages.NoClientChangedDescription = errorInfo.details;

            var viewModel = fixture.faker.CreateFakerClientViewModel();

            var receivedResult = fixture.SUT.InsertClientAsync(viewModel).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void InsertClientAsync_ThrowsExceptionEmailAlreadyInUse()
        {
            var fixture = new ClientServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.Conflict);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var exception = new Exception("Error", new Exception { HResult = -2146233088 });

            fixture.mockRepository.Setup(m => m.Insert(It.IsAny<Client>())).Throws(exception);
            fixture.messages.EmailAlreadyUsed = errorInfo.message;
            fixture.messages.EmailAlreadyUsedDescription = errorInfo.details;

            var viewModel = fixture.faker.CreateFakerClientViewModel();

            var receivedResult = fixture.SUT.InsertClientAsync(viewModel).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void InsertClientAsync_ThrowsExceptionInternalError()
        {
            var fixture = new ClientServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.InternalServerError);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var exception = new Exception("Error");
            exception.HResult = 0;

            fixture.mockRepository.Setup(m => m.Insert(It.IsAny<Client>())).Throws(exception);
            fixture.messages.InternalError = errorInfo.message;
            fixture.messages.InternalErrorDescription = errorInfo.details;

            var viewModel = fixture.faker.CreateFakerClientViewModel();

            var receivedResult = fixture.SUT.InsertClientAsync(viewModel).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void DeleteClientAsync_ReturnCorrectlyOnSuccess()
        {
            var fixture = new ClientServiceFixture();
            var viewModel = fixture.faker.CreateFakerClientViewModel();
            var client = new Client(viewModel);

            fixture.mockRepository.Setup(m => m.Delete(It.IsAny<Client>())).ReturnsAsync(1);
            fixture.mockRepository.Setup(m => m.GetOne(It.IsAny<Guid>())).ReturnsAsync(client);

            var receivedResult = fixture.SUT.DeleteClientAsync(Guid.NewGuid()).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(new Client(viewModel));

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt.Excluding(c => c.data.ClientId));
        }

        [Fact]
        public void DeleteClientAsync_ReturnNoClientChanged()
        {
            var fixture = new ClientServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.Conflict);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var viewModel = fixture.faker.CreateFakerClientViewModel();
            var client = new Client(viewModel);

            fixture.mockRepository.Setup(m => m.Delete(It.IsAny<Client>())).ReturnsAsync(0);
            fixture.mockRepository.Setup(m => m.GetOne(It.IsAny<Guid>())).ReturnsAsync(client);
            fixture.messages.NoClientChanged = errorInfo.message;
            fixture.messages.NoClientChangedDescription = errorInfo.details;

            var receivedResult = fixture.SUT.DeleteClientAsync(Guid.NewGuid()).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void DeleteClientAsync_ThrowsExceptionInternalError()
        {
            var fixture = new ClientServiceFixture();
            var viewModel = fixture.faker.CreateFakerClientViewModel();
            var client = new Client(viewModel);
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.InternalServerError);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var exception = new Exception("Error");

            fixture.mockRepository.Setup(m => m.Delete(It.IsAny<Client>())).Throws(exception);
            fixture.mockRepository.Setup(m => m.GetOne(It.IsAny<Guid>())).ReturnsAsync(client);
            fixture.messages.InternalError = errorInfo.message;
            fixture.messages.InternalErrorDescription = errorInfo.details;

            var receivedResult = fixture.SUT.DeleteClientAsync(Guid.NewGuid()).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void DeleteClientAsync_ReturnClientNotFound()
        {
            var fixture = new ClientServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.NotFound);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var viewModel = fixture.faker.CreateFakerClientViewModel();
            var client = new Client(viewModel);

            fixture.mockRepository.Setup(m => m.Delete(It.IsAny<Client>())).ReturnsAsync(0);
            fixture.mockRepository.Setup(m => m.GetOne(It.IsAny<Guid>())).ReturnsAsync(value: null);
            fixture.messages.ClientNotFound = errorInfo.message;
            fixture.messages.ClientNotFoundDescription = errorInfo.details;

            var receivedResult = fixture.SUT.DeleteClientAsync(Guid.NewGuid()).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void GetClientAsync_ReturnCorrectlyOnSuccess()
        {
            var fixture = new ClientServiceFixture();
            var viewModel = fixture.faker.CreateFakerClientViewModel();
            var client = new Client(viewModel);

            fixture.mockRepository.Setup(m => m.GetOne(It.IsAny<Guid>())).ReturnsAsync(client);

            var receivedResult = fixture.SUT.GetClientAsync(Guid.NewGuid()).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(new Client(viewModel));

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt.Excluding(c => c.data.ClientId));
        }

        [Fact]
        public void GetClientAsync_ReturnClientNotFound()
        {
            var fixture = new ClientServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.NotFound);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var viewModel = fixture.faker.CreateFakerClientViewModel();
            var client = new Client(viewModel);
            fixture.mockRepository.Setup(m => m.GetOne(It.IsAny<Guid>())).ReturnsAsync(value: null);
            fixture.messages.ClientNotFound = errorInfo.message;
            fixture.messages.ClientNotFoundDescription = errorInfo.details;

            var receivedResult = fixture.SUT.GetClientAsync(Guid.NewGuid()).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void GetClientAsync_ThrowsExceptionInternalError()
        {
            var fixture = new ClientServiceFixture();
            var viewModel = fixture.faker.CreateFakerClientViewModel();
            var client = new Client(viewModel);
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.InternalServerError);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var exception = new Exception("Error");

            fixture.mockRepository.Setup(m => m.GetOne(It.IsAny<Guid>())).Throws(exception);
            fixture.messages.InternalError = errorInfo.message;
            fixture.messages.InternalErrorDescription = errorInfo.details;

            var receivedResult = fixture.SUT.GetClientAsync(Guid.NewGuid()).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void UpdateClientAsync_ReturnCorrectlyOnSuccess()
        {
            var fixture = new ClientServiceFixture();
            var viewModel = fixture.faker.CreateFakerClientViewModel();
            var client = new Client();

            fixture.mockRepository.Setup(m => m.GetOne(It.IsAny<Guid>())).ReturnsAsync(client);
            fixture.mockRepository.Setup(m => m.Update(It.IsAny<Client>())).ReturnsAsync(1);

            var receivedResult = fixture.SUT.UpdateClientAsync(Guid.NewGuid(), viewModel).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(new Client(viewModel));

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt.Excluding(c => c.data.ClientId));
        }

        [Fact]
        public void UpdateClientAsync_ReturnNoClientChanged()
        {
            var fixture = new ClientServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.Conflict);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var viewModel = fixture.faker.CreateFakerClientViewModel();
            var client = new Client();

            fixture.mockRepository.Setup(m => m.Update(It.IsAny<Client>())).ReturnsAsync(0);
            fixture.mockRepository.Setup(m => m.GetOne(It.IsAny<Guid>())).ReturnsAsync(client);
            fixture.messages.NoClientChanged = errorInfo.message;
            fixture.messages.NoClientChangedDescription = errorInfo.details;

            var receivedResult = fixture.SUT.UpdateClientAsync(Guid.NewGuid(), viewModel).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void UpdateClientAsync_ReturnClientNotFound()
        {
            var fixture = new ClientServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.NotFound);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var viewModel = fixture.faker.CreateFakerClientViewModel();
            var client = new Client(viewModel);

            fixture.mockRepository.Setup(m => m.Update(It.IsAny<Client>())).ReturnsAsync(0);
            fixture.mockRepository.Setup(m => m.GetOne(It.IsAny<Guid>())).ReturnsAsync(value: null);
            fixture.messages.ClientNotFound = errorInfo.message;
            fixture.messages.ClientNotFoundDescription = errorInfo.details;

            var receivedResult = fixture.SUT.UpdateClientAsync(Guid.NewGuid(), viewModel).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void UpdateClientAsync_ThrowsExceptionInternalError()
        {
            var fixture = new ClientServiceFixture();
            var viewModel = fixture.faker.CreateFakerClientViewModel();
            var client = new Client(viewModel);
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.InternalServerError);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var exception = new Exception("Error");

            fixture.mockRepository.Setup(m => m.Update(It.IsAny<Client>())).Throws(exception);
            fixture.mockRepository.Setup(m => m.GetOne(It.IsAny<Guid>())).ReturnsAsync(client);
            fixture.messages.InternalError = errorInfo.message;
            fixture.messages.InternalErrorDescription = errorInfo.details;

            var receivedResult = fixture.SUT.UpdateClientAsync(Guid.NewGuid(), viewModel).GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<Client>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void GetAllClientsAsync_ReturnCorrectlyOnSuccess()
        {
            var fixture = new ClientServiceFixture();
            var clients = fixture.faker.CreateFakerClientList();

            fixture.mockRepository.Setup(m => m.GetAll()).ReturnsAsync(clients);

            var receivedResult = fixture.SUT.GetAllClientsAsync().GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<List<Client>>(clients);

            receivedResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetAllClientsAsync_ReturnClientsNotFound()
        {
            var fixture = new ClientServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.NotFound);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var clients = new List<Client>();

            fixture.mockRepository.Setup(m => m.GetAll()).ReturnsAsync(clients);
            fixture.messages.ClientNotFound = errorInfo.message;
            fixture.messages.ClientNotFoundDescription = errorInfo.details;

            var receivedResult = fixture.SUT.GetAllClientsAsync().GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<List<Client>>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }

        [Fact]
        public void GetAllClientsAsync_ThrowsExceptionInternalError()
        {
            var fixture = new ClientServiceFixture();
            var errorInfo = fixture.faker.CreateFakerErrorInfo(HttpStatusCode.InternalServerError);
            var errorInfoList = new List<ErrorInfo> { errorInfo };
            var clients = new List<Client>();
            var exception = new Exception("Error");

            fixture.mockRepository.Setup(m => m.GetAll()).Throws(exception);
            fixture.messages.InternalError = errorInfo.message;
            fixture.messages.InternalErrorDescription = errorInfo.details;

            var receivedResult = fixture.SUT.GetAllClientsAsync().GetAwaiter().GetResult();
            var expectedResult = new ResponseModel<List<Client>>(true, errorInfoList);

            receivedResult.Should().BeEquivalentTo(expectedResult, opt => opt
                .Using<ErrorInfo>(ei => ei.Subject.Should()
                .BeEquivalentTo(ei.Expectation, o => o.Excluding(e => e.errorId)))
                .WhenTypeIs<ErrorInfo>());
        }
    }
}
