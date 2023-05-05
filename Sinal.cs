using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

public enum eTipoSinal {
    PROIBICAO,
    OBRIGACAO,
    PERIGO,
    INFORMACAO
}

public enum eFormaSinal {
    TRIANGULAR,
    REDONDO,
    RETANGULAR
}

namespace MADS_TP01_SINAIS {
    internal class Sinal {
        private static int contagemId = 0;

        public int Id { get; set; }
        public string Descricao { get; set; }
        public eTipoSinal TipoSinal { get; set; }
        public eFormaSinal FormaSinal { get; set; }
        public string Codigo { get; set; }
        public bool EmUso { get; set; }
        public string Morada { get; set; }
        public string ImagemUrl { get; set; }
        public Sinal(int id, string codigo, string desc,/* eCoresComuns cor,*/ eTipoSinal tipo, eFormaSinal forma, bool emUso, string morada, string imagemUrl) {
            Id = id;
            Codigo = codigo;
            Descricao = desc;
            TipoSinal = tipo;
            FormaSinal = forma;
            EmUso = emUso;
            Morada = morada;
            ImagemUrl = imagemUrl;
        }

        public Sinal(string codigo, string desc,/* eCoresComuns cor,*/ eTipoSinal tipo, eFormaSinal forma, bool emUso, string morada, string imagemUrl)
        : this(++contagemId, codigo, desc, tipo, forma, emUso, morada, imagemUrl) {
        }

        public Sinal(string codigo, string desc, /*eCoresComuns cor,*/ eTipoSinal tipo, eFormaSinal forma,bool emUso, string imagemUrl)
        : this(++contagemId, codigo, desc, tipo, forma, emUso, "NA", imagemUrl) {
        }

        public Sinal(string codigo, string desc, /*eCoresComuns cor,*/ eTipoSinal tipo, eFormaSinal forma, bool emUso)
            : this(++contagemId, codigo, desc, tipo, forma, emUso, "NA", "") {
        }

        public Sinal(string desc, /*eCoresComuns cor,*/ eTipoSinal tipo, eFormaSinal forma)
            : this(++contagemId, "NA", desc, tipo, forma, false, "NA", "") {
        }

        public Sinal(/*eCoresComuns cor,*/ eTipoSinal tipo, eFormaSinal forma)
            : this(++contagemId, "NA", "NA", tipo, forma, false, "NA", "") {
        }

        public void Desenhar(int x, int y, int width = 50, int height = 50, Form oForm = null) {
            if (oForm == null) 
                throw new ArgumentNullException(nameof(oForm), "NULL FORM.");

            PictureBox pictureBox = new PictureBox();
            pictureBox.Location = new Point(x, y);
            pictureBox.Size = new Size(width, height);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            oForm.Controls.Add(pictureBox);

            string url = this.ImagemUrl != "" ? this.ImagemUrl : "https://cdn.discordapp.com/attachments/1099350514170331198/1102678147037659187/not_found.png";
            HttpWebRequest request = null;

            try {
                request = (HttpWebRequest)WebRequest.Create(url);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                    using (Stream stream = response.GetResponseStream()) {
                        Image image = Image.FromStream(stream);
                        pictureBox.Image = image;
                    }
                }
            }
            catch (Exception) {
                //throw;
            }
        }
    }
}