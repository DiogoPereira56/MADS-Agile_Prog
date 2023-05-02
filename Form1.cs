using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.IO;

namespace MADS_TP01_SINAIS {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void DesenharSinal(Sinal oSinal, int x, int y, int width = 50, int height = 50, Form oForm = null) {
            oForm = oForm == null ? this : oForm;

            PictureBox pictureBox = new PictureBox();
            pictureBox.Location = new Point(x, y);
            pictureBox.Size = new Size(width, height);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(pictureBox);

            string url = oSinal.ImagemUrl != "" ? oSinal.ImagemUrl : "https://cdn.discordapp.com/attachments/1099350514170331198/1102678147037659187/not_found.png";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                using (Stream stream = response.GetResponseStream()) {
                    Image image = Image.FromStream(stream);
                    pictureBox.Image = image;
                }
            }        
        }

        private void Form1_Load(object sender, EventArgs e) {
            Repositorio rep = new Repositorio();

            Sinal sinal_1 = new Sinal("H24", "Autoestrada", eCoresComuns.AZUL, eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, 1, false, "https://servicos.infraestruturasdeportugal.pt/sites/default/files/inline-images/rodoviaria/seguranca_rodoviaria/normas_sinalizacao/3_proibicao/C1.gif");
            Sinal sinal_2 = new Sinal("C1", "Sentido proibido", eCoresComuns.VERMELHO, eTipoSinal.PROIBICAO, eFormaSinal.REDONDO, 1, false, "https://servicos.infraestruturasdeportugal.pt/sites/default/files/inline-images/rodoviaria/seguranca_rodoviaria/normas_sinalizacao/8_informacao/H24.gif");
            Sinal sinal_3 = new Sinal("teste proibido", eCoresComuns.BRANCO, eTipoSinal.PROIBICAO, eFormaSinal.TRIANGULAR);

            rep.AdicionarSinal(sinal_1);
            rep.AdicionarSinal(sinal_2);
            rep.AdicionarSinal(sinal_3);

            int larguraSinal = 50;
            int larguraSinalTotal = larguraSinal * rep.ContarSinais();

            int x = 12 + (189 - larguraSinalTotal) / 2; // a label muda de tamanho depois de escrevermos -- int x = label1.Location.X + (label1.Width - totalSignalWidth) / 2;
            for (int i = 1; i < rep.ContarSinais() + 1; i++) {
                Sinal oSinal = rep.PesquisarSinal(i);

                if (oSinal == null)
                    continue;

                label1.Text += $"[{ oSinal.Id }] Sinal adicionado: { oSinal.Codigo }, { oSinal.Descricao }\n";

                int signalX = x + (i - 1) * larguraSinal; // Calculate the x position for the current signal
                int y = 63; // a altura também muda devido aos \n's -- label1.Location.Y + label1.Height;

                DesenharSinal(oSinal, signalX, y);
            }

            string oInput = "proibido";

            label2.Text = $"Pesquisar por '{oInput}':\n";
            foreach (Sinal sinal in rep.FiltrarSinais(descricao: oInput)) {
                label2.Text += $"[{sinal.Id}]: {sinal.Codigo}, {sinal.Descricao}\n";
            }
        }
    }
}
