using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADS_TP01_SINAIS
{
    public class BaseDeDados
    {
        public static string ConnectString(string name) {
            return "Data Source=LAPTOP\\SQLEXPRESS;Initial Catalog=dbsinais;Integrated Security=True";
        }
    }
}