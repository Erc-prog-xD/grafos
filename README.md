# Algoritmos de Grafos - Árvore Geradora Mínima e Emparelhamento Perfeito

Este projeto implementa algoritmos fundamentais de teoria dos grafos em C#, com foco na construção de árvores geradoras mínimas e emparelhamento perfeito mínimo.

## 📋 Funcionalidades

### 1. Geração de Grafos
- **Geração automática** de matrizes de adjacência aleatórias
- **Ajuste automático** para satisfazer a desigualdade triangular
- **Salvamento** em arquivo de texto para reutilização

### 2. Algoritmo de Kruskal
- **Construção** da Árvore Geradora Mínima (AGM)
- **Implementação** do algoritmo de Kruskal com Union-Find
- **Exibição** das arestas selecionadas e custo total
- **Análise** dos graus dos vértices na AGM

### 3. Emparelhamento Perfeito Mínimo
- **Identificação** dos vértices de grau ímpar na AGM
- **Cálculo** do emparelhamento perfeito de custo mínimo
- **Implementação** de dois algoritmos:
  - Força bruta para conjuntos pequenos
  - Programação dinâmica com bitmask para melhor performance

## 🚀 Como Executar

### Pré-requisitos
- .NET 8.0 ou superior
- Visual Studio Code ou Visual Studio

### Execução
```bash
cd GrafosProgram
dotnet run
```

### Exemplo de Uso
1. Digite o número de vértices desejado
2. O programa gerará uma matriz aleatória e a salvará em `arquivoExemplo/grafo_exemplo.txt`
3. Será exibida a matriz de adjacência
4. O algoritmo de Kruskal construirá a AGM
5. Será calculado o emparelhamento perfeito mínimo para os vértices de grau ímpar

## 📊 Exemplo de Saída

```
Digite o tamanho da matriz de adjacências (número de vértices): 6

Matriz gerada:
          1     2     3     4     5     6
 1 [     0     1     5     4     3     2 ]
 2 [     1     0     2     5     4     3 ]
 3 [     5     2     0     1     5     4 ]
 4 [     4     5     1     0     2     5 ]
 5 [     3     4     5     2     0     1 ]
 6 [     2     3     4     5     1     0 ]

Construindo Árvore Geradora Mínima...
Adicionada: 1 - 2 (peso: 1)
Adicionada: 3 - 4 (peso: 1)
Adicionada: 5 - 6 (peso: 1)
Adicionada: 1 - 6 (peso: 2)
Adicionada: 2 - 3 (peso: 2)

Árvore Geradora Mínima:
  1 - 2 (peso: 1)
  3 - 4 (peso: 1)
  5 - 6 (peso: 1)
  1 - 6 (peso: 2)
  2 - 3 (peso: 2)
Custo total: 7

Graus dos vértices:
  Vértice 1: grau 2
  Vértice 2: grau 2
  Vértice 3: grau 2
  Vértice 4: grau 1
  Vértice 5: grau 1
  Vértice 6: grau 2

Vértices de grau ímpar: 4, 5

Emparelhamento Perfeito Mínimo:
  4 - 5
Custo total do emparelhamento: 2
```

## 🏗️ Estrutura do Projeto

```
GrafosProgram/
├── program.cs                 # Programa principal
├── algoritmos/
│   └── algoritmos.cs         # Implementação dos algoritmos
├── methods/
│   └── methods.cs            # Métodos auxiliares e geração de grafos
├── models/
│   └── grafo.cs              # Modelos de dados
├── arquivoExemplo/
│   └── grafo_exemplo.txt     # Arquivo gerado com a matriz
└── bin/                      # Arquivos compilados
```

## 🔧 Algoritmos Implementados

### Algoritmo de Kruskal
- **Complexidade:** O(E log E) onde E é o número de arestas
- **Estrutura de dados:** Union-Find para detecção de ciclos
- **Objetivo:** Encontrar a árvore geradora de custo mínimo

### Emparelhamento Perfeito Mínimo
- **Força Bruta:** O(n!) para conjuntos pequenos
- **Programação Dinâmica:** O(2^n × n²) com bitmask
- **Objetivo:** Emparelhar vértices de grau ímpar com custo mínimo

## 🎯 Aplicações

Este projeto é útil para:
- **Estudo** de algoritmos de grafos
- **Implementação** do algoritmo de Christofides para TSP
- **Análise** de redes e conectividade
- **Otimização** de rotas e custos

## 📝 Observações

- O programa ajusta automaticamente a matriz para satisfazer a desigualdade triangular
- O emparelhamento perfeito é calculado apenas para vértices de grau ímpar
- Para grafos grandes (>20 vértices ímpares), considere usar algoritmos mais eficientes como o Algoritmo de Edmonds

## 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para:
- Reportar bugs
- Sugerir melhorias
- Implementar novos algoritmos
- Otimizar o código existente

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.