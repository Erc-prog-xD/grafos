using System;
using System.Collections.Generic;
using System.Linq;

namespace algoritmos
{
    public class Aresta
    {
        public int Origem;
        public int Destino;
        public double Peso;
        
        public Aresta(int origem, int destino, double peso)
        {
            Origem = origem;
            Destino = destino;
            Peso = peso;
        }
    }
    
    public static class ArvoreGeradoraMinima
    {
        public static List<Aresta> Kruskal(double[,] matriz, int vertices)
        {
            Console.WriteLine("Construindo Árvore Geradora Mínima...");
            
            // 1. Criar todas as arestas
            List<Aresta> arestas = new List<Aresta>();
            for (int i = 0; i < vertices; i++)
            {
                for (int j = i + 1; j < vertices; j++)
                {
                    arestas.Add(new Aresta(i, j, matriz[i, j]));
                }
            }
            
            // 2. Ordenar por peso
            arestas = arestas.OrderBy(a => a.Peso).ToList();
            
            // 3. Aplicar Kruskal
            List<Aresta> agm = new List<Aresta>();
            int[] parent = new int[vertices];
            
            // Inicializar union-find
            for (int i = 0; i < vertices; i++)
            {
                parent[i] = i;
            }
            
            foreach (var aresta in arestas)
            {
                if (Find(parent, aresta.Origem) != Find(parent, aresta.Destino))
                {
                    agm.Add(aresta);
                    Union(parent, aresta.Origem, aresta.Destino);
                    
                    Console.WriteLine($"Adicionada: {aresta.Origem + 1} - {aresta.Destino + 1} (peso: {aresta.Peso:F0})");
                    
                    if (agm.Count == vertices - 1)
                        break;
                }
            }
            
            return agm;
        }
        
        private static int Find(int[] parent, int x)
        {
            if (parent[x] != x)
                parent[x] = Find(parent, parent[x]);
            return parent[x];
        }
        
        private static void Union(int[] parent, int x, int y)
        {
            parent[Find(parent, x)] = Find(parent, y);
        }
        
        public static void ExibirAGM(List<Aresta> agm)
        {
            Console.WriteLine("\nÁrvore Geradora Mínima:");
            double custoTotal = 0;
            
            foreach (var aresta in agm)
            {
                Console.WriteLine($"  {aresta.Origem + 1} - {aresta.Destino + 1} (peso: {aresta.Peso:F0})");
                custoTotal += aresta.Peso;
            }
            
            Console.WriteLine($"Custo total: {custoTotal:F0}");
        }
        
        public static void ExibirGraus(List<Aresta> agm, int vertices)
        {
            int[] graus = new int[vertices];
            
            foreach (var aresta in agm)
            {
                graus[aresta.Origem]++;
                graus[aresta.Destino]++;
            }
            
            Console.WriteLine("\nGraus dos vértices:");
            for (int i = 0; i < vertices; i++)
            {
                Console.WriteLine($"  Vértice {i + 1}: grau {graus[i]}");
            }
        }
    }
}