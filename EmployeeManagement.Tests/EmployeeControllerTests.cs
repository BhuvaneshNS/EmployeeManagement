using AutoFixture;
using EmployeeManagement.Controllers;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagement.Tests
{
    [TestClass]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeRepository> _mockEmployeeRepository;
        private Fixture _fixture;
        private EmployeeController _employeeController;

        public EmployeeControllerTests()
        {
            _fixture = new Fixture();
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _employeeController = new EmployeeController(_mockEmployeeRepository.Object);

        }
        [TestMethod]
        public async Task AddAsync_ReturnsCreatedAtAction()
        {
            //Arrange
            var employee = _fixture.Create<Employee>();
            _mockEmployeeRepository.Setup(repo => repo.AddAsync(It.IsAny<Employee>()));

            //Act
            var response = await _employeeController.AddAsync(employee);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task AddAsync_ReturnsBadRequest()
        {
            //Arrange         
            _mockEmployeeRepository.Setup(repo => repo.AddAsync(It.IsAny<Employee>()));

            //Act
            var response = await _employeeController.AddAsync(null);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task AddAsync_ReturnsInternalServerError()
        {
            //Arrange
            var employee = _fixture.Create<Employee>();
            _mockEmployeeRepository.Setup(repo => repo.AddAsync(It.IsAny<Employee>())).Throws(new Exception("Something Wrong"));

            // Act
            var result = await _employeeController.AddAsync(employee) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsOk()
        {
            //Arrange
            var employees = _fixture.CreateMany<Employee>(5).ToList();
            _mockEmployeeRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(employees);

            //Act
            var response = await _employeeController.GetAllAsync();

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsInternalServerError()
        {
            //Arrange
            _mockEmployeeRepository.Setup(repo => repo.GetAllAsync()).Throws(new Exception("Something Wrong"));

            //Act
            var response = await _employeeController.GetAllAsync() as ObjectResult;

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);

        }
        [TestMethod]
        public async Task GetByIdAsync_ReturnsOk()
        {
            //Arrange
            var employee = _fixture.Create<Employee>();
            _mockEmployeeRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(employee);

            //Act
            var response = await _employeeController.GetByIdAsync(2);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsBadRequest()
        {
            //Arrange
            var employee = _fixture.Create<Employee>();
            _mockEmployeeRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(employee);

            //Act
            var response = await _employeeController.GetByIdAsync(-2);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsNotFound()
        {
            //Arrange
            var employee = _fixture.Create<Employee>();
            _mockEmployeeRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(employee);

            //Act
            var response = await _employeeController.GetByIdAsync(2);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsInternalServerError()
        {
            //Arrange
            var employee = _fixture.Create<Employee>();
            _mockEmployeeRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Throws(new Exception("Something wrong"));

            //Act
            var response = await _employeeController.GetByIdAsync(2) as ObjectResult;

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnsOk()
        {
            //Arrange
            var employee = _fixture.Create<Employee>();
            _mockEmployeeRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(employee);
            _mockEmployeeRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()));

            //Act
            var response = await _employeeController.DeleteAsync(1);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnsBadRequet()
        {
            //Arrange
            var employee = _fixture.Create<Employee>();
            _mockEmployeeRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(employee);
            _mockEmployeeRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()));

            //Act
            var response = await _employeeController.DeleteAsync(It.IsAny<int>());

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnsNotFound()
        {
            //Arrange
            var employee = _fixture.Create<Employee>();
            _mockEmployeeRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(employee);
            _mockEmployeeRepository.Setup(repo => repo.DeleteAsync(1));

            //Act
            var response = await _employeeController.DeleteAsync(2);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnsInternalServerError()
        {
            //Arrange
            var employee = _fixture.Create<Employee>();
            _mockEmployeeRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(employee);
            _mockEmployeeRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).Throws(new Exception("Something wrong"));

            //Act
            var response = await _employeeController.DeleteAsync(2) as ObjectResult;

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

    }
}