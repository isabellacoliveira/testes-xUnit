using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class VeiculoTeste : IDisposable
    {
        public ITestOutputHelper saidaConsoleTeste;

        public VeiculoTeste(ITestOutputHelper _saidaConsoleTeste)
        {
            saidaConsoleTeste = _saidaConsoleTeste;
            saidaConsoleTeste.WriteLine("Construtor Invocado");
        }

        [Fact(DisplayName = "Teste Veículo Acelerar")]
        // funciona como um esquema de chave valor 
        [Trait("Funcionalidade", "Acelerar")]
        public void TestaVeiculoAcelerarComParametro10()
        {
            var veiculo = new Veiculo();
            veiculo.Acelerar(10);

            // o que espero de resultado , espero 100 
            Assert.Equal(100, veiculo.VelocidadeAtual);
        }

        [Fact(DisplayName = "Teste Veículo Frear")]
        [Trait("Funcionalidade", "Frear")]
        public void TestaVeiculoFrearComParametro10()
        {
            // Arranje
            var veiculo = new Veiculo();

            // Act
            veiculo.Frear(10);

            // Assert
            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }

        // Teste para validar o tipo do objeto de veículo 
        [Fact(DisplayName = "Teste Veículo Tipo")]
        public void TestaTipoVeiculo()
        {
            //Arrange
            var veiculo = new Veiculo();
            //Act            
            //Assert
            Assert.Equal(TipoVeiculo.Automovel, veiculo.Tipo);
        }

        public void Dispose()
        {
            saidaConsoleTeste.WriteLine("Dispose Invocado");
        }
    }
}
