using System;
using System.Globalization;
using ProcessamentoArquivosN2.Classes;
namespace ProcessamentoArquivosN2 {
    class Program {


        static void Main(string[] args) {

            Console.WriteLine("Pressione Qualquer tecla para inicar o processamento");
            Console.ReadKey();

            Processamentos.Inicio = DateTime.Now;

            LeFabricantes();
            LeCategorias();
            LeProdutos();
            LeClientes();
            LeVendas();

            Processamentos.Iniciar();

            Console.WriteLine($"Tempo de execução: {Processamentos.Duracao.TotalSeconds} s");
        }



        static void LeFabricantes() {

            try {

                using (StreamReader sr = new StreamReader("fabricantes.txt")) {

                    string line;

                    while ((line = sr.ReadLine()) != null) {

                        try {

                            string[] arr = line.Split("|");

                            Fabricante f = new Fabricante(arr[1]);

                            Processamentos.Fabricantes.Add(Convert.ToInt16(arr[0]), f);

                        } catch (Exception e) {
                            Console.WriteLine("Exceção ao inserir Fabricante; " + " " + e.Message);
                        }

                    }

                }

            } catch (Exception ex) {

                Console.WriteLine(ex.Message);

            }

        }

        static void LeCategorias() {

            try {

                using (StreamReader sr = new StreamReader("categorias.txt")) {

                    string line;

                    while ((line = sr.ReadLine()) != null) {

                        try {

                            string[] arr = line.Split("|");

                            Categoria categoria = new Categoria(arr[1]);

                            Processamentos.Categorias.Add(Convert.ToInt16(arr[0]), categoria);

                        } catch (Exception e) {
                            Console.WriteLine("Exceção ao inserir Categorias; " + " " + e.Message);
                        }

                    }

                }

            } catch (Exception ex) {

                Console.WriteLine(ex.Message);

            }

        }


        static void LeProdutos() {

            try {

                using (StreamReader sr = new StreamReader("produtos.txt")) {

                    string line;

                    while ((line = sr.ReadLine()) != null) {

                        try {

                            string[] arr = line.Split("|");

                            // Verificando se produto é válido!
                            Int16 fabricanteID = Convert.ToInt16(arr[6]);
                            Int16 categoriaID = Convert.ToInt16(arr[3]);
                            if (!Processamentos.Fabricantes.ContainsKey(fabricanteID) || !Processamentos.Categorias.ContainsKey(categoriaID))
                                continue;

                            //Caso o número de avaliações venha como ""
                            if (arr[5] == String.Empty)
                                arr[5] = "0";

                            //Removendo "," do nº de avaliações!
                            arr[5] = arr[5].Replace(",", String.Empty);

                            double preco = Convert.ToDouble(arr[1]);
                            double avaliacao = Convert.ToDouble(arr[4]);
                            Int16 numAvaliacoes = Convert.ToInt16(arr[5]);

                            Produto produto = new Produto(preco, arr[1], categoriaID, avaliacao, numAvaliacoes, fabricanteID);
                            Int16 produtoId = Convert.ToInt16(arr[0]);
                            produto.Key_Temp = produtoId;

                            Processamentos.Produtos.Add(produtoId, produto);

                        } catch (Exception e) {
                            Console.WriteLine("Exceção ao inserir Produto; " + " " + e.Message + line.ToString());
                        }

                    }

                }

            } catch (Exception ex) {

                Console.WriteLine(ex.Message);

            }

        }


