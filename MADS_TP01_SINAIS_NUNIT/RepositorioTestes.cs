using System.Collections.Generic;
using System;
using MADS_TP01_SINAIS;
using NUnit.Framework;

namespace MADS_TP01_SINAIS_NUNIT
{
    internal class RepositorioTestes
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
        public void TesteContarSinais()
        {
            rep.AdicionarSinal(new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://servicos.infraestruturasdeportugal.pt/sites/default/files/inline-images/rodoviaria/seguranca_rodoviaria/normas_sinalizacao/3_proibicao/C1.gif"));
            rep.AdicionarSinal(new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, false, "https://servicos.infraestruturasdeportugal.pt/sites/default/files/inline-images/rodoviaria/seguranca_rodoviaria/normas_sinalizacao/8_informacao/H24.gif"));

            int count = rep.ContarSinais();

            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public void AdicionarSinal_DeveAumentarContagem()
        {
            Sinal sinal = new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://example.com/sinal.gif");
            int contagemInicial = rep.ContarSinais();

            rep.AdicionarSinal(sinal);
            int contagemFinal = rep.ContarSinais();

            Assert.That(contagemFinal, Is.EqualTo(contagemInicial + 1));
        }

        [Test]
        public void RemoverSinal_DeveDiminuirContagem()
        {
            Sinal sinal = new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://example.com/sinal.gif");
            Sinal sinal2 = new Sinal("H30", "VIA RAPIDA", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://example.com/sinal.gif");
            Sinal sinal3 = new Sinal("H40", "OUTOFBOUNDS", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://example.com/sinal.gif");
            rep.AdicionarSinal(sinal);
            rep.AdicionarSinal(sinal2);
            rep.AdicionarSinal(sinal3);

            int contagemInicial = rep.ContarSinais();

            rep.RemoverSinal(sinal.Id);
            rep.RemoverSinal(sinal3.Id);

            int contagemFinal = rep.ContarSinais();

            Assert.That(contagemFinal, Is.EqualTo(contagemInicial - 2));
        }

        [Test]
        public void PesquisarSinal_DeveRetornarSinalSeEncontrar()
        {
            Sinal sinal = new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://example.com/sinal.gif");
            rep.AdicionarSinal(sinal);
            Sinal foundSinal = rep.PesquisarSinal(sinal.Id);

            Assert.IsNotNull(foundSinal);

            Assert.That(foundSinal.Id, Is.EqualTo(sinal.Id));
            Assert.That(foundSinal.Codigo, Is.EqualTo(sinal.Codigo));
            Assert.That(foundSinal.Descricao, Is.EqualTo(sinal.Descricao));
            Assert.That(foundSinal.TipoSinal, Is.EqualTo(sinal.TipoSinal));
            Assert.That(foundSinal.FormaSinal, Is.EqualTo(sinal.FormaSinal));
            Assert.That(foundSinal.EmUso, Is.EqualTo(sinal.EmUso));
            Assert.That(foundSinal.Morada, Is.EqualTo(sinal.Morada));
            Assert.That(foundSinal.ImagemUrl, Is.EqualTo(sinal.ImagemUrl));
        }

        [Test]
        public void PesquisarSinal_NaoExisteIdRetornaNulo()
        {
            int idInexistente = 999;

            Sinal sinalEncontrado = rep.PesquisarSinal(idInexistente);

            Assert.IsNull(sinalEncontrado);
        }

        [Test]
        public void FiltrarSinais_SemParametros_RetornaTodos()
        {
            Sinal sinal1 = new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://example.com/sinal1.gif");
            Sinal sinal2 = new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, false, "https://example.com/sinal2.gif");
            rep.AdicionarSinal(sinal1);
            rep.AdicionarSinal(sinal2);

            List<Sinal> sinais = rep.FiltrarSinais();

            Assert.That(sinais.Count, Is.EqualTo(2));
        }

        [Test]
        public void CalcularStock_CodigoValido_RetornaStockCorreto()
        {
            Sinal sinal1 = new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://testsite.com/sinal1.gif");
            Sinal sinal2 = new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, false, "https://testsite.com/sinal2.gif");
            Sinal sinal3 = new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, true, "https://testsite.com/sinal3.gif");
            rep.AdicionarSinal(sinal1);
            rep.AdicionarSinal(sinal2);
            rep.AdicionarSinal(sinal3);

            string codigo = "C1";

            int contagemStock = rep.CalcularStock(codigo);

            // Deve ser 2 porque o calculo do stock é eLocalizacaoSinal.INDIFERENTE
            Assert.That(contagemStock, Is.EqualTo(2));
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

            int contagemStock = rep.CalcularStock(codigo, eLocalizacaoSinal.COLOCADO);

            // Deve ser 1 porque apenas o sinal 3 está em uso.
            Assert.That(contagemStock, Is.EqualTo(1));
        }
        [Test]
        public void AtualizarSinal_ExistingSinal_SuccessfullyUpdatesSinal()
        {
            Sinal sinal2 = new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, false, "https://testsite.com/sinal2.gif");
            rep.AdicionarSinal(sinal2);

