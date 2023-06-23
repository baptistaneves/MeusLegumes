using MeusLegumes.Application.Contexts.Clientes.Services;
using MeusLegumes.Domain.Contexts.Categorias.Repositories;
using MeusLegumes.Domain.Contexts.Clientes.Repositories;
using Moq;

namespace MeusLegumes.Application.Tests.Contexts.Clientes
{
    [Collection(nameof(ClienteAppServiceTestsColletion))]
    public class ClienteAppServiceTestes
    {
        private ClienteAppService _clienteAppService;
        private ClienteAppServiceTestsFixture _fixture;

        public ClienteAppServiceTestes(ClienteAppServiceTestsFixture fixture)
        {
            _fixture = fixture;
            _clienteAppService= _fixture.ObterClienteAppService();

            _fixture.Mocker.GetMock<IClienteRepository>().Setup(r => r.UnitOfWork.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(true));
        }

        [Fact(DisplayName = "Adicionar cliente com sucesso")]
        [Trait("Cliente", "Cliente Service Tests")]
        public async Task ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            //Arrange
            var novoCliente = _fixture.ObterClienteValido();

            //Act
            await _clienteAppService.Adicionar(novoCliente, Guid.NewGuid(), CancellationToken.None);

            //Assert
            _fixture.Mocker.GetMock<IClienteRepository>().Verify(v => v.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar cliente com falha")]
        [Trait("Cliente", "Cliente Service Tests")]
        public async Task ClienteService_Adicionar_DeveExecutarComFalha()
        {
            //Arrange
            var novoCliente = _fixture.ObterClienteInvalido();

            //Act
            await _clienteAppService.Adicionar(novoCliente, Guid.NewGuid(), CancellationToken.None);

            //Assert
            _fixture.Mocker.GetMock<IClienteRepository>().Verify(v => v.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Never);
        }
    }
}
