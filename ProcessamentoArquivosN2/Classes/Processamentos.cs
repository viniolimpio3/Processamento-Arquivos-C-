using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoArquivosN2.Classes {
    static class Processamentos {

        public static DateTime Inicio { get; set; }
        public static TimeSpan Duracao { get; set; }

        /**
         * 
         * Principais Dicionarios
         * 
         * **/
        public static Dictionary<Int16, Categoria> Categorias = new Dictionary<Int16, Categoria>();
        public static Dictionary<Int16, Fabricante> Fabricantes = new Dictionary<Int16, Fabricante>();
        public static Dictionary<Int16, Produto> Produtos = new Dictionary<Int16, Produto>();
        public static Dictionary<string, Cliente> Clientes = new Dictionary<string, Cliente>();
        public static Dictionary<int, Venda> Vendas = new Dictionary<int, Venda>();


        /**
         * 
         * Dicionarios Secundários
         * 
         * **/
        //public static Dictionary<Int16, Produto> ProdutosEmVenda = new Dictionary<Int16, Produto>();
        //public static Dictionary<string, Cliente> ClientesEmVenda = new Dictionary<string, Cliente>();

        public static Dictionary<string, byte> NomesInseridos = new Dictionary<string, byte>();
        public static int NomesRepetidos = 0;

        public static int QtdProdutosVendidos = 0;
        public static int ClientesComComprasNoMesAnivesario = 0;

        public static Dictionary<string, Int16> ComprasAgrupadasPorMes = new Dictionary<string, Int16>();

        public static void Iniciar() {

            
            string filename = "resultado.txt";

            File.WriteAllText(filename, String.Empty); //Limpa o arquivo

            using (StreamWriter sw = new StreamWriter(filename)) {

                sw.WriteLine(Inicio.ToString("HH:mm:ss"));

                string a = A_ProdutosValidos();
                sw.WriteLine(a);
                Console.WriteLine("Finalizou Processamento a: " + a);

                var B = B_ClientesValidos();
                sw.WriteLine(B);
                Console.WriteLine("Finalizou Processamento B: " + B);

                var C = C_VendasValidas();
                sw.WriteLine(C);
                Console.WriteLine("Finalizou Processamento C: " + C);

                var D = D_ProdutosSemVenda();
                sw.WriteLine(D);
                Console.WriteLine("Finalizou Processamento D: " + D);

                var E = E_ClientesSemVenda();
                sw.WriteLine(E);
                Console.WriteLine("Finalizou Processamento E: " + E);

                var F = F_CategoriasSemProdutosVendidos();
                sw.WriteLine(F);
                Console.WriteLine("Finalizou Processamento F: " + F);

                var G = G_QtdProdutosVendidos();
                sw.WriteLine(G);
                Console.WriteLine("Finalizou Processamento G: " + G);

                var H = H_NomesRepetidos();
                sw.WriteLine(H);
                Console.WriteLine("Finalizou Processamento H: " + H);

                var J = J_ClientesComprasMesAniversario();
                sw.WriteLine(J);
                Console.WriteLine("Finalizou Processamento J: " + J);

                var K = K_ComprasAgrupadasPorMesAno();
                sw.WriteLine(K);
                Console.WriteLine("Finalizou Processamento K: " + K);

                //var L = L_ComprasMesMaiorVenda();
                //sw.WriteLine(L);
                //Console.WriteLine("Finalizou Processamento L: " + L);

                var M = M_ComprasMesMaiorVenda();
                sw.WriteLine(M);
                Console.WriteLine("Finalizou Processamento M: " + M);

                var N = N_VendaMaiorValor();
                sw.WriteLine(N);
                Console.WriteLine("Finalizou Processamento N: " + N);

                var O = O_5ClientesQueMaisCompraram();
                sw.WriteLine(O);
                Console.WriteLine("Finalizou Processamento O: " + O);

                var P = P_5FabricasQueMaisVenderam();
                sw.WriteLine(P);
                Console.WriteLine("Finalizou Processamento P: " + P);

                var Q = Q_5ProdutosMaisVendidos();
                sw.WriteLine(Q);
                Console.WriteLine("Finalizou Processamento Q: " + Q);

                var R = R_5CategoriasMaisVendidas();
                sw.WriteLine(R);
                Console.WriteLine("Finalizou Processamento R: " + R);

                var T = T_3ProdutosMaisVendidosComPiorAvaliacao();
                sw.WriteLine(T);
                Console.WriteLine("Finalizou Processamento T: " + T);


                Duracao = DateTime.Now.Subtract(Inicio);
                Console.WriteLine("Duração do processamento (s): " + Duracao.TotalSeconds);
                sw.WriteLine(Duracao.TotalSeconds);
            }

        }

        #region opCodes Methods
        public static string A_ProdutosValidos() {
            return $"A - {Produtos.Count}";
        }

        public static string B_ClientesValidos() {
            return $"B - {Clientes.Count}";
        }

        public static string C_VendasValidas() {
            return $"C - {Vendas.Count}";
        }

        public static string D_ProdutosSemVenda() {
            int qtd = 0;

            foreach (Produto p in Produtos.Values) {
                if (p.NumeroVendas == 0)
                    qtd++;
            }

            return $"D - {qtd}";
        }

        public static string E_ClientesSemVenda() {

            int qtd = 0;

            foreach (string clienteID in Clientes.Keys.ToList()) {
                if (Clientes[clienteID].TotalCompras == 0)
                    qtd++;
            }

            return $"E - {qtd}";
        }

        public static string F_CategoriasSemProdutosVendidos() {

            int qtd = 0;

            foreach (Categoria cat in Categorias.Values) {
                if (cat.ProdutosVendidos.Count == 0)
                    qtd++;
            }

            return $"F - {qtd}";
        }

        public static string G_QtdProdutosVendidos() {
            int qtd = 0;
            foreach (Produto p in Produtos.Values) {
                if (p.NumeroVendas > 0) qtd++;
            }
            return $"G - {qtd}";
        }

        public static string H_NomesRepetidos() {
            return $"H - {NomesRepetidos}";
        }

        public static string J_ClientesComprasMesAniversario() {
            return $"J - {ClientesComComprasNoMesAnivesario}";
        }
        public static string K_ComprasAgrupadasPorMesAno() {

            string retorno = "";

            foreach (string key in ComprasAgrupadasPorMes.Keys) {
                retorno += $"K - {key} - {ComprasAgrupadasPorMes[key]}" + Environment.NewLine;
            }

            return retorno;
        }

        public static string L_ComprasMesMaiorVenda() {

            string retorno = "";

            double maior = 0;

            var auxComprasAgrupadas = new Dictionary<string, double>();



            //foreach (var key in ComprasAgrupadasPorMes.Keys) {

            //    double valorMes = 0;

            //    foreach (Venda v in ComprasAgrupadasPorMes[key])
            //        valorMes += v.Preco;

            //    if (valorMes > maior) {
            //        maior = valorMes;

            //        //Reduzindo o número da próxima iteração, inserido no dictionary aux a seguir
            //        auxComprasAgrupadas.Add(key, valorMes);
            //    }
            //}

            //Verificando se possui um empate - removendo os que possuem valor menor que o maior valor por mês
            var finalDictionary = auxComprasAgrupadas;
            foreach (var k in auxComprasAgrupadas.Keys) {

                double valorVendidoMes = auxComprasAgrupadas[k];

                if (valorVendidoMes < maior)
                    finalDictionary.Remove(k);
            }

            foreach (string key in finalDictionary.Keys)
                retorno += $"{key} - {finalDictionary[key]}" + Environment.NewLine;



            return retorno;
        }

        public static string M_ComprasMesMaiorVenda() {

            string retorno = "";

            List<Produto> maisVendidos = new List<Produto>();

            int maiorNumeroVendas = 0;

            foreach (Int16 k in Produtos.Keys) {
                if (Produtos[k].NumeroVendas > maiorNumeroVendas) {
                    maiorNumeroVendas = Produtos[k].NumeroVendas;
                    Produtos[k].Key_Temp = k;
                    maisVendidos.Add(Produtos[k]);
                }
            }

            List<Produto> listaFinal = new List<Produto>();
            //Filtra a lista - com os produtos que tem numeroVendas == maiorNumeroVendas
            foreach (Produto produto in maisVendidos)
                if (produto.NumeroVendas == maiorNumeroVendas)
                    listaFinal.Add(produto);

            foreach(Produto produto in listaFinal)
                retorno += $"M - {produto.Key_Temp}|{produto.Descricao}|{produto.TotalEmVendas}" + Environment.NewLine;

            return retorno;

        }


        public static string N_VendaMaiorValor() {

            string retorno = "";

            double maior = 0;

            var aux = new Dictionary<int, Venda>();

            foreach (int k in Vendas.Keys) {
                if (Vendas[k].Preco > maior) {
                    maior = Vendas[k].Preco;
                    aux.Add(k, Vendas[k]);
                }
            }
            

            foreach (int key in aux.Keys) 
                if (aux[key].Preco < maior) 
                    aux.Remove(key);
            

            foreach(int key in aux.Keys) 
                retorno += $"N - {key}|{aux[key].Cliente.Key_Temp}|{aux[key].Preco}" + Environment.NewLine;

            return retorno;
        }


        public static string O_5ClientesQueMaisCompraram() {

            string retorno = "";

            Cliente[] maiores = new Cliente[5];

            foreach (string k in Clientes.Keys) {

                for (int i = 0; i < maiores.Length; i++) {

                    if (maiores[i] == null || Clientes[k].TotalCompras > maiores[i].TotalCompras) {

                        Cliente atual = maiores[i];

                        maiores[i] = Clientes[k];

                        maiores[i].Key_Temp = k;

                        //Joga para o próximo índice - se for menor que length - 2
                        if (i < maiores.Length - 1) 
                            maiores[i + 1] = atual;
                        
                        break;
                    }
                }
            }

            foreach (Cliente c in maiores)
                retorno += $"O - {c.Key_Temp}|{c.TotalCompras}" + Environment.NewLine;

            return retorno;
        }


        public static string P_5FabricasQueMaisVenderam() {

            string retorno = "";

            Fabricante[] maiores = new Fabricante[5];

            foreach (Int16 k in Fabricantes.Keys) {

                for (int i = 0; i < maiores.Length; i++) {

                    if (maiores[i] == null || Fabricantes[k].NumeroVendas > maiores[i].NumeroVendas) {

                        Fabricante atual = maiores[i];

                        maiores[i] = Fabricantes[k];
                        maiores[i].Key_Temp = k;

                        //Joga para o próximo índice - se for menor que length - 2
                        if (i < maiores.Length - 1)
                            maiores[i + 1] = atual;

                        break;
                    }
                }
            }

            foreach (Fabricante fabricante in maiores)
                retorno += $"P - {fabricante.Key_Temp}|{fabricante.Descricao}|{fabricante.NumeroVendas}" + Environment.NewLine;

            return retorno;
        }

        public static string Q_5ProdutosMaisVendidos() {

            string retorno = "";

            Produto[] maiores = new Produto[5];

            foreach (Int16 k in Produtos.Keys) {

                for (int i = 0; i < maiores.Length; i++) {

                    if (maiores[i] == null || Produtos[k].NumeroVendas > maiores[i].NumeroVendas) {

                        Produto atual = maiores[i];

                        maiores[i] = Produtos[k];
                        maiores[i].Key_Temp = k;

                        //Joga para o próximo índice - se for menor que length - 2
                        if (i < maiores.Length - 1)
                            maiores[i + 1] = atual;

                        break;
                    }
                }
            }

            foreach (Produto produto in maiores)
                retorno += $"Q - {produto.Key_Temp}|{produto.NumeroVendas}" + Environment.NewLine;

            return retorno;
        }

        public static string R_5CategoriasMaisVendidas() {

            string retorno = "";

            Categoria[] maiores = new Categoria[5];

            foreach (Int16 k in Categorias.Keys) {

                for (int i = 0; i < maiores.Length; i++) {

                    if (maiores[i] == null || Categorias[k].NumeroVendas > maiores[i].NumeroVendas) {

                        Categoria atual = maiores[i];

                        maiores[i] = Categorias[k];
                        maiores[i].Key_Temp = k;

                        //Joga para o próximo índice - se for menor que length - 2
                        if (i < maiores.Length - 1)
                            maiores[i + 1] = atual;

                        break;
                    }
                }
            }

            foreach (Categoria cat in maiores)
                retorno += $"R - {cat.Key_Temp}|{cat.NumeroVendas}" + Environment.NewLine;

            return retorno;
        }

        public static string T_3ProdutosMaisVendidosComPiorAvaliacao() {

            string retorno = "";

            Produto[] maiores = new Produto[3];

            foreach (Int16 k in Produtos.Keys) {

                for (int i = 0; i < maiores.Length; i++) {

                    //Condição: Produtos que tem maior número de vendas e menor avaliação
                    if (maiores[i] == null || (Produtos[k].NumeroVendas > maiores[i].NumeroVendas && Produtos[k].Avaliacao < maiores[i].Avaliacao)) {

                        Produto atual = maiores[i];

                        maiores[i] = Produtos[k];
                        maiores[i].Key_Temp = k;

                        //Joga para o próximo índice - se for menor que length - 2
                        if (i < maiores.Length - 1)
                            maiores[i + 1] = atual;

                        break;
                    }
                }
            }

            foreach (Produto produto in maiores)
                retorno += $"T - {produto.Key_Temp}|{produto.Avaliacao}|{produto.NumeroVendas}" + Environment.NewLine;

            return retorno;
        }

        #endregion
    }
}
