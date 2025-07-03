using System;
using System.IO;
using GrafosApp.Models;

namespace GrafosApp.Utils
{
    /// <summary>
    /// Utilitários para carregar grafos de arquivos
    /// </summary>
    public static class CarregadorGrafo
    {
        /// <summary>
        /// Carrega um grafo completo de um arquivo .txt no formato especificado
        /// Primeira linha: número de vértices N
        /// Próximas N linhas: matriz de adjacências com pesos
        /// </summary>
        public static Grafo CarregarDeArquivo(string caminhoArquivo)
        {
            if (!File.Exists(caminhoArquivo))
            {
                throw new FileNotFoundException($"Arquivo não encontrado: {caminhoArquivo}");
            }

            var linhas = File.ReadAllLines(caminhoArquivo);
            
            if (linhas.Length == 0)
            {
                throw new InvalidDataException("Arquivo vazio");
            }

            // Primeira linha contém o número de vértices
            if (!int.TryParse(linhas[0].Trim(), out int numVertices))
            {
                throw new InvalidDataException("Primeira linha deve conter um número inteiro válido");
            }

            if (linhas.Length != numVertices + 1)
            {
                throw new InvalidDataException($"Esperado {numVertices + 1} linhas, encontrado {linhas.Length}");
            }

            var grafo = new Grafo(false); // TSP usa grafos não direcionados

            // Processar matriz de adjacências
            for (int i = 0; i < numVertices; i++)
            {
                var valores = linhas[i + 1].Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                
                if (valores.Length != numVertices)
                {
                    throw new InvalidDataException($"Linha {i + 2}: esperado {numVertices} valores, encontrado {valores.Length}");
                }

                for (int j = 0; j < numVertices; j++)
                {
                    if (!double.TryParse(valores[j], out double peso))
                    {
                        throw new InvalidDataException($"Valor inválido na linha {i + 2}, coluna {j + 1}: {valores[j]}");
                    }

                    // Adicionar aresta apenas se i < j para evitar duplicatas e i != j para evitar auto-loops
                    if (i < j && peso > 0)
                    {
                        grafo.AdicionarArestaPonderada(i + 1, j + 1, peso); // Vértices numerados de 1 a N
                    }
                }
            }

            Console.WriteLine($"Grafo carregado com sucesso:");
            Console.WriteLine($"- Vértices: {numVertices}");
            Console.WriteLine($"- Arestas: {grafo.ObterNumeroArestas()}");
            
            return grafo;
        }

        /// <summary>
        /// Valida se o grafo satisfaz a desigualdade triangular
        /// </summary>
        public static bool ValidarDesigualdadeTriangular(Grafo grafo)
        {
            var vertices = grafo.ObterVertices();
            
            Console.WriteLine("Validando desigualdade triangular...");
            
            foreach (int i in vertices)
            {
                foreach (int j in vertices)
                {
                    if (i == j) continue;
                    
                    foreach (int k in vertices)
                    {
                        if (k == i || k == j) continue;
                        
                        double pesoIJ = grafo.ObterPesoAresta(i, j);
                        double pesoIK = grafo.ObterPesoAresta(i, k);
                        double pesoKJ = grafo.ObterPesoAresta(k, j);
                        
                        // Verifica se c(i,j) <= c(i,k) + c(k,j)
                        if (pesoIJ > pesoIK + pesoKJ + 1e-9) // Pequena tolerância para erros de ponto flutuante
                        {
                            Console.WriteLine($"Desigualdade triangular violada: c({i},{j})={pesoIJ:F2} > c({i},{k})+c({k},{j})={pesoIK:F2}+{pesoKJ:F2}={pesoIK + pesoKJ:F2}");
                            return false;
                        }
                    }
                }
            }
            
            Console.WriteLine("Desigualdade triangular validada com sucesso!");
            return true;
        }

        /// <summary>
        /// Cria um arquivo de exemplo para teste
        /// </summary>
        public static void CriarArquivoExemplo(string caminhoArquivo)
        {
            var conteudo = @"4
0 10 15 20
10 0 25 30
15 25 0 35
20 30 35 0";

            File.WriteAllText(caminhoArquivo, conteudo);
            Console.WriteLine($"Arquivo de exemplo criado: {caminhoArquivo}");
        }
    }
}
