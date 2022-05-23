using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoArquivosN2.Classes {
    class Produto {

        public Produto(double preco, string desc, Int16 categoriaID, double avaliacao, Int16 numeroAvaliacoes, Int16 fabricante) {
            this.Preco = preco;
            this.Descricao = desc;
            this.CategoriaID = categoriaID;
            this.Avaliacao = avaliacao;
            this.NumeroAvaliacoes = numeroAvaliacoes;
            this.FabricanteID = fabricante;
            this.TotalEmVendas = 0;
        }

        public double Preco { get; set; }
        public string Descricao { get; set; }
        public Int16 CategoriaID { get; set; }
        public double Avaliacao { get; set; }
        public Int16 NumeroAvaliacoes { get; set; }
        public Int16 FabricanteID { get; set; }


        public int NumeroVendas { get; set; }

        public double TotalEmVendas { get; set; }
        public Int16 Key_Temp { get; set; }

    }
}
