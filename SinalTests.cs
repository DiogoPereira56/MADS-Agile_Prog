using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MADS_TP01_SINAIS
{
    [TestFixture]
    public class SinalTests
    {
        [Test]
        public void CriarSinal_DeveRetornarIdCorreto()
        {
            int id = 1;
            Sinal sinal = new Sinal(id, "COD01", "Descricao do sinal", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, true, "Rua 1", "http://imagem1.com");

            int returnedId = sinal.Id;

            Assert.AreEqual(id, returnedId);
        }

        [Test]
        public void CriarSinal_DeveRetornarCodigoCorreto()
        {
            string codigo = "COD01";
            Sinal sinal = new Sinal(1, codigo, "Descricao do sinal", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, true, "Rua 2", "http://imagem2.com");

            string returnedCodigo = sinal.Codigo;

            Assert.AreEqual(codigo, returnedCodigo);
        }

        [Test]
        public void CriarSinal_DeveRetornarDescricaoCorreta()
        {
            string descricao = "Descricao do sinal";
            Sinal sinal = new Sinal(1, "COD01", descricao, eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, true, "Rua 3", "http://imagem3.com");

            string returnedDescricao = sinal.Descricao;

            Assert.AreEqual(descricao, returnedDescricao);
        }

        [Test]
        public void Desenhar_URLInvalido_NaoAplicar()
        {
            Form form = new Form();
            PictureBox pictureBox = new PictureBox();
            form.Controls.Add(pictureBox);

            Sinal sinal = new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "invalid-url");

            sinal.Desenhar(x: 0, y: 0, oForm: form);

            Assert.IsNull(pictureBox.Image);
        }

    }
}
