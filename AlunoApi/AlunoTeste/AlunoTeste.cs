using AlunoApi.Controllers;
using AlunoApi.Models;
using AlunoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AlunoTeste
{
    public class AlunosControllerTests
    {
        // Mock simula o comportamento do IAlunoService
        private readonly Mock<IAlunoService> _mockAlunoService;

        // mock como dependência
        private readonly AlunosController _controller;

        public AlunosControllerTests()
        {
            // Iniciando  o mock 
            _mockAlunoService = new Mock<IAlunoService>();
            _controller = new AlunosController(_mockAlunoService.Object);
        }

        [Fact]
        public async Task GetAlunos_ReturnsOkResult_WithListOfAlunos()
        {
            // Arrange: Configurando o cenário de teste
            var alunos = new List<Aluno>
            {
                new Aluno { Id = 1, Nome = "João" },
                new Aluno { Id = 2, Nome = "Maria" }
            };

            // Configurando o mock para a lista de alunos 
            _mockAlunoService.Setup(service => service.GetAlunos()).ReturnsAsync(alunos);

            // Act: Executando a ação GetAlunos 
            var result = await _controller.GetAlunos();

            // Assert: Verificando o resultado contendo a lista de alunos
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnAlunos = Assert.IsAssignableFrom<IEnumerable<Aluno>>(okResult.Value);
            Assert.Equal(2, returnAlunos.Count());  // Verifica se a lista contém exatamente dois alunos
        }

        [Fact]
        public async Task GetAluno_ReturnsNotFound_WhenAlunoDoesNotExist()
        {
            // Arrange: Configurando o cenário onde o aluno não existe
            int id = 1;
            _mockAlunoService.Setup(service => service.GetAluno(id)).ReturnsAsync((Aluno)null);

            // Act: Executando a ação GetAluno no controlador
            var result = await _controller.GetAluno(id);

            // Assert: Verificando se o resultado é NotFoundObjectResult com a mensagem correta
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal($"Não existe aluno com id={id}", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAluno_ReturnsOkResult_WithAluno()
        {
            // Arrange: Configurando o cenário onde um aluno existe
            int id = 1;
            var aluno = new Aluno { Id = id, Nome = "João" };
            _mockAlunoService.Setup(service => service.GetAluno(id)).ReturnsAsync(aluno);

            // Act: Executando a ação GetAluno no controlador
            var result = await _controller.GetAluno(id);

            // Assert: Verificando se o resultado é OkObjectResult e contém o aluno esperado
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnAluno = Assert.IsType<Aluno>(okResult.Value);
            Assert.Equal(id, returnAluno.Id);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtRoute_WithAluno()
        {
            // Arrange: Configurando o cenário de criação de um novo aluno
            var aluno = new Aluno { Id = 1, Nome = "João" };
            _mockAlunoService.Setup(service => service.CreateAluno(aluno)).Returns(Task.CompletedTask);

            // Act: Executando a ação Create 
            var result = await _controller.Create(aluno);

            // Assert: Verificando se o resultado é CreatedAtRouteResult 
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("GetAluno", createdAtRouteResult.RouteName);
            Assert.Equal(aluno.Id, ((Aluno)createdAtRouteResult.Value).Id);
        }

        [Fact]
        public async Task Edit_ReturnsOkResult_WhenAlunoIsUpdated()
        {
            // Arrange: Configurando o cenário onde um aluno existente é atualizado
            var aluno = new Aluno { Id = 1, Nome = "João Atualizado" };
            _mockAlunoService.Setup(service => service.UpdateAluno(aluno)).Returns(Task.CompletedTask);

            // Act: Executando a ação Edit no controlador
            var result = await _controller.Edit(aluno.Id, aluno);

            // Assert: Verificando se o resultado é OkObjectResult com a mensagem de sucesso
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal($"Aluno com id= {aluno.Id} foi alterado com sucesso", okResult.Value);
        }

        [Fact]
        public async Task Edit_ReturnsBadRequest_WhenIdDoesNotMatchAlunoId()
        {
            // Arrange: Configurando o cenário onde o ID fornecido não corresponde ao ID do aluno
            var aluno = new Aluno { Id = 1, Nome = "João Atualizado" };

            // Act: Executando a ação Edit com IDs incorreto
            var result = await _controller.Edit(2, aluno);

            // Assert: Verificando se o resultado é BadRequestObjectResult com a mensagem de erro
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Dados inconsistentes", badRequestResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenAlunoDoesNotExist()
        {
            // Arrange: Configurando o cenário onde o aluno a ser deletado não existe
            int id = 1;
            _mockAlunoService.Setup(service => service.GetAluno(id)).ReturnsAsync((Aluno)null);

            // Act: Executando a ação Delete no controlador
            var result = await _controller.Delete(id);

            // Assert: Verificando se o resultado é NotFoundObjectResult 
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Aluno com id={id} não encontrado", notFoundResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WhenAlunoIsDeleted()
        {
            // Arrange: Configurando o cenário onde um aluno existente é deletado
            int id = 1;
            var aluno = new Aluno { Id = id, Nome = "João Atualizado" };
            _mockAlunoService.Setup(service => service.GetAluno(id)).ReturnsAsync(aluno);
            _mockAlunoService.Setup(service => service.DeleteAluno(aluno)).Returns(Task.CompletedTask);

            // Act: Executando a ação Delete no controlador
            var result = await _controller.Delete(id);

            // Assert: Verificando se o resultado é OkObjectResult 
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal($"Aluno de id={id} foi exlcuido com sucesso", okResult.Value);
        }
    }
}
