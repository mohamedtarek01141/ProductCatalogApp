using AutoMapper;
using ecpmmerceApp.Application.DTOs;
using ecpmmerceApp.Application.DTOs.Product;
using ecpmmerceApp.Application.Services;
using ecpmmerceApp.Application.Services.Logging;
using ecpmmerceApp.Domain.Entities;
using ecpmmerceApp.Domain.Interface;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductCatalogApp.Tests.Controller
{
    public class ProductControllerTests
    {
        private  ProductController _productcontroller;
       private IProductService _productService;
        private ICategoryService _categoryService;
        private IWebHostEnvironment _webHostEnvironment;
        private IAppLogger<ProductController> _appLogger;
        public ProductControllerTests()
        {
            _productService=A.Fake<IProductService>();
            _categoryService=A.Fake<ICategoryService>();
            _appLogger=A.Fake<IAppLogger<ProductController>>();
            _webHostEnvironment=A.Fake<IWebHostEnvironment>();

            
            _productcontroller=new ProductController(_productService,_webHostEnvironment,_appLogger, _categoryService);
        }
        [Fact]
        public void ProductController_Index_ReturnSuccess()
        {
            var Products = A.Fake<IEnumerable<ProductResponse>>();
            A.CallTo(() => _productService.getAll()).Returns(Products);
           var result= _productcontroller.Index();
            result.Should().BeOfType<Task<IActionResult>>();

        }

        [Fact]
        public async Task ProductController_Edit_Get_ReturnsView()
        {
            // Arrange
            var productId = Guid.NewGuid();
            A.CallTo(() => _productService.getByID(productId)).Returns(new ProductResponse());

            // Act
            var result = await _productcontroller.Edit(productId);

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            viewResult.Model.Should().BeOfType<ProductResponse>();
        }

        [Fact]
        public async Task ProductController_Edit_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            // Arrange
            var productResponse = new ProductResponse(); // Simulate invalid model state
            _productcontroller.ModelState.AddModelError("Error", "Model is invalid");

            // Act
            var result = await _productcontroller.Edit(productResponse);

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            viewResult.Model.Should().Be(productResponse);
        }
        [Fact]
        public async Task ProductController_Delete_ReturnsRedirect()
        {
            // Arrange
            var productId = Guid.NewGuid();
            A.CallTo(() => _productService.delete(productId)).Returns(new ServiceResponse());

            // Act
            var result = await _productcontroller.Delete(productId);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
            var redirectResult = result as RedirectToActionResult;
            redirectResult.ActionName.Should().Be("Index");
        }

    }
}
