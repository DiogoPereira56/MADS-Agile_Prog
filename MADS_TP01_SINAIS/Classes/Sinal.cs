using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Collections;

/*
public enum eCoresComuns
{
    VERMELHO,
    AZUL,
    VERDE,
    AMARELO,
    BRANCO
}
*/

public enum eTipoSinal {
    INDEFINIDO = 0,
    PROIBICAO,
    OBRIGACAO,
    PERIGO,
    INFORMACAO
}

public enum eFormaSinal {
    INDEFINIDO = 0,
    TRIANGULAR,
    REDONDO,
    RETANGULAR
}

namespace MADS_TP01_SINAIS {
    public class Sinal {

        public int Id { get; set; }

        private string descricao;
        public string Descricao
        {
            get { return descricao; }
            set
            {
                if (value == null) descricao = "";
                else descricao = value;
            }
        }

        private eTipoSinal tipoSinal;
        public eTipoSinal TipoSinal
        {
            get { return tipoSinal; }
            set
            {
                if (value < eTipoSinal.INDEFINIDO || value > eTipoSinal.INFORMACAO) 
                    tipoSinal = eTipoSinal.INDEFINIDO;
                else 
                    tipoSinal = value;
            }
        }
        private eFormaSinal formaSinal;
        public eFormaSinal FormaSinal
        {
            get { return formaSinal; }
            set
            {
                if (value < eFormaSinal.INDEFINIDO || value > eFormaSinal.RETANGULAR) 
                    formaSinal = eFormaSinal.INDEFINIDO;
                else 
                    formaSinal = value;
            }
        }
        private string codigo;
        public string Codigo
        {
            get { return codigo; }
            set
            {
                if (value == null) codigo = "";
                else codigo = value;
            }
        }

        private bool emUso;
        public bool EmUso
        {
            get { return emUso; }
            set
            {
                if (value != true && value != false)
                {
                    emUso = false;
                }
                else
                {
                    emUso = value;
                }
            }
        }

        private string morada;
        public string Morada
        {
            get { return morada; }
            set
            {
                if (value == null) morada = "";
                else morada = value;
            }
        }

        private string imagemUrl;
        public string ImagemUrl
        {
            get { return imagemUrl; }
            set
            {
                if (value == null) imagemUrl = "";
                else imagemUrl = value;
            }
        }

        public Sinal() { 
        
        }
        public Sinal(int id, string codigo, string desc, eTipoSinal tipo, eFormaSinal forma, bool emUso, string morada, string imagemUrl) {
            Id = id;
            Codigo = codigo;
            Descricao = desc;
            TipoSinal = tipo;
            FormaSinal = forma;
            EmUso = emUso;
            Morada = morada;
            ImagemUrl = imagemUrl;
        }

        public Sinal(string codigo, string desc, eTipoSinal tipo, eFormaSinal forma, bool emUso, string morada, string imagemUrl)
        {
            Codigo = codigo;
            Descricao = desc;
            TipoSinal = tipo;
            FormaSinal = forma;
            EmUso = emUso;
            Morada = morada;
            ImagemUrl = imagemUrl;
        }

        public Sinal(string codigo, string desc, eTipoSinal tipo, eFormaSinal forma, bool emUso, string imagemUrl)
        {
            Codigo = codigo;
            Descricao = desc;
            TipoSinal = tipo;
            FormaSinal = forma;
            EmUso = emUso;
            Morada = "";
            ImagemUrl = imagemUrl;
        }

        public Sinal(string codigo, string desc, eTipoSinal tipo, eFormaSinal forma, bool emUso)
        {
            Codigo = codigo;
            Descricao = desc;
            TipoSinal = tipo;
            FormaSinal = forma;
            EmUso = emUso;
            Morada = "";
            ImagemUrl = "";
        }

        public Sinal(string codigo, eTipoSinal tipo, eFormaSinal forma)
        {
            Codigo = codigo;
            Descricao = "";
            TipoSinal = tipo;
            FormaSinal = forma;
            EmUso = false;
            Morada = "";
            ImagemUrl = "";
        }

        public Sinal(string codigo, string imagemUrl)
        {
            Codigo = codigo;
            Descricao = "";
            TipoSinal = eTipoSinal.INDEFINIDO;
            FormaSinal = eFormaSinal.INDEFINIDO;
            EmUso = false;
            Morada = "";
            ImagemUrl = imagemUrl;
        }

        public Sinal(string codigo)
        {
            Codigo = codigo;
            Descricao = "";
            TipoSinal = eTipoSinal.INDEFINIDO;
            FormaSinal = eFormaSinal.INDEFINIDO;
            EmUso = false;
            Morada = "";
            ImagemUrl = "";
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