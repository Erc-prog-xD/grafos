using System;
using System.Collections.Generic;
using System.Linq;

namespace algoritmos
{
    /// <summary>
    /// Representa uma aresta do grafo, com origem, destino e peso.
    /// </summary>
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

    /// <summary>
    /// Classe estática com métodos para calcular e exibir a Árvore Geradora Mínima (AGM) usando o algoritmo de Kruskal.
    /// </summary>
    public static class ArvoreGeradoraMinima
    {
        /// <summary>
        /// Executa o algoritmo de Kruskal para encontrar a AGM de um grafo representado por matriz de adjacência.
        /// </summary>
        /// <param name="matriz">Matriz de adjacência do grafo.</param>
        /// <param name="vertices">Número de vértices do grafo.</param>
        /// <returns>Lista de arestas que compõem a AGM.</returns>
        public static List<Aresta> Kruskal(double[,] matriz, int vertices)
        {
            Console.WriteLine("Construindo Árvore Geradora Mínima...");

            // 1. Criar todas as arestas do grafo (apenas metade superior, pois é não direcionado)
            List<Aresta> arestas = new List<Aresta>();
            for (int i = 0; i < vertices; i++)
            {
                for (int j = i + 1; j < vertices; j++)
                {
                    arestas.Add(new Aresta(i, j, matriz[i, j]));
                }
            }

            // 2. Ordenar as arestas por peso crescente
            arestas = arestas.OrderBy(a => a.Peso).ToList();

            // 3. Inicializar estruturas para union-find
            List<Aresta> agm = new List<Aresta>();
            int[] parent = new int[vertices];
            for (int i = 0; i < vertices; i++)
            {
                parent[i] = i;
            }

            // 4. Percorrer as arestas ordenadas e adicionar à AGM se não formar ciclo
            foreach (var aresta in arestas)
            {
                if (Find(parent, aresta.Origem) != Find(parent, aresta.Destino))
                {
                    agm.Add(aresta);
                    Union(parent, aresta.Origem, aresta.Destino);

                    Console.WriteLine($"Adicionada: {aresta.Origem + 1} - {aresta.Destino + 1} (peso: {aresta.Peso:F0})");

                    // Se já tem (n-1) arestas, a AGM está completa
                    if (agm.Count == vertices - 1)
                        break;
                }
            }

            return agm;
        }

        /// <summary>
        /// Função auxiliar do union-find: encontra o representante do conjunto de x.
        /// </summary>
        private static int Find(int[] parent, int x)
        {
            if (parent[x] != x)
                parent[x] = Find(parent, parent[x]);
            return parent[x];
        }

        /// <summary>
        /// Função auxiliar do union-find: une os conjuntos de x e y.
        /// </summary>
        private static void Union(int[] parent, int x, int y)
        {
            parent[Find(parent, x)] = Find(parent, y);
        }

        /// <summary>
        /// Exibe as arestas da Árvore Geradora Mínima e o custo total.
        /// </summary>
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

        /// <summary>
        /// Exibe o grau (número de conexões) de cada vértice na AGM.
        /// </summary>
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

        public static List<int> VerticesGrauImpar(List<Aresta> agm, int vertices)
        {
            int[] graus = new int[vertices];
            foreach (var aresta in agm)
            {
                graus[aresta.Origem]++;
                graus[aresta.Destino]++;
            }
            List<int> impares = new List<int>();
            for (int i = 0; i < vertices; i++)
            {
                if (graus[i] % 2 != 0)
                    impares.Add(i + 1); // +1 para exibir no padrão 1..n
            }
            return impares;
        }
    }

    public static class EmparelhamentoPerfeito
    {
        /// <summary>
        /// Encontra o emparelhamento perfeito de custo mínimo usando força bruta otimizada.
        /// </summary>
        /// <param name="matriz">Matriz de adjacência original do grafo.</param>
        /// <param name="vertices">Lista de vértices de grau ímpar.</param>
        /// <returns>Tupla com o custo total e lista de pares emparelhados.</returns>
        public static (double custo, List<(int, int)> emparelhamento) EmparelhamentoPerfeitoMinimo(double[,] matriz, List<int> vertices)
        {
            if (vertices.Count % 2 != 0)
            {
                throw new ArgumentException("O número de vértices deve ser par para formar um emparelhamento perfeito.");
            }

            if (vertices.Count == 0)
            {
                return (0, new List<(int, int)>());
            }

            double melhorCusto = double.MaxValue;
            List<(int, int)> melhorEmparelhamento = new List<(int, int)>();

            // Gera todas as permutações possíveis de emparelhamento
            var emparelhamentos = GerarEmparelhamentos(vertices);

            foreach (var emparelhamento in emparelhamentos)
            {
                double custo = CalcularCustoEmparelhamento(matriz, emparelhamento);
                if (custo < melhorCusto)
                {
                    melhorCusto = custo;
                    melhorEmparelhamento = emparelhamento;
                }
            }

            return (melhorCusto, melhorEmparelhamento);
        }

