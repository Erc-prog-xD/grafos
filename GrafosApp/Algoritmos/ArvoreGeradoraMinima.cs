using System;
using System.Collections.Generic;
using System.Linq;
using GrafosApp.Models;

namespace GrafosApp.Algoritmos
{
    /// <summary>
    /// Implementação do algoritmo de Kruskal para encontrar a Árvore Geradora Mínima
    /// </summary>
    public static class ArvoreGeradoraMinima
    {
        public static List<(int origem, int destino, double peso)> Kruskal(Grafo grafo)
        {
            var arestas = grafo.ObterTodasArestas().OrderBy(a => a.peso).ToList();
            var vertices = grafo.ObterVertices();
            var arvore = new List<(int, int, double)>();
            var conjuntoDisjunto = new UnionFind(vertices);

            foreach (var (origem, destino, peso) in arestas)
            {
                // Se os vértices não estão no mesmo conjunto, adiciona a aresta
                if (conjuntoDisjunto.Find(origem) != conjuntoDisjunto.Find(destino))
                {
                    conjuntoDisjunto.Union(origem, destino);
                    arvore.Add((origem, destino, peso));

                    // Se já temos n-1 arestas, paramos
                    if (arvore.Count == vertices.Count - 1)
                        break;
                }
            }

            return arvore;
        }

        public static Grafo ConstruirArvoreComoGrafo(List<(int origem, int destino, double peso)> arestas)
        {
            var arvore = new Grafo(false); // Árvore é não direcionada

            foreach (var (origem, destino, peso) in arestas)
            {
                arvore.AdicionarArestaPonderada(origem, destino, peso);
            }

            return arvore;
        }

        public static double CalcularCustoTotal(List<(int origem, int destino, double peso)> arestas)
        {
            return arestas.Sum(a => a.peso);
        }
    }

    /// <summary>
    /// Estrutura de dados Union-Find (Disjoint Set) para o algoritmo de Kruskal
    /// </summary>
    public class UnionFind
    {
        private Dictionary<int, int> pai;
        private Dictionary<int, int> rank;

        public UnionFind(List<int> vertices)
        {
            pai = new Dictionary<int, int>();
            rank = new Dictionary<int, int>();

            foreach (int v in vertices)
            {
                pai[v] = v;
                rank[v] = 0;
            }
        }

        public int Find(int x)
        {
            if (pai[x] != x)
            {
                pai[x] = Find(pai[x]); // Compressão de caminho
            }
            return pai[x];
        }

        public void Union(int x, int y)
        {
            int raizX = Find(x);
            int raizY = Find(y);

            if (raizX == raizY) return;

            // União por rank
            if (rank[raizX] < rank[raizY])
            {
                pai[raizX] = raizY;
            }
            else if (rank[raizX] > rank[raizY])
            {
                pai[raizY] = raizX;
            }
            else
            {
                pai[raizY] = raizX;
                rank[raizX]++;
            }
        }
    }
}
