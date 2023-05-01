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
        public int Id { get; set; }
        public string Descricao { get; set; }
        public eCoresComuns Cor { get; set; }
        public eTipoSinal TipoSinal { get; set; }
        public eFormaSinal FormaSinal { get; set; }
        public string Codigo { get; set; }
        public int Stock { get; set; }
        public bool EmUso { get; set; }
        public string Morada { get; set; }

        public Sinal(int id, string codigo, string desc, eCoresComuns cor, eTipoSinal tipo, eFormaSinal forma, int stock, bool emUso, string morada) {
            Id = id;
            Codigo = codigo;
            Descricao = desc;
            Cor = cor;
            TipoSinal = tipo;
            FormaSinal = forma;
            Stock = stock;
            EmUso = emUso;
            Morada = morada;
        }

        public Sinal(int id, string codigo, string desc, eCoresComuns cor, eTipoSinal tipo, eFormaSinal forma, int stock, bool emUso)
        {
            Id = id;
            Codigo = codigo;
            Descricao = desc;
            Cor = cor;
            TipoSinal = tipo;
            FormaSinal = forma;
            Stock = stock;
            EmUso = emUso;
            Morada = "NA";
        }

        public Sinal(int id, string codigo, string desc, eCoresComuns cor, eTipoSinal tipo, eFormaSinal forma) {
            Id = id;
            Codigo = "NA";
            Descricao = desc;
            Cor = cor;
            TipoSinal = tipo;
            FormaSinal = forma;
            Stock = -1;
            EmUso = false;
            Morada = "NA";
        }

        public Sinal(int id, eCoresComuns cor, eTipoSinal tipo, eFormaSinal forma) {
            Id = id;
            Codigo = "NA";
            Descricao = "NA";
            Cor = cor;
            TipoSinal = tipo;
            FormaSinal = forma;
            Stock = -1;
            EmUso = false;
            Morada = "NA";
        }

        public static void Criar(Sinal novoSinal) {
            //repositorio.Add(novoSinal);
            Console.WriteLine("Sinal adicionado: " + novoSinal.Descricao);
        }
    }

}