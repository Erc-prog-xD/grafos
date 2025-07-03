using System;
using System.Collections.Generic;
using System.Linq;

namespace GrafosApp.Models
{
    public class Grafo
    {
        private Dictionary<int, List<int>> adjacencias;
        private Dictionary<int, Dictionary<int, double>> adjacenciasPonderadas;
        private bool ehDirecionado;

        public Grafo(bool direcionado = false)
        {
            adjacencias = new Dictionary<int, List<int>>();
            adjacenciasPonderadas = new Dictionary<int, Dictionary<int, double>>();
            ehDirecionado = direcionado;
        }

        public void AdicionarVertice(int vertice)
        {
            if (!adjacencias.ContainsKey(vertice))
            {
                adjacencias[vertice] = new List<int>();
            }
        }

        public void AdicionarAresta(int origem, int destino)
        {
            AdicionarVertice(origem);
            AdicionarVertice(destino);

            adjacencias[origem].Add(destino);

            if (!ehDirecionado)
            {
                adjacencias[destino].Add(origem);
            }
        }

        public void AdicionarArestaPonderada(int origem, int destino, double peso)
        {
            AdicionarVertice(origem);
            AdicionarVertice(destino);

            if (!adjacenciasPonderadas.ContainsKey(origem))
                adjacenciasPonderadas[origem] = new Dictionary<int, double>();
            if (!adjacenciasPonderadas.ContainsKey(destino))
                adjacenciasPonderadas[destino] = new Dictionary<int, double>();

            adjacenciasPonderadas[origem][destino] = peso;
            
            if (!ehDirecionado)
            {
                adjacenciasPonderadas[destino][origem] = peso;
            }

            // Para multigrafo, sempre adicionar as conexões
            adjacencias[origem].Add(destino);
            if (!ehDirecionado)
            {
                adjacencias[destino].Add(origem);
            }
        }

        public List<int> ObterVertices()
        {
            return adjacencias.Keys.ToList();
        }

        public List<int> ObterVizinhos(int vertice)
        {
            return adjacencias.ContainsKey(vertice) ? adjacencias[vertice] : new List<int>();
        }

        public double ObterPesoAresta(int origem, int destino)
        {
            if (adjacenciasPonderadas.ContainsKey(origem) && 
                adjacenciasPonderadas[origem].ContainsKey(destino))
            {
                return adjacenciasPonderadas[origem][destino];
            }
            return double.MaxValue; // Retorna infinito se não há aresta
        }

        public bool ExisteAresta(int origem, int destino)
        {
            return adjacenciasPonderadas.ContainsKey(origem) && 
                   adjacenciasPonderadas[origem].ContainsKey(destino);
        }

        public List<(int origem, int destino, double peso)> ObterTodasArestas()
        {
            var arestas = new List<(int, int, double)>();
            
            foreach (var origem in adjacenciasPonderadas.Keys)
            {
                foreach (var destino in adjacenciasPonderadas[origem].Keys)
                {
                    if (ehDirecionado || origem < destino) // Evita duplicatas em grafos não direcionados
                    {
                        arestas.Add((origem, destino, adjacenciasPonderadas[origem][destino]));
                    }
                }
            }
            
            return arestas;
        }

        public void ExibirGrafo()
        {
            Console.WriteLine($"Grafo {(ehDirecionado ? "Direcionado" : "Não Direcionado")}:");
            foreach (var vertice in adjacencias.Keys)
            {
                Console.Write($"Vértice {vertice}: ");
                Console.WriteLine(string.Join(", ", adjacencias[vertice]));
            }
        }

        public void ExibirGrafoPonderado()
        {
            Console.WriteLine($"Grafo Ponderado {(ehDirecionado ? "Direcionado" : "Não Direcionado")}:");
            foreach (var origem in adjacenciasPonderadas.Keys.OrderBy(x => x))
            {
                Console.Write($"Vértice {origem}: ");
                var conexoes = adjacenciasPonderadas[origem]
                    .Select(kvp => $"{kvp.Key}({kvp.Value:F1})")
                    .ToList();
                Console.WriteLine(string.Join(", ", conexoes));
            }
        }

        public int ObterGrauVertice(int vertice)
        {
            return adjacencias.ContainsKey(vertice) ? adjacencias[vertice].Count : 0;
        }

        public int ObterNumeroVertices()
        {
            return adjacencias.Count;
        }

        public int ObterNumeroArestas()
        {
            int totalArestas = adjacencias.Values.Sum(lista => lista.Count);
            return ehDirecionado ? totalArestas : totalArestas / 2;
        }
    }
}
