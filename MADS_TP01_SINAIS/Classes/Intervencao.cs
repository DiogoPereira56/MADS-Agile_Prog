using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MADS_TP01_SINAIS
{
    public class Intervencao
    {
        public int Id { get; set; }
        public int IdSinal { get; set; }
        public string Descricao { get; set; }
        public string Morada { get; set; }
        public DateTime DataIntervencao { get; set; }

        public Intervencao() { }

        public Intervencao(int id, int idSinal, string descricao, string morada, DateTime dataIntervencao)
        {
            Id = id;
            IdSinal = idSinal;
            Descricao = descricao;
            Morada = morada;
            DataIntervencao = dataIntervencao;
        }

        public Intervencao(int idSinal, string descricao, string morada, DateTime dataIntervencao)
        {
            IdSinal = idSinal;
            Descricao = descricao;
            Morada = morada;
            DataIntervencao = dataIntervencao;
        }
    }
}
