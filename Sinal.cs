using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum eCoresComuns
{
    VERMELHO,
    AZUL,
    VERDE,
    AMARELO,
    BRANCO
}

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
        public eCoresComuns Cor { get; set; }
        public eTipoSinal TipoSinal { get; set; }
        public eFormaSinal FormaSinal { get; set; }
        public string Codigo { get; set; }
        public int Stock { get; set; }
        public bool EmUso { get; set; }
        public string Morada { get; set; }
        public string ImagemUrl { get; set; }
        public Sinal(int id, string codigo, string desc, eCoresComuns cor, eTipoSinal tipo, eFormaSinal forma, int stock, bool emUso, string morada, string imagemUrl)
        {
            Id = id;
            Codigo = codigo;
            Descricao = desc;
            Cor = cor;
            TipoSinal = tipo;
            FormaSinal = forma;
            Stock = stock;
            EmUso = emUso;
            Morada = morada;
            ImagemUrl = imagemUrl;
        }

        public Sinal(string codigo, string desc, eCoresComuns cor, eTipoSinal tipo, eFormaSinal forma, int stock, bool emUso, string morada, string imagemUrl)
        : this(++contagemId, codigo, desc, cor, tipo, forma, stock, emUso, morada, imagemUrl) {
        }

        public Sinal(string codigo, string desc, eCoresComuns cor, eTipoSinal tipo, eFormaSinal forma, int stock, bool emUso, string imagemUrl)
        : this(++contagemId, codigo, desc, cor, tipo, forma, stock, emUso, "NA", imagemUrl) {
        }

        public Sinal(string codigo, string desc, eCoresComuns cor, eTipoSinal tipo, eFormaSinal forma, int stock, bool emUso)
            : this(++contagemId, codigo, desc, cor, tipo, forma, stock, emUso, "NA", "") {
        }

        public Sinal(string desc, eCoresComuns cor, eTipoSinal tipo, eFormaSinal forma)
            : this(++contagemId, "NA", desc, cor, tipo, forma, -1, false, "NA", "") {
        }

        public Sinal(eCoresComuns cor, eTipoSinal tipo, eFormaSinal forma)
            : this(++contagemId, "NA", "NA", cor, tipo, forma, -1, false, "NA", "") {
        }

        public static void Criar(Sinal novoSinal) {
            
        }
    }
}