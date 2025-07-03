using System;
using System.Collections.Generic;
using System.Linq;
using GrafosApp.Models;
using GrafosApp.Algoritmos;

namespace GrafosApp.Algoritmos
{
    /// <summary>
    /// Implementação do Algoritmo de Christofides para o TSP métrico
    /// </summary>
    public static class AlgoritmoChristofides
    {
        /// <summary>
        /// Executa o algoritmo de Christofides completo
        /// </summary>
        public static (List<int> ciclo, double custo) ExecutarChristofides(Grafo grafoCompleto)
        {
            Console.WriteLine("=== ALGORITMO DE CHRISTOFIDES ===\n");

            // Etapa I: Construir árvore geradora mínima
            Console.WriteLine("Etapa I: Construindo árvore geradora mínima...");
            var arestasAGM = ArvoreGeradoraMinima.Kruskal(grafoCompleto);
            var arvore = ArvoreGeradoraMinima.ConstruirArvoreComoGrafo(arestasAGM);
            double custoAGM = ArvoreGeradoraMinima.CalcularCustoTotal(arestasAGM);
            Console.WriteLine($"AGM construída com custo: {custoAGM:F2}");
            Console.WriteLine($"Arestas da AGM: {arestasAGM.Count}");

            // Etapa II: Identificar vértices de grau ímpar
            Console.WriteLine("\nEtapa II: Identificando vértices de grau ímpar...");
            var verticesGrauImpar = IdentificarVerticesGrauImpar(arvore);
            Console.WriteLine($"Vértices de grau ímpar: [{string.Join(", ", verticesGrauImpar)}]");
            Console.WriteLine($"Quantidade: {verticesGrauImpar.Count}");

            // Etapa III: Emparelhamento perfeito de custo mínimo
            Console.WriteLine("\nEtapa III: Construindo emparelhamento perfeito...");
            var emparelhamento = EmparelhamentoPerfeitoMinimo(grafoCompleto, verticesGrauImpar);
            double custoEmparelhamento = CalcularCustoEmparelhamento(grafoCompleto, emparelhamento);
            Console.WriteLine($"Emparelhamento construído com custo: {custoEmparelhamento:F2}");
            Console.WriteLine($"Pares emparelhados: {emparelhamento.Count}");

            // Etapa IV: Unir AGM e emparelhamento
            Console.WriteLine("\nEtapa IV: Unindo AGM e emparelhamento...");
            var multigrafo = UnirAGMEmparelhamento(arvore, grafoCompleto, emparelhamento);
            Console.WriteLine("Multigrafo construído com todos os vértices de grau par");

            // Etapa V: Encontrar ciclo euleriano
            Console.WriteLine("\nEtapa V: Encontrando ciclo euleriano...");
            var cicloEuleriano = CicloEuleriano.EncontrarCicloEuleriano(multigrafo);
            Console.WriteLine($"Ciclo euleriano encontrado com {cicloEuleriano.Count} vértices");

            // Etapa VI: Aplicar shortcutting
            Console.WriteLine("\nEtapa VI: Aplicando shortcutting...");
            var cicloHamiltoniano = CicloEuleriano.AplicarShortcutting(cicloEuleriano);
            double custoCiclo = CalcularCustoCiclo(grafoCompleto, cicloHamiltoniano);
            Console.WriteLine($"Ciclo hamiltoniano obtido com custo: {custoCiclo:F2}");

            return (cicloHamiltoniano, custoCiclo);
        }

        private static List<int> IdentificarVerticesGrauImpar(Grafo arvore)
        {
            var verticesGrauImpar = new List<int>();
            
            foreach (int vertice in arvore.ObterVertices())
            {
                if (arvore.ObterGrauVertice(vertice) % 2 == 1)
                {
                    verticesGrauImpar.Add(vertice);
                }
            }
            
            return verticesGrauImpar.OrderBy(x => x).ToList();
        }

        /// <summary>
        /// Implementação simples de emparelhamento perfeito (algoritmo guloso)
        /// NOTA: Para uma implementação mais eficiente, usar biblioteca externa como sugerido no trabalho
        /// </summary>
        private static List<(int, int)> EmparelhamentoPerfeitoMinimo(Grafo grafo, List<int> vertices)
        {
            var emparelhamento = new List<(int, int)>();
            var verticesDisponiveis = new List<int>(vertices);

            while (verticesDisponiveis.Count >= 2)
            {
                int melhorOrigem = -1;
                int melhorDestino = -1;
                double menorCusto = double.MaxValue;

                // Encontrar o par com menor custo
                for (int i = 0; i < verticesDisponiveis.Count; i++)
                {
                    for (int j = i + 1; j < verticesDisponiveis.Count; j++)
                    {
                        double custo = grafo.ObterPesoAresta(verticesDisponiveis[i], verticesDisponiveis[j]);
                        if (custo < menorCusto)
                        {
                            menorCusto = custo;
                            melhorOrigem = verticesDisponiveis[i];
                            melhorDestino = verticesDisponiveis[j];
                        }
                    }
                }

                // Adicionar o melhor par ao emparelhamento
                if (melhorOrigem != -1)
                {
                    emparelhamento.Add((melhorOrigem, melhorDestino));
                    verticesDisponiveis.Remove(melhorOrigem);
                    verticesDisponiveis.Remove(melhorDestino);
                }
            }

            return emparelhamento;
        }

        private static double CalcularCustoEmparelhamento(Grafo grafo, List<(int, int)> emparelhamento)
        {
            double custo = 0;
            foreach (var (origem, destino) in emparelhamento)
            {
                custo += grafo.ObterPesoAresta(origem, destino);
            }
            return custo;
        }

        private static Grafo UnirAGMEmparelhamento(Grafo arvore, Grafo grafoOriginal, List<(int, int)> emparelhamento)
        {
            var multigrafo = new Grafo(false);

            // Adicionar todas as arestas da árvore
            foreach (int origem in arvore.ObterVertices())
            {
                foreach (int destino in arvore.ObterVizinhos(origem))
                {
                    if (origem < destino) // Evitar duplicatas
                    {
                        double peso = arvore.ObterPesoAresta(origem, destino);
                        multigrafo.AdicionarArestaPonderada(origem, destino, peso);
                    }
                }
            }

            // Adicionar arestas do emparelhamento
            foreach (var (origem, destino) in emparelhamento)
            {
                double peso = grafoOriginal.ObterPesoAresta(origem, destino);
                multigrafo.AdicionarArestaPonderada(origem, destino, peso);
            }

            return multigrafo;
        }

        private static double CalcularCustoCiclo(Grafo grafo, List<int> ciclo)
        {
            if (ciclo.Count < 2)
                return 0;

            double custo = 0;
            for (int i = 0; i < ciclo.Count - 1; i++)
            {
                custo += grafo.ObterPesoAresta(ciclo[i], ciclo[i + 1]);
            }

            return custo;
        }
    }
}
