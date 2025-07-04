using System;
using Methods;
using algoritmos;

namespace GrafosProgram
{
    class Program
    {
        static void Main(string[] args)
        {

            // Definir tamanho da matriz
            Console.Write("Digite o tamanho da matriz de adjacências (número de vértices):");
            int x = int.Parse(Console.ReadLine());
            // Pega o tamanho x, gerar e salvar matriz em arquivo
            GeradorGrafo.GerarMatrizAleatoria(x);

            // Busca o arquivo gerado e lê a matriz
            string caminhoArquivo = "arquivoExemplo/grafo_exemplo.txt";
            var (tamanho, matriz) = GeradorGrafo.LerMatrizDeArquivo(caminhoArquivo);

            // Exibir matriz
            GeradorGrafo.ExibirMatriz(matriz, tamanho);

            // Executar o algoritmo de Kruskal para encontrar a Árvore Geradora Mínima (AGM)
            var agm = ArvoreGeradoraMinima.Kruskal(matriz, tamanho);
            ArvoreGeradoraMinima.ExibirAGM(agm);
            ArvoreGeradoraMinima.ExibirGraus(agm, tamanho);

            var impares = ArvoreGeradoraMinima.VerticesGrauImpar(agm, tamanho);
            Console.WriteLine("\nVértices de grau ímpar: " + string.Join(", ", impares));

            // Construir emparelhamento perfeito mínimo
            if (impares.Count > 0)
            {
                var (custo, emparelhamento) = EmparelhamentoPerfeito.EmparelhamentoPerfeitoMinimo(matriz, impares);
                EmparelhamentoPerfeito.ExibirEmparelhamento(emparelhamento, custo);
            }
            else
            {
                Console.WriteLine("\nTodos os vértices têm grau par. Nenhum emparelhamento necessário.");
            }
        }

    }
}