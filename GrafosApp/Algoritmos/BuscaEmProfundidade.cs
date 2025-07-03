using System;
using System.Collections.Generic;

namespace GrafosApp.Algoritmos
{
    public static class BuscaEmProfundidade
    {
        public static List<int> DFS(Models.Grafo grafo, int verticeInicial)
        {
            var visitados = new HashSet<int>();
            var resultado = new List<int>();
            var pilha = new Stack<int>();

            pilha.Push(verticeInicial);

            while (pilha.Count > 0)
            {
                int verticeAtual = pilha.Pop();

                if (!visitados.Contains(verticeAtual))
                {
                    visitados.Add(verticeAtual);
                    resultado.Add(verticeAtual);

                    var vizinhos = grafo.ObterVizinhos(verticeAtual);
                    // Adiciona vizinhos em ordem reversa para manter a ordem
                    for (int i = vizinhos.Count - 1; i >= 0; i--)
                    {
                        if (!visitados.Contains(vizinhos[i]))
                        {
                            pilha.Push(vizinhos[i]);
                        }
                    }
                }
            }

            return resultado;
        }

        public static void DFSRecursivo(Models.Grafo grafo, int vertice, HashSet<int> visitados, List<int> resultado)
        {
            visitados.Add(vertice);
            resultado.Add(vertice);

            foreach (int vizinho in grafo.ObterVizinhos(vertice))
            {
                if (!visitados.Contains(vizinho))
                {
                    DFSRecursivo(grafo, vizinho, visitados, resultado);
                }
            }
        }
    }
}
