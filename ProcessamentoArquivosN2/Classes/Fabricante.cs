using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoArquivosN2.Classes {
    class Fabricante {

        public Fabricante(string desc) { 
            this.Descricao = desc;
            this.NumeroVendas = 0;
        }

        public Int16 Key_Temp { get; set; }

        public int NumeroVendas { get; set; }

        public string Descricao { get; set; }

    }
}