        static void LeClientes() {

            try {

                using (StreamReader sr = new StreamReader("clientes.txt")) {

                    string line;

                    while ((line = sr.ReadLine()) != null) {

                        try {

                            string[] arr = line.Split("|");

                            //Caso seja duplicidade de clientes: ignorar
                            if (Processamentos.Clientes.ContainsKey(arr[0]))
                                continue;

                            DateTime dataNasc = DateTime.ParseExact(arr[2], "yyyyMMdd", CultureInfo.InvariantCulture);
                            string nome = arr[1];

                            Cliente cliente = new Cliente(nome, dataNasc);
                            cliente.Key_Temp = arr[0];
                            Processamentos.Clientes.Add(arr[0], cliente);


                            if (Processamentos.NomesInseridos.ContainsKey(nome))
                                Processamentos.NomesRepetidos++;
                            else 
                                Processamentos.NomesInseridos.Add(nome, (byte)0);



                        } catch (Exception e) {
                            Console.WriteLine("Exceção ao inserir Cliente; " + " " + e.Message + line.ToString());
                        }

                    }

                }

                Processamentos.NomesInseridos.Clear();//Necessário apenas enquanto estava lendo - para somar os nomes repetidos!

            } catch (Exception ex) {

                Console.WriteLine(ex.Message);

            }

        }

        static void LeVendas() {

            try {

                using (StreamReader sr = new StreamReader("vendas.txt")) {

                    string line;

                    int l = 0;
                    Venda venda;
                    DateTime dataVenda;

                    while ((line = sr.ReadLine()) != null) {

                        l++;

                        if(l == 10000000)
                            Console.WriteLine($"10M {Processamentos.Vendas.Count}");
                        else if (l == 20000000)
                            Console.WriteLine($"20M {Processamentos.Vendas.Count}");
                        else if (l == 30000000)
                            Console.WriteLine($"30M {Processamentos.Vendas.Count}");
                        else if (l == 40000000)
                            Console.WriteLine($"40M {Processamentos.Vendas.Count}");

                        string[] arr = line.Split("|");

                        Int16 produtoID = Convert.ToInt16(arr[2]);
                        string clienteID = arr[1];

                        //Verifica se é uma venda válida: cliente e produto válidos!
                        if (!Processamentos.Clientes.ContainsKey(clienteID) || !Processamentos.Produtos.ContainsKey(produtoID))
                            continue;
                            
                        int vendaID = Convert.ToInt32(arr[0]);
                        dataVenda = DateTime.ParseExact(arr[3], "yyyyMMddHHmmss", CultureInfo.InvariantCulture);


                        double preco = Convert.ToDouble(arr[4]);
                        if (!Processamentos.Vendas.ContainsKey(vendaID)) {

                            venda = new Venda(Processamentos.Clientes[clienteID], Processamentos.Produtos[produtoID], dataVenda, preco);
                            Processamentos.Vendas.Add(vendaID, venda);

                        }else {                               

                            venda = Processamentos.Vendas[vendaID];
                            
                            Processamentos.Vendas[vendaID].Produtos.Add(Processamentos.Produtos[produtoID]);

                            string key = $"{dataVenda.Month}/{dataVenda.Year}";
                            if (!Processamentos.ComprasAgrupadasPorMes.ContainsKey(key))
                                Processamentos.ComprasAgrupadasPorMes.Add(key, 0);

                            Processamentos.ComprasAgrupadasPorMes[key]++;

                            if (Processamentos.Clientes[clienteID].DataNasc.ToString("MM/dd") == dataVenda.ToString("MM/dd"))
                                Processamentos.ClientesComComprasNoMesAnivesario++;

                        }

                        //Processamentos Secundários - para os opCodes
                        Int16 categoriaID = Processamentos.Produtos[produtoID].CategoriaID;
                        if (Processamentos.Categorias.ContainsKey(categoriaID))
                            Processamentos.Categorias[categoriaID].ProdutosVendidos.Add(Processamentos.Produtos[produtoID]);

                        Processamentos.Clientes[clienteID].TotalCompras += preco;

                        //Número vendas de fabricantes
                        Processamentos.Fabricantes[Processamentos.Produtos[produtoID].FabricanteID].NumeroVendas++;

                        //Número vendas de Produtos
                        Processamentos.Produtos[produtoID].NumeroVendas++;
                        Processamentos.Produtos[produtoID].TotalEmVendas += preco;


                        //Número vendas de Categorias
                        Processamentos.Categorias[categoriaID].NumeroVendas++;

                    }

                    Console.WriteLine("Finalizou o processamento do arquivo de Vendas");
                }

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

        }

    }
}
