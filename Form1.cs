using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MADS_TP01_SINAIS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Sinal sinal_1 = new Sinal(1, "H24", "Autoestrada", eCoresComuns.AZUL, eTipoSinal.INFORMACAO, eFormaSinal.RETANGULAR, 100, true);


            label1.Text = "Sinal adicionado ESEX: " + sinal_1.Descricao;
        }
    }
}
