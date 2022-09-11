using AutoMapper;
using KolmeoAPI.BLayer;
using KolmeoAPI.Controllers;
using KolmeoAPI.DALayer;
using KolmeoAPI.Data;
using KolmeoAPI.Data.Models;
using KolmeoAPI.Models;
using KolmeoAPI.Tests.SeedData;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace KolmeoAPI.Tests
{
    public class APIUnitTest
    {

        ProductContext _context;
        ProductController _productController;
                
        [SetUp]
        public void Setup()
        {
            _context = DbSetup.GetMemoryContext();
            ProductSeedData.Seed(_context);

            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<Product, ProductDTO>().ReverseMap();
            });

            var mapper = config.CreateMapper();

            var dataAccessLayer = new DataAccessLayer(_context);
            var businessLayer = new BusinessLayer(mapper, dataAccessLayer);
            _productController = new ProductController(businessLayer);
        }

        [Test]
        public void CheckGetProductsReturnSuccess()
        {
            var products = _productController.GetProducts();
            //Assert
            Assert.IsInstanceOf<OkObjectResult>(products.Result.Result);

            var list = products.Result.Result as OkObjectResult;

            Assert.IsInstanceOf<List<ProductDTO>>(list.Value);

            var listProducts = list.Value as List<ProductDTO>;

            Assert.AreEqual(5, listProducts.Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        public void CheckGetProductReturnSuccess(int Id)
        {
            var output = _productController.GetProduct(Id);
            //Assert
            Assert.IsInstanceOf<OkObjectResult>(output.Result.Result);

            var product = output.Result.Result as OkObjectResult;

            Assert.IsInstanceOf<ProductDTO>(product.Value);
        }

        [Test]
        [TestCase(0)]
        public void CheckGetProductReturnNotFound(int Id)
        {
            var product = _productController.GetProduct(Id);
            //Assert
            Assert.IsInstanceOf<NotFoundResult>(product.Result.Result);
        }

        [Test]
        public void CheckCreateProductReturnSuccess()
        {
            var productDTO = new ProductDTO
            {
                Name = "New Product",
                Description = "New Product Description",
                Price = 450.76M
            };

            var output = _productController.CreateProduct(productDTO);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(output.Result.Result);

            var product = output.Result.Result as OkObjectResult;

            Assert.IsInstanceOf<ProductDTO>(product.Value);

            Assert.IsTrue((product.Value as ProductDTO).Id != 0);
        }

        [Test]
        public void CheckCreateProductFailsOnEmptyName()
        {
            var productDTO = new ProductDTO
            {
                Name = "",
                Description = "New Product Description",
                Price = 450.76M
            };

            var output = _productController.CreateProduct(productDTO);

            //Assert
            var ex = Assert.ThrowsAsync<AggregateException>(() => _productController.CreateProduct(productDTO));

            Assert.That(ex.InnerException.Message, Is.EqualTo("Name cannot be empty"));
        }

        [Test]
        public void CheckCreateProductFailsOnEmptyDescription()
        {
            var productDTO = new ProductDTO
            {
                Name = "New Product",
                Description = "",
                Price = 450.76M
            };

            var output = _productController.CreateProduct(productDTO);

            //Assert
            var ex = Assert.ThrowsAsync<AggregateException>(() => _productController.CreateProduct(productDTO));

            Assert.That(ex.InnerException.Message, Is.EqualTo("Description cannot be empty"));
        }

        [Test]
        public void CheckCreateProductFailsOnInvalidPrice()
        {
            var productDTO = new ProductDTO
            {
                Name = "New Product",
                Description = "New Product Description",
                Price = 0.00M
            };

            var output = _productController.CreateProduct(productDTO);

            //Assert
            var ex = Assert.ThrowsAsync<AggregateException>(() => _productController.CreateProduct(productDTO));

            Assert.That(ex.InnerException.Message, Is.EqualTo("Price cannot be less than $0.01"));
        }
    }
}