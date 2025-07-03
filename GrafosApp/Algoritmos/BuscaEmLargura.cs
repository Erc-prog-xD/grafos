using System;
using System.Collections.Generic;

namespace GrafosApp.Algoritmos
{
    public static class BuscaEmLargura
    {
        public static List<int> BFS(Models.Grafo grafo, int verticeInicial)
        {
            var visitados = new HashSet<int>();
            var resultado = new List<int>();
            var fila = new Queue<int>();

            fila.Enqueue(verticeInicial);
            visitados.Add(verticeInicial);

            while (fila.Count > 0)
            {
                int verticeAtual = fila.Dequeue();
                resultado.Add(verticeAtual);

                foreach (int vizinho in grafo.ObterVizinhos(verticeAtual))
                {
                    if (!visitados.Contains(vizinho))
                    {
                        visitados.Add(vizinho);
                        fila.Enqueue(vizinho);
                    }
                }
            }

            return resultado;
        }

        public static Dictionary<int, int> BFSComDistancia(Models.Grafo grafo, int verticeInicial)
        {
            var visitados = new HashSet<int>();
            var distancias = new Dictionary<int, int>();
            var fila = new Queue<int>();

            fila.Enqueue(verticeInicial);
            visitados.Add(verticeInicial);
            distancias[verticeInicial] = 0;

            while (fila.Count > 0)
            {
                int verticeAtual = fila.Dequeue();

                foreach (int vizinho in grafo.ObterVizinhos(verticeAtual))
                {
                    if (!visitados.Contains(vizinho))
                    {
                        visitados.Add(vizinho);
                        distancias[vizinho] = distancias[verticeAtual] + 1;
                        fila.Enqueue(vizinho);
                    }
                }
            }

            return distancias;
        }
    }
}
