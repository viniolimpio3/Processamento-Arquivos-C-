using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoArquivosN2.Classes {
    class Categoria {


        public Categoria(string desc) {
            this.Description = desc;
            this.ProdutosVendidos = new List<Produto>();
        }

        public List<Produto> ProdutosVendidos { get; set; }

        public string Description { get; set; }


        public int NumeroVendas { get; set; }

        public Int16 Key_Temp { get; set; }

    }
}
