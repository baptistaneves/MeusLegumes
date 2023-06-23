using Bogus;
using Bogus.DataSets;
using MeusLegumes.Application.Contexts.Clientes.Contracts;
using MeusLegumes.Application.Contexts.Clientes.Services;
using Moq.AutoMock;

namespace MeusLegumes.Application.Tests.Contexts.Clientes
{
    [CollectionDefinition(nameof(ClienteAppServiceTestsColletion))]
    public class ClienteAppServiceTestsColletion : ICollectionFixture<ClienteAppServiceTestsFixture>
    {
    }

    public class ClienteAppServiceTestsFixture
    {
        public AutoMocker Mocker;
        public ClienteAppService ClienteAppService;

        public ClienteAppService ObterClienteAppService()
        {
            Mocker = new AutoMocker();
            ClienteAppService = Mocker.CreateInstance<ClienteAppService>();

            return ClienteAppService;
        }

        public CriarCliente ObterClienteValido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var cliente = new Faker<CriarCliente>("pt_PT")
                .CustomInstantiator(f => new CriarCliente
                {
                    MunicipioId = Guid.NewGuid(),
                    Nome = f.Person.FirstName,
                    Tipo = "Cliente",
                    NumeroContribuinte = "1234567890",
                    TelefonePrincipal = f.Phone.PhoneNumber(),
                    TelefoneAlternativo = f.Phone.PhoneNumber(),
                    Email = f.Person.Email,
                    Senha = f.Lorem.Letter(8),
                    Rua = f.Address.StreetName(),
                    Casa = f.Address.BuildingNumber(),
                    CodigoPostal = f.Address.ZipCode(),
                    PontoDeReferencia = f.Address.FullAddress(),
                });

            return cliente;
        }

        public CriarCliente ObterClienteInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var cliente = new Faker<CriarCliente>("pt_PT")
                .CustomInstantiator(f => new CriarCliente
                {
                    MunicipioId = Guid.Empty,
                    Nome = "",
                    Tipo = "",
                    NumeroContribuinte = "1234567890",
                    TelefonePrincipal = f.Phone.PhoneNumber(),
                    TelefoneAlternativo = f.Phone.PhoneNumber(),
                    Email = f.Person.Website,
                    Senha = f.Lorem.Letter(8),
                    Rua = f.Address.StreetName(),
                    Casa = f.Address.BuildingNumber(),
                    CodigoPostal = f.Address.ZipCode(),
                    PontoDeReferencia = f.Address.FullAddress(),
                });

            return cliente;
        }
    }
}