        /// <summary>
        /// Gera todos os emparelhamentos possíveis para um conjunto de vértices.
        /// </summary>
        private static List<List<(int, int)>> GerarEmparelhamentos(List<int> vertices)
        {
            var emparelhamentos = new List<List<(int, int)>>();

            if (vertices.Count == 0)
            {
                emparelhamentos.Add(new List<(int, int)>());
                return emparelhamentos;
            }

            if (vertices.Count == 2)
            {
                emparelhamentos.Add(new List<(int, int)> { (vertices[0], vertices[1]) });
                return emparelhamentos;
            }

            int primeiro = vertices[0];
            var restantes = vertices.Skip(1).ToList();

            // Para cada vértice restante, emparelha com o primeiro
            for (int i = 0; i < restantes.Count; i++)
            {
                int parceiro = restantes[i];
                var novosRestantes = restantes.Where((v, index) => index != i).ToList();

                // Gera emparelhamentos recursivamente para os vértices restantes
                var subEmparelhamentos = GerarEmparelhamentos(novosRestantes);

                foreach (var subEmparelhamento in subEmparelhamentos)
                {
                    var novoEmparelhamento = new List<(int, int)> { (primeiro, parceiro) };
                    novoEmparelhamento.AddRange(subEmparelhamento);
                    emparelhamentos.Add(novoEmparelhamento);
                }
            }

            return emparelhamentos;
        }

        /// <summary>
        /// Calcula o custo total de um emparelhamento.
        /// </summary>
        private static double CalcularCustoEmparelhamento(double[,] matriz, List<(int, int)> emparelhamento)
        {
            double custo = 0;
            foreach (var (v1, v2) in emparelhamento)
            {
                custo += matriz[v1 - 1, v2 - 1]; // Ajusta para índice base 0
            }
            return custo;
        }

        /// <summary>
        /// Exibe o emparelhamento perfeito mínimo.
        /// </summary>
        public static void ExibirEmparelhamento(List<(int, int)> emparelhamento, double custo)
        {
            Console.WriteLine("\nEmparelhamento Perfeito Mínimo:");
            if (emparelhamento.Count == 0)
            {
                Console.WriteLine("  Nenhum emparelhamento necessário (todos os vértices têm grau par).");
                return;
            }

            foreach (var (v1, v2) in emparelhamento)
            {
                Console.WriteLine($"  {v1} - {v2}");
            }
            Console.WriteLine($"Custo total do emparelhamento: {custo:F0}");
        }

        /// <summary>
        /// Implementação alternativa usando programação dinâmica com bitmask para conjuntos menores.
        /// Mais eficiente que força bruta para conjuntos de até 20 vértices.
        /// </summary>
        public static (double custo, List<(int, int)> emparelhamento) EmparelhamentoPerfeitoMinimoBitmask(double[,] matriz, List<int> vertices)
        {
            int n = vertices.Count;
            if (n % 2 != 0)
            {
                throw new ArgumentException("O número de vértices deve ser par para formar um emparelhamento perfeito.");
            }

            if (n == 0)
            {
                return (0, new List<(int, int)>());
            }

            // dp[mask] = custo mínimo para emparelhar os vértices representados pela máscara
            var dp = new double[1 << n];
            var parent = new int[1 << n];

            Array.Fill(dp, double.MaxValue);
            Array.Fill(parent, -1);
            dp[0] = 0;

            for (int mask = 0; mask < (1 << n); mask++)
            {
                if (dp[mask] == double.MaxValue) continue;

                // Encontra o primeiro vértice não emparelhado
                int primeiro = -1;
                for (int i = 0; i < n; i++)
                {
                    if ((mask & (1 << i)) == 0)
                    {
                        primeiro = i;
                        break;
                    }
                }

                if (primeiro == -1) continue; // Todos emparelhados

                // Tenta emparelhar com todos os outros vértices não emparelhados
                for (int segundo = primeiro + 1; segundo < n; segundo++)
                {
                    if ((mask & (1 << segundo)) == 0)
                    {
                        int novaMask = mask | (1 << primeiro) | (1 << segundo);
                        double novoCusto = dp[mask] + matriz[vertices[primeiro] - 1, vertices[segundo] - 1];
                        
                        if (novoCusto < dp[novaMask])
                        {
                            dp[novaMask] = novoCusto;
                            parent[novaMask] = mask;
                        }
                    }
                }
            }

            // Reconstrói o emparelhamento
            var emparelhamento = new List<(int, int)>();
            int currentMask = (1 << n) - 1;

            while (parent[currentMask] != -1)
            {
                int prevMask = parent[currentMask];
                int diff = currentMask ^ prevMask;

                // Encontra os dois vértices que foram emparelhados
                var paresEmparelhados = new List<int>();
                for (int i = 0; i < n; i++)
                {
                    if ((diff & (1 << i)) != 0)
                    {
                        paresEmparelhados.Add(i);
                    }
                }

                if (paresEmparelhados.Count == 2)
                {
                    emparelhamento.Add((vertices[paresEmparelhados[0]], vertices[paresEmparelhados[1]]));
                }

                currentMask = prevMask;
            }

            return (dp[(1 << n) - 1], emparelhamento);
        }
    }

}