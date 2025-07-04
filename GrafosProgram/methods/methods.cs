using System;
using System.IO;
using System.Text;

namespace Methods
{
    public static class GeradorGrafo
    {


        public static void GerarMatrizAleatoria(int tamanho)
        {
            Random random = new Random();
            double[,] matriz = new double[tamanho, tamanho];

            Console.WriteLine($"Gerando matriz de adjacências {tamanho}x{tamanho}...");

            // Preencher matriz
            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    if (i == j)
                    {
                        matriz[i, j] = 0; // Diagonal principal = 0
                    }
                    else if (i < j)
                    {
                        double peso = random.Next(10, 101);
                        matriz[i, j] = peso;
                        matriz[j, i] = peso; // Matriz simétrica
                    }
                }
            }

            matriz = AjustarDesigualdadeTriangular(matriz, tamanho);
            SalvarMatrizEmArquivo(matriz, tamanho);
        }

        private static double[,] AjustarDesigualdadeTriangular(double[,] matriz, int tamanho)
        {
            Console.WriteLine("Ajustando para satisfazer desigualdade triangular...");

            for (int k = 0; k < tamanho; k++)
            {
                for (int i = 0; i < tamanho; i++)
                {
                    for (int j = 0; j < tamanho; j++)
                    {
                        if (i != j && matriz[i, k] + matriz[k, j] < matriz[i, j])
                        {
                            matriz[i, j] = matriz[i, k] + matriz[k, j];
                        }
                    }
                }
            }
            return matriz;
        }
        public static (int tamanho, double[,] matriz) LerMatrizDeArquivo(string caminhoArquivo)
        {
            string[] linhas = File.ReadAllLines(caminhoArquivo);
            int tamanho = int.Parse(linhas[0]);
            double[,] matriz = new double[tamanho, tamanho];

            for (int i = 0; i < tamanho; i++)
            {
                string[] valores = linhas[i + 1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < tamanho; j++)
                {
                    matriz[i, j] = double.Parse(valores[j]);
                }
            }
            return (tamanho, matriz);
        }
        public static void ExibirMatriz(double[,] matriz, int tamanho)
        {
            Console.WriteLine("\nMatriz gerada:");

            // Cabeçalho
            Console.Write("     ");
            for (int i = 0; i < tamanho; i++)
            {
                Console.Write($"{i + 1,6}");
            }
            Console.WriteLine();

            // Linhas da matriz
            for (int i = 0; i < tamanho; i++)
            {
                Console.Write($"{i + 1,2} [ ");
                for (int j = 0; j < tamanho; j++)
                {
                    Console.Write($"{matriz[i, j],5:F0} ");
                }
                Console.WriteLine("]");
            }
        }

        public static void SalvarMatrizEmArquivo(double[,] matriz, int tamanho)
        {
            // Criar diretório se não existir
            string diretorio = "arquivoExemplo";
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            // Nome do arquivo
            string nomeArquivo = $"grafo_exemplo.txt";
            string caminhoCompleto = Path.Combine(diretorio, nomeArquivo);

            // Criar conteúdo
            StringBuilder conteudo = new StringBuilder();
            conteudo.AppendLine(tamanho.ToString());

            for (int i = 0; i < tamanho; i++)
            {
                string linha = "";
                for (int j = 0; j < tamanho; j++)
                {
                    if (j > 0) linha += " ";
                    linha += matriz[i, j].ToString("F0");
                }
                conteudo.AppendLine(linha);
            }

            File.WriteAllText(caminhoCompleto, conteudo.ToString());
        }


    }

}