            Sinal sinal3 = new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, true, "https://testsite.com/sinal3.gif");
            rep.AdicionarSinal(sinal3);

            rep.AtualizarSinal(sinal2.Id, sinal3);

            Sinal sinalAtualizado = rep.PesquisarSinal(sinal2.Id);

            Assert.IsNotNull(sinalAtualizado);
            Assert.That(sinalAtualizado.Id, Is.Not.EqualTo(sinal3.Id));
            Assert.That(sinalAtualizado.Codigo, Is.EqualTo(sinal3.Codigo));
            Assert.That(sinalAtualizado.Descricao, Is.EqualTo(sinal3.Descricao));
            Assert.That(sinalAtualizado.TipoSinal, Is.EqualTo(sinal3.TipoSinal));
            Assert.That(sinalAtualizado.FormaSinal, Is.EqualTo(sinal3.FormaSinal));
            Assert.That(sinalAtualizado.EmUso, Is.EqualTo(sinal3.EmUso));
            Assert.That(sinalAtualizado.Morada, Is.EqualTo(sinal3.Morada));
            Assert.That(sinalAtualizado.ImagemUrl, Is.EqualTo(sinal3.ImagemUrl));
        }

        [Test]
        public void AdicionarIntervencao_DeveAumentarContagem()
        {
            Sinal sinal1 = new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, false, "https://testsite.com/sinal3.gif");
            rep.AdicionarSinal(sinal1);

            Intervencao intervencao = new Intervencao(sinal1.Id, "Colocar sinal de volta no sitio", "Rua do ze", DateTime.Now);
            Intervencao intervencao2 = new Intervencao(sinal1.Id, "Pintar o sinal", "Rua do ze", DateTime.Now);

            int contagemInicial = rep.ContarIntervencoes();

            rep.AdicionarIntervencao(sinal1, intervencao);
            rep.AdicionarIntervencao(sinal1, intervencao);
            rep.AdicionarIntervencao(sinal1, intervencao);


            int contagemFinal = rep.ContarIntervencoes();

            Assert.That(contagemFinal, Is.EqualTo(contagemInicial + 3));
        }

        [Test]
        public void AdicionarAcidente_DeveAumentarContagem()
        {
            Sinal sinal1 = new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, false, "https://testsite.com/sinal3.gif");
            rep.AdicionarSinal(sinal1);

            int contagemInicial = rep.ContarAcidentes();

            Intervencao intervencao = new Intervencao(sinal1.Id, "Colocar sinal de volta no sitio", "Rua do ze", DateTime.Now);
            rep.AdicionarIntervencao(sinal1, intervencao);

            Acidente acidente = new Acidente(intervencao.Id, "O Ze levou com o sinal na testa", "Rua do ze", DateTime.Now);
            rep.AdicionarAcidente(intervencao, acidente);

            int contagemFinal = rep.ContarAcidentes();

            Assert.That(contagemFinal, Is.EqualTo(contagemInicial + 1));
        }
    }
}