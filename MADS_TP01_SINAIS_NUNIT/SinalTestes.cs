using MADS_TP01_SINAIS;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADS_TP01_SINAIS_NUNIT
{
    internal class SinalTestes
    {
        private Repositorio rep;

        [OneTimeSetUp]
        public void Setup()
        {
            rep = new Repositorio();
            rep.LimparBD();
        }

        [TearDown]
        public void TearDown()
        {
            rep.LimparBD();
        }

        [Test]
        public void CriarSinal_DeveRetornarIdCorreto()
        {
            int id = 1;
            Sinal sinal = new Sinal(id, "COD01", "Descricao do sinal", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, true, "Rua 1", "http://imagem1.com");

            int returnedId = sinal.Id;

            Assert.That(returnedId, Is.EqualTo(id));
        }

        [Test]
        public void CriarSinal_DeveRetornarCodigoCorreto()
        {
            string codigo = "COD01";
            Sinal sinal = new Sinal(1, codigo, "Descricao do sinal", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, true, "Rua 2", "http://imagem2.com");

            string returnedCodigo = sinal.Codigo;

            Assert.That(returnedCodigo, Is.EqualTo(codigo));
        }

        [Test]
        public void CriarSinal_DeveRetornarDescricaoCorreta()
        {
            string descricao = "Descricao do sinal";
            Sinal sinal = new Sinal(1, "COD01", descricao, eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, true, "Rua 3", "http://imagem3.com");

            string returnedDescricao = sinal.Descricao;

            Assert.That(returnedDescricao, Is.EqualTo(descricao));
        }
    }
}