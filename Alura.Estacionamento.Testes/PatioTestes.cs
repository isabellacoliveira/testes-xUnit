using System.Security.Cryptography.X509Certificates;
using Alura.Estacionamento.Modelos;
using System;
using Xunit;
using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Newtonsoft.Json.Bson;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class PatioTestes : IDisposable
    {
        public ITestOutputHelper saidaConsoleTeste;

        public PatioTestes(ITestOutputHelper _saidaConsoleTeste)
        {
            saidaConsoleTeste = _saidaConsoleTeste;
            saidaConsoleTeste.WriteLine("Construtor Invocado");
        }

        // Vamos validar a função de faturamento da aplicação
        [Fact(DisplayName = "Teste Pátio Faturamento")]
        public void ValidaFaturamento()
        {
            // Arrange 
            var estacionamento = new Patio();
            var veiculo = new Veiculo();

            veiculo.Proprietario = "Isabella Oliveira";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Marca";
            veiculo.Placa = "ASD-9999";

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            // Act 
            double faturamento = estacionamento.TotalFaturado();

            // Assert 
            Assert.Equal(2, faturamento);
        }

        // mesmo método mas testando com vários veículos, cada valor é aplicado na chamada do método
        [Theory]
        [InlineData("André Silva", "ASD-9999", "Preto", "Gol")]
        [InlineData("Isabella Oliveira", "CBB-9999", "Preto", "Mob")]
        [InlineData("Manuela Jesus", "SDF-9999", "Branco", "HB20")]
        public void ValidaFaturamentoComVariosVeiculos(string proprietario, string placa, string cor, string modelo)
        {
            // criar estacionamento e receber os parâmetros 
            // Arrange
            var estacionamento = new Patio();
            var veiculo = new Veiculo();

            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            // Act
            double faturamento = estacionamento.TotalFaturado();

            // Assert 
            Assert.Equal(2, faturamento); 
        }

        // caso eu queira que um teste seja ignorado 
        [Fact(DisplayName = "Teste Valida Nome Proprietário", Skip = "Teste ainda não implementado, ignorar")]
        public void ValidaNomeProprietarioDoVeiculo()
        {
            // método não implementado 
        }

        //[Theory]
        //[ClassData(typeof(Veiculo))]
        //public void TestaVeiculoClass(Veiculo modelo)
        //{
        //    //Arrange
        //    var veiculo = new Veiculo();

        //    //Act
        //    veiculo.Acelerar(10);
        //    modelo.Acelerar(10);

        //    //Assert
        //    Assert.Equal(modelo.VelocidadeAtual, veiculo.VelocidadeAtual);
        //}

        [Theory]
        [InlineData("André Silva", "ASD-1498", "preto", "Gol")]
        public void LocalizaVeiculoNoPatioComBaseNaPlaca(string proprietario,
                                           string placa,
                                           string cor,
                                           string modelo)
        {
            //Arrange
            Patio estacionamento = new Patio();
            var veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Acelerar(10);
            veiculo.Frear(5);
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = estacionamento.PesquisaVeiculo(placa);

            //Assert
            Assert.Equal(placa, consultado.Placa);
        }

        //[Fact]
        //public void AlterarDadosDoProprioVeiculo()
        //{

        //    // Arrange 
        //    Patio estacionamento = new Patio();
        //    var veiculo = new Veiculo();

        //    veiculo.Proprietario = "Isabella Oliveira";
        //    veiculo.Cor = "Verde";
        //    veiculo.Modelo = "Marca";
        //    veiculo.Placa = "ASD-9999";
        //    estacionamento.RegistrarEntradaVeiculo(veiculo);

        //    var veiculoAlterado = new Veiculo();
        //    veiculoAlterado.Proprietario = "Isabella Cruz";
        //    veiculoAlterado.Cor = "Preto";
        //    veiculoAlterado.Modelo = "Hyundai";
        //    veiculoAlterado.Placa = "KJH-9999";

        //    // Act 
        //    Veiculo alterado = estacionamento.AlterarDadosVeiculo(veiculoAlterado);
            
        //    // Assert 
        //    Assert.Equal(alterado.Cor, veiculoAlterado.Cor);

        //}

        [Fact]
        public void FichaDeInformacaoDoVeiculo()
        {
            // Arrange 
            var carro = new Veiculo();
            carro.Proprietario = "Isa";
            carro.Tipo = TipoVeiculo.Automovel;
            carro.Placa = "ZAP-3453";
            carro.Cor = "Preto";
            carro.Modelo = "Variante";

            // Act 
            string dados = carro.ToString();

            // Assert 
            Assert.Contains("Ficha do Veículo", dados); 
        }

        [Fact]
        public void TestaNomeProprietarioVeiculoComMenosDeTresCaracteres()
        {
            // Arrange
            string nomeProprietario = "ab";

            // Assert 
            // testar exceções , qual tipo espera receber 
            Assert.Throws<System.FormatException>(
                // Act 
                () => new Veiculo(nomeProprietario)
            );
        }

        [Fact]
        public void TestaMensagemDeExcecaoDoQuartoCaracterDaPlaca()
        {
            // Arrange
            // capturar mensagem da placa 
            string placa = "BNHH9980";

            // Act 
            var mensagem = Assert.Throws<System.FormatException>(
                // Act 
                // a placa está errada, a mensagem vai ser disparada pois o quarto caracter deve ser um hífen
                () => new Veiculo().Placa = placa 
            );

            // Assert 
            Assert.Equal("O 4 caracter deve ser um hífen", mensagem.Message);
        }

        [Fact]
        public void TestaUltimosCaracteresPlacaVeiculoComoNumeros()
        {
            //Arrange
            string placaFormatoErrado = "ASD-995U";

            //Assert
            Assert.Throws<FormatException>(
                //Act
                () => new Veiculo().Placa = placaFormatoErrado
            );

        }

        public void Dispose()
        {
            saidaConsoleTeste.WriteLine("Dispose Invocado");
        }
    }
}
