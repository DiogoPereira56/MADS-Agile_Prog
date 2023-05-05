using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;

namespace MADS_TP01_SINAIS {
    [TestFixture]
    internal class RepositorioTests {
        private Repositorio rep;

        [SetUp]
        public void Setup() {
            rep = new Repositorio();
        }

        [Test]
        public void TesteContarSinais() {
            Repositorio rep = new Repositorio();
            rep.AdicionarSinal(new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://servicos.infraestruturasdeportugal.pt/sites/default/files/inline-images/rodoviaria/seguranca_rodoviaria/normas_sinalizacao/3_proibicao/C1.gif"));
            rep.AdicionarSinal(new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, false, "https://servicos.infraestruturasdeportugal.pt/sites/default/files/inline-images/rodoviaria/seguranca_rodoviaria/normas_sinalizacao/8_informacao/H24.gif"));

            int count = rep.ContarSinais();

            Assert.AreEqual(2, count);
        }

        [Test]
        public void AdicionarSinal_DeveAumentarContagem() {
            Sinal sinal = new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://example.com/sinal.gif");
            int initialCount = rep.ContarSinais();

            rep.AdicionarSinal(sinal);
            int newCount = rep.ContarSinais();

            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public void RemoverSinal_DeveDiminuirContagem() {
            Sinal sinal = new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://example.com/sinal.gif");
            rep.AdicionarSinal(sinal);
            int initialCount = rep.ContarSinais();

            rep.RemoverSinal(sinal.Id);
            int newCount = rep.ContarSinais();

            Assert.AreEqual(initialCount - 1, newCount);
        }

        [Test]
        public void PesquisarSinal_DeveRetornarSinalSeEncontrar() {
            Sinal sinal = new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://example.com/sinal.gif");
            rep.AdicionarSinal(sinal);

            Sinal foundSinal = rep.PesquisarSinal(sinal.Id);

            Assert.IsNotNull(foundSinal);
            Assert.AreEqual(sinal, foundSinal);
        }

        [Test]
        public void PesquisarSinal_NaoExisteIdRetornaNulo() {
            int nonExistingId = 999;

            Sinal foundSinal = rep.PesquisarSinal(nonExistingId);

            Assert.IsNull(foundSinal);
        }

        [Test]
        public void FiltrarSinais_SemParametros_RetornaTodos() {
            Sinal sinal1 = new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://example.com/sinal1.gif");
            Sinal sinal2 = new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, false, "https://example.com/sinal2.gif");
            rep.AdicionarSinal(sinal1);
            rep.AdicionarSinal(sinal2);

            List<Sinal> sinais = rep.FiltrarSinais();

            Assert.AreEqual(2, sinais.Count);
            CollectionAssert.Contains(sinais, sinal1);
            CollectionAssert.Contains(sinais, sinal2);
        }

        [Test]
        public void CalcularStock_CodigoValido_RetornaStockCorreto() {
            Sinal sinal1 = new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://testsite.com/sinal1.gif");
            Sinal sinal2 = new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, false, "https://testsite.com/sinal2.gif");
            Sinal sinal3 = new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, true, "https://testsite.com/sinal3.gif");
            rep.AdicionarSinal(sinal1);
            rep.AdicionarSinal(sinal2);
            rep.AdicionarSinal(sinal3);
            string codigo = "C1";

            int stockCount = rep.CalcularStock(codigo);

            // Deve ser 2 porque o calculo do stock é eLocalizacaoSinal.INDIFERENTE
            Assert.AreEqual(2, stockCount);
        }

        [Test]
        public void CalcularStock_CodigoValido_RetornaStockCorreto_ForaArmazem()
        {
            Sinal sinal1 = new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://testsite.com/sinal1.gif");
            Sinal sinal2 = new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, false, "https://testsite.com/sinal2.gif");
            Sinal sinal3 = new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, true, "https://testsite.com/sinal3.gif");
            rep.AdicionarSinal(sinal1);
            rep.AdicionarSinal(sinal2);
            rep.AdicionarSinal(sinal3);
            string codigo = "C1";

            int stockCount = rep.CalcularStock(codigo, eLocalizacaoSinal.COLOCADO);

            // Deve ser 1 porque apenas o sinal 3 está em uso.
            Assert.AreEqual(1, stockCount);
        }
    }
}
