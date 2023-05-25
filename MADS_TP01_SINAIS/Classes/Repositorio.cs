using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum eLocalizacaoSinal {
    INDIFERENTE,
    ARMAZEM,
    COLOCADO
}

namespace MADS_TP01_SINAIS {
    public class Repositorio {
        private List<Sinal> sinais;
        private List<Intervencao> intervencoes;
        private List<Acidente> acidentes;

        public Repositorio() {
            sinais = new List<Sinal>();
            intervencoes = new List<Intervencao>();
            acidentes = new List<Acidente>();
        }
        public int ContarSinais() {
            sinais = ObterListaSinais();

            return sinais.Count; 
        }
        public int ContarIntervencoes()
        {
            intervencoes = ObterListaIntervencoes();

            return intervencoes.Count;
        }
        public int ContarAcidentes()
        {
            acidentes = ObterListaAcidentes();

            return acidentes.Count;
        }
        public List<Sinal> ObterListaSinais()
        {
            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                string query = "SELECT id AS Id, codigo AS Codigo, descricao AS Descricao, tipo_sinal as TipoSinal, forma_sinal as FormaSinal, em_uso as EmUso, morada as Morada, imagem_url as ImagemUrl FROM sinal";
                return connection.Query<Sinal>(query).ToList();
            }
        }

