using System;
using System.Collections.Generic;
using System.Linq;
using GrafosApp.Models;

namespace GrafosApp.Algoritmos
{
    /// <summary>
    /// Implementação do algoritmo para encontrar ciclos eulerianos
    /// </summary>
    public static class CicloEuleriano
    {
        /// <summary>
        /// Verifica se o grafo tem um ciclo euleriano (todos os vértices têm grau par)
        /// </summary>
        public static bool TemCicloEuleriano(Grafo grafo)
        {
            var vertices = grafo.ObterVertices();
            
            foreach (int vertice in vertices)
            {
                if (grafo.ObterGrauVertice(vertice) % 2 != 0)
                {
                    return false;
                }
            }
            
            return vertices.Count > 0;
        }

        /// <summary>
        /// Encontra um ciclo euleriano usando o algoritmo de Hierholzer
        /// </summary>
        public static List<int> EncontrarCicloEuleriano(Grafo grafo)
        {
            if (!TemCicloEuleriano(grafo))
            {
                throw new InvalidOperationException("O grafo não possui ciclo euleriano");
            }

            // Criar uma cópia das arestas para modificação
            var arestasRestantes = CriarListaArestas(grafo);
            var vertices = grafo.ObterVertices();
            
            if (vertices.Count == 0)
                return new List<int>();

            // Iniciar com qualquer vértice
            int verticeInicial = vertices[0];
            var ciclo = new List<int>();
            var pilha = new Stack<int>();
            
            pilha.Push(verticeInicial);

            while (pilha.Count > 0)
            {
                int verticeAtual = pilha.Peek();
                
                // Encontrar uma aresta não usada
                var arestaDisponivel = arestasRestantes
                    .FirstOrDefault(a => a.origem == verticeAtual || a.destino == verticeAtual);

                if (arestaDisponivel != default)
                {
                    // Determinar o próximo vértice
                    int proximoVertice = arestaDisponivel.origem == verticeAtual 
                        ? arestaDisponivel.destino 
                        : arestaDisponivel.origem;

                    // Remover a aresta usada
                    arestasRestantes.Remove(arestaDisponivel);
                    
                    // Ir para o próximo vértice
                    pilha.Push(proximoVertice);
                }
                else
                {
                    // Não há mais arestas, adicionar vértice ao ciclo
                    ciclo.Add(pilha.Pop());
                }
            }

            // Reverter para obter a ordem correta
            ciclo.Reverse();
            
            return ciclo;
        }

        private static List<(int origem, int destino)> CriarListaArestas(Grafo grafo)
        {
            var arestas = new List<(int, int)>();
            var arestasProcessadas = new HashSet<string>();

            foreach (int origem in grafo.ObterVertices())
            {
                foreach (int destino in grafo.ObterVizinhos(origem))
                {
                    // Para grafos não direcionados, evitar duplicatas
                    string chave1 = $"{origem}-{destino}";
                    string chave2 = $"{destino}-{origem}";
                    
                    if (!arestasProcessadas.Contains(chave1) && !arestasProcessadas.Contains(chave2))
                    {
                        arestas.Add((origem, destino));
                        arestasProcessadas.Add(chave1);
                        arestasProcessadas.Add(chave2);
                    }
                }
            }

            return arestas;
        }

        /// <summary>
        /// Aplica shortcutting ao ciclo euleriano para obter um ciclo hamiltoniano
        /// </summary>
        public static List<int> AplicarShortcutting(List<int> cicloEuleriano)
        {
            if (cicloEuleriano.Count == 0)
                return new List<int>();

            var cicloHamiltoniano = new List<int>();
            var visitados = new HashSet<int>();

            foreach (int vertice in cicloEuleriano)
            {
                if (!visitados.Contains(vertice))
                {
                    cicloHamiltoniano.Add(vertice);
                    visitados.Add(vertice);
                }
            }

            // Fechar o ciclo voltando ao vértice inicial
            if (cicloHamiltoniano.Count > 1 && cicloHamiltoniano[0] != cicloHamiltoniano[cicloHamiltoniano.Count - 1])
            {
                cicloHamiltoniano.Add(cicloHamiltoniano[0]);
            }

            return cicloHamiltoniano;
        }
    }
}
