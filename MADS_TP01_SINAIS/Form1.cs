using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MADS_TP01_SINAIS {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Repositorio rep = new Repositorio();

            int sinal_count = rep.ObterListaSinais().Count();
            if (sinal_count < 3)
            {
                Sinal sinal_1 = new Sinal("H24", "Autoestrada", eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, false, "https://servicos.infraestruturasdeportugal.pt/sites/default/files/inline-images/rodoviaria/seguranca_rodoviaria/normas_sinalizacao/3_proibicao/C1.gif");
                Sinal sinal_2 = new Sinal("C1", "Sentido proibido", eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, false, "https://servicos.infraestruturasdeportugal.pt/sites/default/files/inline-images/rodoviaria/seguranca_rodoviaria/normas_sinalizacao/8_informacao/H24.gif");
                Sinal sinal_3 = new Sinal("teste proibido", eTipoSinal.PROIBICAO, eFormaSinal.TRIANGULAR);

                rep.AdicionarSinal(sinal_1);
                rep.AdicionarSinal(sinal_2);
                rep.AdicionarSinal(sinal_3);
            }

            int larguraSinal = 50;
            int larguraSinalTotal = larguraSinal * rep.ContarSinais();

            int xInicio = 12 + (189 - larguraSinalTotal) / 2; // a label muda de tamanho depois de escrevermos -- int x = label1.Location.X + (label1.Width - totalSignalWidth) / 2;
            int i = 1;

            foreach (var oSinal in rep.ObterListaSinais()) {
                if (oSinal == null)
                    continue;

                label1.Text += $"[{oSinal.Id}] Sinal adicionado: {oSinal.Codigo}, {oSinal.Descricao}\n";

                int x = xInicio + (i - 1) * larguraSinal;
                int y = 63; // a altura também muda devido aos \n's -- label1.Location.Y + label1.Height;

                oSinal.Desenhar(x, y, oForm: this);

                i++;
            }

            string oInput = "proibido";

            label2.Text = $"Pesquisar por '{oInput}':\n";
            foreach (Sinal sinal in rep.FiltrarSinais(descricao: oInput)) {
                label2.Text += $"[{sinal.Id}]: {sinal.Codigo}, {sinal.Descricao}\n";
            }

            int teste = rep.CalcularStock("C1", eLocalizacaoSinal.COLOCADO);
        }
    }
}