        public List<Intervencao> ObterListaIntervencoes()
        {
            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                string query = "SELECT id AS Id, sinal_id AS IdSinal, descricao AS Descricao, morada as Morada, data_intervencao as DataIntervencao FROM intervencao";
                return connection.Query<Intervencao>(query).ToList();
            }
        }

        public List<Acidente> ObterListaAcidentes()
        {
            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                string query = "SELECT id AS Id, intervencao_id AS IdIntervencao, descricao AS Descricao, morada as Morada, data_acidente as DataAcidente FROM acidente";
                return connection.Query<Acidente>(query).ToList();
            }
        }

        public void LimparBD()
        {
            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                connection.Query<Acidente>($"DELETE FROM acidente");
                connection.Query<Intervencao>($"DELETE FROM intervencao");

                connection.Query<Sinal>($"DELETE FROM sinal");
            }


            acidentes.Clear();
            intervencoes.Clear();
            sinais.Clear();
        }

        public void LimparIntervencoesBD()
        {
            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                connection.Query<Intervencao>($"DELETE FROM intervencao");
                connection.Query<Acidente>($"DELETE FROM acidente");
            }

            intervencoes.Clear();
            acidentes.Clear();
        }

        public void LimparAcidentesBD()
        {
            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                connection.Query<Acidente>($"DELETE FROM acidente");
            }
            acidentes.Clear();
        }
        public int CalcularStock(string codigo, eLocalizacaoSinal localizacao = eLocalizacaoSinal.INDIFERENTE) {
            sinais = ObterListaSinais();

            int contagemTemp = 0;
            foreach (var oSinal in sinais) {
                if (oSinal == null)
                    continue;

                if (codigo != oSinal.Codigo)
                    continue;

                if ((localizacao == eLocalizacaoSinal.ARMAZEM && !oSinal.EmUso) ||
                    (localizacao == eLocalizacaoSinal.COLOCADO && oSinal.EmUso) ||
                    (localizacao == eLocalizacaoSinal.INDIFERENTE)) {
                        contagemTemp++;
                    }
                }
            
            return contagemTemp;
        }

        public void AdicionarSinal(Sinal novoSinal) {

            int idAtual = 0;
            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                string query = "INSERT INTO sinal (codigo, descricao, tipo_sinal, forma_sinal, em_uso, morada, imagem_url) " +
                               "VALUES (@Codigo, @Descricao, @TipoSinal, @FormaSinal, @EmUso, @Morada, @ImagemUrl)";

                connection.Execute(query, new
                {
                    novoSinal.Codigo,
                    novoSinal.Descricao,
                    novoSinal.TipoSinal,
                    novoSinal.FormaSinal,
                    novoSinal.EmUso,
                    novoSinal.Morada,
                    novoSinal.ImagemUrl
                });

                string selectQuery = "SELECT IDENT_CURRENT('sinal')";

                idAtual = connection.QuerySingle<int>(selectQuery);
            }

            novoSinal.Id = idAtual;
            sinais.Add(novoSinal);

        }

        public void AdicionarIntervencao(Sinal sinalIntervencao, Intervencao novaIntervencao)
        {

            int idAtual = 0;

            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                string query = "INSERT INTO intervencao (sinal_id, descricao, morada, data_intervencao) " +
                              "VALUES (@IdSinal, @Descricao, @Morada, @DataIntervencao)";

                connection.Execute(query, new
                {
                    IdSinal = sinalIntervencao.Id,
                    novaIntervencao.Descricao,
                    novaIntervencao.Morada,
                    novaIntervencao.DataIntervencao
                });

                string selectQuery = "SELECT IDENT_CURRENT('intervencao')";

                idAtual = connection.QuerySingle<int>(selectQuery);
            }

            novaIntervencao.Id = idAtual;
            intervencoes.Add(novaIntervencao);

        }

        public void AdicionarAcidente(Intervencao intervencaoAcidente, Acidente novoAcidente)
        {
            int idAtual = 0;
            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                string query = "INSERT INTO acidente (intervencao_id, descricao, morada, data_acidente) " +
                              "VALUES (@IdIntervencao, @Descricao, @Morada, @DataAcidente)";

                connection.Execute(query, new
                {
                    IdIntervencao = intervencaoAcidente.Id,
                    novoAcidente.Descricao,
                    novoAcidente.Morada,
                    novoAcidente.DataAcidente
                });
            }

            novoAcidente.Id = idAtual;
            acidentes.Add(novoAcidente);
        }

        public Sinal PesquisarSinal(int id)
        {
            sinais = ObterListaSinais();

            return sinais.FirstOrDefault(s => s.Id == id);
        }

        public Intervencao PesquisarIntervencao(int id)
        {
            intervencoes = ObterListaIntervencoes();

            return intervencoes.FirstOrDefault(s => s.Id == id);
        }

        public Acidente PesquisarAcidente(int id) {
            acidentes = ObterListaAcidentes();

            return acidentes.FirstOrDefault(s => s.Id == id);
        }

        public void RemoverSinal(int id) {
            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                string query = "DELETE FROM sinal WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }

            sinais = ObterListaSinais();
        }

        public void RemoverIntervencao(int id)
        {
            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                string query = "DELETE FROM intervencao WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }

            intervencoes = ObterListaIntervencoes();
        }

        public void RemoverAcidente(int id)
        {
            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                string query = "DELETE FROM acidente WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }

            acidentes = ObterListaAcidentes();
        }

        public void AtualizarSinal(int id, Sinal novoSinal) {
            sinais = ObterListaSinais();

            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                string query = "UPDATE sinal SET codigo = @Codigo, descricao = @Descricao, tipo_sinal = @TipoSinal, forma_sinal = @FormaSinal, em_uso = @EmUso, morada = @Morada, imagem_url = @ImagemUrl WHERE id = @Id";
                connection.Execute(query, new
                {
                    Id = id,
                    novoSinal.Codigo,
                    novoSinal.Descricao,
                    novoSinal.TipoSinal,
                    novoSinal.FormaSinal,
                    novoSinal.EmUso,
                    novoSinal.Morada,
                    novoSinal.ImagemUrl
                });
            }

            sinais = ObterListaSinais();
        }

        public void AtualizarIntervencao(int id, Intervencao novaIntervencao)
        {
            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                string query = "UPDATE intervencao SET descricao = @Descricao, descricao = @Descricao, data_intervencao = @DataIntervencao WHERE id = @Id";
                connection.Execute(query, new
                {
                    Id = id,
                    novaIntervencao.IdSinal,
                    novaIntervencao.Descricao,
                    novaIntervencao.Morada,
                    novaIntervencao.DataIntervencao
                });
            }

            intervencoes = ObterListaIntervencoes();
        }

        public void AtualizarAcidente(int id, Acidente novoAcidente)
        {
            using (IDbConnection connection = new SqlConnection(BaseDeDados.ConnectString("dbsinais")))
            {
                string query = "UPDATE acidente SET descricao = @Descricao, descricao = @Descricao, data_acidente = @DataAcidente WHERE id = @Id";
                connection.Execute(query, new
                {
                    Id = id,
                    novoAcidente.IdIntervencao,
                    novoAcidente.Descricao,
                    novoAcidente.Morada,
                    novoAcidente.DataAcidente
                });
            }

            acidentes = ObterListaAcidentes();
        }

        public List<Sinal> FiltrarSinais(int? id = null, string codigo = null, string descricao = null) {
            sinais = ObterListaSinais();

            if (descricao != null)
                descricao = descricao.ToLower();

            if (codigo != null)
                codigo = codigo.ToLower();

            List<Sinal> sinaisFiltrados = new List<Sinal>();
            foreach (Sinal sinal in sinais) {
                string descricaoMin = sinal.Descricao.ToLower();
                string codigoMin = sinal.Codigo.ToLower();

                if ((id == null || sinal.Id == id)
                    && (codigo == null || codigoMin == codigo)
                    && (descricao == null || descricaoMin.Contains(descricao))) {
                    sinaisFiltrados.Add(sinal);
                }
            }
            return sinaisFiltrados;
        }
    }
}
