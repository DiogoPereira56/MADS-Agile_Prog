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
    public class Acidente
    {
        public int Id { get; set; }
        public int IdIntervencao { get; set; }
        public string Descricao { get; set; }
        public string Morada { get; set; }
        public DateTime DataAcidente { get; set; }
        public Acidente() { }
        public Acidente(int id, int idAcidente, string descricao, string morada, DateTime dataAcidente)
        {
            Id = id;
            IdIntervencao = idAcidente;
            Descricao = descricao;
            Morada = morada;
            DataAcidente = dataAcidente;
        }
        public Acidente(int idAcidente, string descricao, string morada, DateTime dataAcidente)
        {
            IdIntervencao = idAcidente;
            Descricao = descricao;
            Morada = morada;
            DataAcidente = dataAcidente;
        }
    }
}