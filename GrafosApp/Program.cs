using GrafosApp.Models;
using GrafosApp.Algoritmos;
using GrafosApp.Utils;
using System;
using System.IO;

namespace GrafosApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("    ALGORITMO DE CHRISTOFIDES PARA TSP");
            Console.WriteLine("    Implementação do Trabalho Prático");
            Console.WriteLine("===============================================\n");

            try
            {
                string caminhoArquivo;
                
                // Verificar se foi passado arquivo como argumento
                if (args.Length > 0)
                {
                    caminhoArquivo = args[0];
                    Console.WriteLine($"Usando arquivo especificado: {caminhoArquivo}");
                }
                else
                {
                    caminhoArquivo = "exemplo.txt";
                    
                    // Criar arquivo de exemplo se não existir
                    if (!File.Exists(caminhoArquivo))
                    {
                        Console.WriteLine("Criando arquivo de exemplo...\n");
                        CarregadorGrafo.CriarArquivoExemplo(caminhoArquivo);
                    }
                }

                // Carregar grafo do arquivo
                Console.WriteLine($"Carregando grafo do arquivo: {caminhoArquivo}");
                var grafo = CarregadorGrafo.CarregarDeArquivo(caminhoArquivo);
                
                Console.WriteLine();
                grafo.ExibirGrafoPonderado();
                
                // Validar desigualdade triangular
                Console.WriteLine();
                bool valido = CarregadorGrafo.ValidarDesigualdadeTriangular(grafo);
                
                if (!valido)
                {
                    Console.WriteLine("ERRO: O grafo não satisfaz a desigualdade triangular!");
                    return;
                }

                Console.WriteLine();
                
                // Executar o algoritmo de Christofides
                var (cicloHamiltoniano, custoTotal) = AlgoritmoChristofides.ExecutarChristofides(grafo);

                // Exibir resultado final
                Console.WriteLine("\n===============================================");
                Console.WriteLine("              RESULTADO FINAL");
                Console.WriteLine("===============================================");
                Console.WriteLine($"Ciclo Hamiltoniano: {string.Join(" -> ", cicloHamiltoniano)}");
                Console.WriteLine($"Custo Total: {custoTotal:F2}");
                Console.WriteLine("===============================================");

                // Salvar resultado em arquivo
                SalvarResultado(caminhoArquivo, cicloHamiltoniano, custoTotal);

                // Demonstração adicional com exemplo maior (apenas se não foi passado arquivo)
                if (args.Length == 0)
                {
                    Console.WriteLine("\n\nDeseja testar com um exemplo maior? (s/n)");
                    var resposta = Console.ReadLine();
                    
                    if (resposta?.ToLower() == "s")
                    {
                        TestarExemploMaior();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                Console.WriteLine("\nVerifique se o arquivo de entrada está no formato correto:");
                Console.WriteLine("- Primeira linha: número de vértices");
                Console.WriteLine("- Próximas N linhas: matriz de adjacências com pesos");
                Console.WriteLine("\nUso: dotnet run [arquivo.txt]");
            }

            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }

        static void SalvarResultado(string arquivoEntrada, List<int> ciclo, double custo)
        {
            string arquivoSaida = Path.ChangeExtension(arquivoEntrada, ".resultado.txt");
            
            var conteudo = $"Arquivo de entrada: {arquivoEntrada}\n";
            conteudo += $"Algoritmo: Christofides\n";
            conteudo += $"Data/Hora: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n";
            conteudo += $"\nResultado:\n";
            conteudo += $"Ciclo Hamiltoniano: {string.Join(" -> ", ciclo)}\n";
            conteudo += $"Custo Total: {custo:F2}\n";
            
            File.WriteAllText(arquivoSaida, conteudo);
            Console.WriteLine($"\nResultado salvo em: {arquivoSaida}");
        }

        static void TestarExemploMaior()
        {
            Console.WriteLine("\n=== TESTANDO COM EXEMPLO MAIOR ===");
            
            string arquivoMaior = "exemplo_maior.txt";
            var conteudoMaior = @"5
0 29 20 21 16
29 0 15 17 28
20 15 0 28 14
21 17 28 0 25
16 28 14 25 0";

            File.WriteAllText(arquivoMaior, conteudoMaior);
            
            var grafoMaior = CarregadorGrafo.CarregarDeArquivo(arquivoMaior);
            
            Console.WriteLine();
            grafoMaior.ExibirGrafoPonderado();
            
            Console.WriteLine();
            CarregadorGrafo.ValidarDesigualdadeTriangular(grafoMaior);
            
            Console.WriteLine();
            var (ciclo, custo) = AlgoritmoChristofides.ExecutarChristofides(grafoMaior);
            
            Console.WriteLine("\n=== RESULTADO DO EXEMPLO MAIOR ===");
            Console.WriteLine($"Ciclo: {string.Join(" -> ", ciclo)}");
            Console.WriteLine($"Custo: {custo:F2}");
        }
    }
}
