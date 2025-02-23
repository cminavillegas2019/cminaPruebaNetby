using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using TareasApi.Application.DTOs;
using TareasApi.Application.Interfaces;
using TareasApi.Controllers;
using TareasApi.Domain.Entities;
using TareasApi.Infrastructure.Interfaces;

public class TareasControllerTests
{
    private readonly Mock<ITareaService> _tareaServiceMock;
    private readonly Mock<ILoggerService> _loggerMock;
    private readonly TareasController _controller;

    public TareasControllerTests()
    {
        _tareaServiceMock = new Mock<ITareaService>();
        _loggerMock = new Mock<ILoggerService>();
        _controller = new TareasController(_tareaServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetTarea_TareaExiste_RetornaOk()
    {
        var tareaMock = new Tarea { Id = 1, Titulo = "Tarea de prueba", Descripcion = "Descripción" };

        _tareaServiceMock.Setup(service => service.GetTarea(1))
            .ReturnsAsync(tareaMock);

        var result = await _controller.GetTarea(1);

  
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<Tarea>(okResult.Value);
        Assert.Equal(1, response.Id);
    }

    [Fact]
    public async Task GetTarea_TareaNoExiste_RetornaBadRequestYLog()
    {
       
        _tareaServiceMock.Setup(service => service.GetTarea(99))
            .ReturnsAsync((Tarea)null); 


        var result = await _controller.GetTarea(99);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errorResponse = Assert.IsType<ErrorDto>(badRequestResult.Value);
        Assert.Equal(404, errorResponse.Codigo);
        Assert.Equal("Tarea no encontrada.", errorResponse.Mensaje);

    }
}
