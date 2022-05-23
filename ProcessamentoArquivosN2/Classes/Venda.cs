using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoArquivosN2.Classes {
    class Venda {

        public Venda(Cliente cliente, Produto produto, DateTime hora, double preco) {

            this.Cliente = cliente;
            this.HoraVenda = hora;
            this.Preco = preco;

            //Se a lista de produtos ainda não foi instanciâda..
            //if(Produtos == null) this.Produtos = new Dictionary<Int16, Produto>();
            if (Produtos == null) this.Produtos = new List<Produto>();

            Produtos.Add(produto);
        }

        public DateTime HoraVenda { get; set; }
        public Cliente Cliente { get; set; }

        //Produtos da venda

        //public Dictionary<Int16, Produto> Produtos { get; set; }
        public List<Produto> Produtos { get; set; }
        //public Dictionary<Int16, byte> ProdutosIDs { get; set; }
        public double Preco { get; set; }

    }
}
