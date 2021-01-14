using Bogus;
using LuizaLabs.Wishlist.App.ResponseModels;
using LuizaLabs.Wishlist.Domain.Entities;
using LuizaLabs.Wishlist.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LuizaLabs.Wishlist.Tests.Base
{
    public class FakerProvider
    {
        public readonly int[] STATUS_CODE_LIST = { 100, 101, 201, 203, 204, 205, 206, 300, 301, 302, 303, 304, 305, 306, 307, 400, 401, 402, 403, 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 415, 416, 417, 426, 500, 501, 502, 503, 504, 505 };

        public Faker genericFaker { get; set; }
        public FakerProvider()
        {
            genericFaker = new Faker();
        }

        public ClientViewModel CreateFakerClientViewModel()
        {
            var fakerViewModel = new Faker<ClientViewModel>()
                .CustomInstantiator(c => new ClientViewModel())
                .RuleFor(c => c.Name, o => o.Name.FirstName())
                .RuleFor(c => c.Email, o => o.Person.Email);
            return fakerViewModel.Generate();
        }

        public Client CreateFakerClient()
        {
            var fakerViewModel = new Faker<Client>()
                .CustomInstantiator(c => new Client())
                .RuleFor(c => c.Name, o => o.Name.FirstName())
                .RuleFor(c => c.Email, o => o.Person.Email)
                .RuleFor(c => c.ClientId, o => o.Random.Guid());
            return fakerViewModel.Generate();
        }

        public List<Client> CreateFakerClientList()
        {
            var fakerViewModel = new Faker<Client>()
                .CustomInstantiator(c => new Client())
                .RuleFor(c => c.Name, o => o.Name.FirstName())
                .RuleFor(c => c.Email, o => o.Person.Email)
                .RuleFor(c => c.ClientId, o => o.Random.Guid());
            return fakerViewModel.Generate(genericFaker.Random.Int(1, 10));
        }

        public FavoriteViewModel CreateFakerFavoriteViewModel()
        {
            var fakerViewModel = new Faker<FavoriteViewModel>()
                .CustomInstantiator(c => new FavoriteViewModel())
                .RuleFor(f => f.ClientId, o => o.Random.Guid())
                .RuleFor(f => f.ProductId, o => o.Random.Guid());
            return fakerViewModel.Generate();
        }

        public Favorite CreateFakerFavorite()
        {
            var fakerViewModel = new Faker<Favorite>()
                .CustomInstantiator(c => new Favorite())
                .RuleFor(f => f.ClientId, o => o.Random.Guid())
                .RuleFor(f => f.ProductId, o => o.Random.Guid())
                .RuleFor(f => f.Product, o => CreateFakerProduct());
            return fakerViewModel.Generate();
        }

        public List<Favorite> CreateFakerFavoriteList()
        {
            var fakerViewModel = new Faker<Favorite>()
                .CustomInstantiator(c => new Favorite())
                .RuleFor(f => f.ClientId, o => o.Random.Guid())
                .RuleFor(f => f.ProductId, o => o.Random.Guid())
                .RuleFor(f => f.Product, o => CreateFakerProduct());
            return fakerViewModel.Generate(genericFaker.Random.Int(1, 10));
        }

        public Product CreateFakerProduct()
        {
            var faker = new Faker<Product>()
                .CustomInstantiator(c => new Product())
                .RuleFor(p => p.Title, o => o.Lorem.Sentence())
                .RuleFor(p => p.Price, o => o.Random.Decimal())
                .RuleFor(p => p.Brand, o => o.Lorem.Word())
                .RuleFor(p => p.Image, o => o.Image.LoremPixelUrl())
                .RuleFor(p => p.ReviewScore, o => o.Random.Number(5));
            return faker.Generate();
        }

        public IList<ErrorInfo> CreateFakerErrorInfoList(HttpStatusCode? statusCode = null)
        {
            statusCode = statusCode.HasValue ? statusCode : (HttpStatusCode)genericFaker.Random.ListItem(STATUS_CODE_LIST);

            var fakerErrorInfo = new Faker<ErrorInfo>()
                .CustomInstantiator(c => new ErrorInfo(statusCode.Value, c.Lorem.Word(), c.Lorem.Sentences()));

            return fakerErrorInfo.Generate(genericFaker.Random.Int(1, 10));
        }

        public ErrorInfo CreateFakerErrorInfo(HttpStatusCode? statusCode = null)
        {
            statusCode = statusCode.HasValue ? statusCode : (HttpStatusCode)genericFaker.Random.ListItem(STATUS_CODE_LIST);

            var fakerErrorInfo = new Faker<ErrorInfo>()
                .CustomInstantiator(c => new ErrorInfo(statusCode.Value, c.Lorem.Word(), c.Lorem.Sentences()));

            return fakerErrorInfo.Generate();
        }

        public ClientResponseMessages CreateFakerClientResponseMessages()
        {
            var fakerResponseMessages = new Faker<ClientResponseMessages>()
                .CustomInstantiator(m => new ClientResponseMessages())
                .RuleFor(m => m.ClientNotFound, o => o.Lorem.Word())
                .RuleFor(m => m.ClientNotFoundDescription, o => o.Lorem.Sentence())
                .RuleFor(m => m.EmailAlreadyUsed, o => o.Lorem.Word())
                .RuleFor(m => m.EmailAlreadyUsedDescription, o => o.Lorem.Sentence())
                .RuleFor(m => m.InternalError, o => o.Lorem.Word())
                .RuleFor(m => m.InternalErrorDescription, o => o.Lorem.Sentence())
                .RuleFor(m => m.NoClientChanged, o => o.Lorem.Word())
                .RuleFor(m => m.NoClientChangedDescription, o => o.Lorem.Sentence());
            return fakerResponseMessages.Generate();
        }

        public FavoriteResponseMessages CreateFakerFavoriteResponseMessages()
        {
            var fakerResponseMessages = new Faker<FavoriteResponseMessages>()
                .CustomInstantiator(m => new FavoriteResponseMessages())
                .RuleFor(m => m.InternalError, o => o.Lorem.Word())
                .RuleFor(m => m.InternalErrorDescription, o => o.Lorem.Sentence())
                .RuleFor(m => m.NoFavoriteChanged, o => o.Lorem.Word())
                .RuleFor(m => m.NoFavoriteChangedDescription, o => o.Lorem.Sentence())
                .RuleFor(m => m.NoFavoriteFound, o => o.Lorem.Word())
                .RuleFor(m => m.NoFavoriteFoundDescription, o => o.Lorem.Sentence())
                .RuleFor(m => m.NoFavoritesFound, o => o.Lorem.Word())
                .RuleFor(m => m.NoFavoritesFoundDescription, o => o.Lorem.Sentence())
                .RuleFor(m => m.ProductAlreadyInFavorites, o => o.Lorem.Word())
                .RuleFor(m => m.ProductAlreadyInFavoritesDescription, o => o.Lorem.Sentence())
                .RuleFor(m => m.ProductDoesNotExist, o => o.Lorem.Word())
                .RuleFor(m => m.ProductDoesNotExistDescription, o => o.Lorem.Sentence());
            return fakerResponseMessages.Generate();
        }

        public ResponseModel<Client> CreateFakerResponseModelOfClient()
        {
            var fakerResponseModelOfClient = new Faker<ResponseModel<Client>>()
                .CustomInstantiator(c => new ResponseModel<Client>(CreateFakerClient()));

            return fakerResponseModelOfClient.Generate();
        }

        public ResponseModel<Favorite> CreateFakerResponseModelOfFavorite()
        {
            var fakerResponseModelOfFavorite = new Faker<ResponseModel<Favorite>>()
                .CustomInstantiator(c => new ResponseModel<Favorite>(CreateFakerFavorite()));

            return fakerResponseModelOfFavorite.Generate();
        }

        public ResponseModel<Client> CreateFakerResponseModelClientWithError()
        {
            var fakerResponseModelOfClient = new Faker<ResponseModel<Client>>()
                .CustomInstantiator(c => new ResponseModel<Client>(true, CreateFakerErrorInfoList()));

            return fakerResponseModelOfClient.Generate();
        }

        public ResponseModel<Favorite> CreateFakerResponseModelFavoriteWithError()
        {
            var fakerResponseModelOfFavorite = new Faker<ResponseModel<Favorite>>()
                .CustomInstantiator(c => new ResponseModel<Favorite>(true, CreateFakerErrorInfoList()));

            return fakerResponseModelOfFavorite.Generate();
        }
    }
}
