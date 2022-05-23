using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoArquivosN2.Classes {
    class Cliente {

        public Cliente(string nome, DateTime dataNasc) {
            this.Nome = nome;
            this.DataNasc = dataNasc;
            this.TotalCompras = 0;
        }

        public double TotalCompras { get; set; }
        public string Nome { get; set; }
        public DateTime DataNasc { get; set; }

        public string Key_Temp { get; set; }
    }
}
