using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADS_TP01_SINAIS {
    internal class Repositorio {
        private List<Sinal> sinais;

        public Repositorio() {
            sinais = new List<Sinal>();
        }
        public int ContarSinais() { 
            return sinais.Count; 
        }
        public void AdicionarSinal(Sinal novoSinal) {
            sinais.Add(novoSinal);
        }

        public Sinal PesquisarSinal(int id) {
            return sinais.FirstOrDefault(s => s.Id == id);
        }

        public void RemoverSinal(int id) {
            Sinal sinal = PesquisarSinal(id);
            if (sinal != null) {
                sinais.Remove(sinal);
            }
        }
            
        public void AtualizarSinal(int id, Sinal novoSinal) {
            int index = sinais.FindIndex(s => s.Id == id);
            if (index != -1) {
                sinais[index] = novoSinal;
            }
        }

        public List<Sinal> FiltrarSinais(int? id = null, string codigo = null, string descricao = null, eCoresComuns? cor = null) {
            if (descricao != null)
                descricao = descricao.ToLower();

            if (codigo != null)
                codigo = codigo.ToLower();

            List<Sinal> sinaisFiltrados = new List<Sinal>();
            foreach (Sinal sinal in sinais) {
                string descricaoMin = sinal.Descricao.ToLower();
                string codigoMin = sinal.Codigo.ToLower();

                if ( (id == null || sinal.Id == id)
                    && (codigo == null || codigoMin == codigo)
                    && (descricao == null || descricaoMin.Contains(descricao))
                    && (cor == null || sinal.Cor == cor)) {
                    sinaisFiltrados.Add(sinal);
                }
            }
            return sinaisFiltrados;
        }
    }
}
