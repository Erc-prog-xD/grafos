# Algoritmo de Christofides para TSP Métrico

## Descrição

Implementação em C# do **Algoritmo de Christofides** para resolver o Problema do Caixeiro Viajante (TSP) em grafos completos que satisfazem a desigualdade triangular. Este algoritmo fornece uma solução aproximada com garantia de no máximo 1,5 vezes o custo da solução ótima.

## Estrutura do Projeto

```
GrafosApp/
├── Models/
│   └── Grafo.cs                    # Classe principal para representação de grafos
├── Algoritmos/
│   ├── AlgoritmoChristofides.cs    # Implementação completa do algoritmo
│   ├── ArvoreGeradoraMinima.cs     # Algoritmo de Kruskal para AGM
│   ├── BuscaEmLargura.cs           # Implementação BFS
│   ├── BuscaEmProfundidade.cs      # Implementação DFS
│   └── CicloEuleriano.cs           # Algoritmo de Hierholzer
├── Utils/
│   └── CarregadorGrafo.cs          # Utilitários para carregar grafos
└── Program.cs                      # Programa principal

```

## Funcionalidades Implementadas

### ✅ Etapas do Algoritmo de Christofides

1. **Construção de Árvore Geradora Mínima (AGM)**
   - Implementação do algoritmo de Kruskal
   - Estrutura Union-Find otimizada

2. **Identificação de Vértices de Grau Ímpar**
   - Análise automática dos graus na AGM

3. **Emparelhamento Perfeito de Custo Mínimo**
   - Algoritmo guloso (pode ser substituído por biblioteca externa)

4. **União de AGM e Emparelhamento**
   - Criação de multigrafo onde todos os vértices têm grau par

5. **Determinação de Ciclo Euleriano**
   - Implementação do algoritmo de Hierholzer

6. **Shortcutting**
   - Conversão do ciclo euleriano em ciclo hamiltoniano

### 🔧 Recursos Adicionais

- ✅ Validação da desigualdade triangular
- ✅ Carregamento de grafos via arquivo .txt
- ✅ Suporte a grafos ponderados
- ✅ Visualização detalhada do processo
- ✅ Exemplos de teste incluídos

## Como Executar

### Pré-requisitos
- .NET 6.0 ou superior
- Sistema operacional: Windows, Linux ou macOS

### Instruções

1. **Clone ou baixe o projeto**
2. **Navegue até a pasta do projeto:**
   ```bash
   cd GrafosApp
   ```

3. **Execute o programa:**
   ```bash
   dotnet run
   ```

## Formato de Entrada

O programa aceita arquivos .txt no seguinte formato:

```
N
w11 w12 w13 ... w1N
w21 w22 w23 ... w2N
...
wN1 wN2 wN3 ... wNN
```

Onde:
- `N` = número de vértices
- `wij` = peso da aresta entre vértices i e j
- Matriz deve ser simétrica (wij = wji)
- Diagonal principal geralmente com zeros
- Deve satisfazer a desigualdade triangular

### Exemplo de Arquivo de Entrada (exemplo.txt)

```
4
0 10 15 20
10 0 25 30
15 25 0 35
20 30 35 0
```

## Exemplos de Execução

### Exemplo 1: Grafo com 4 vértices
```
Ciclo Hamiltoniano: 1 -> 2 -> 3 -> 4 -> 1
Custo Total: 90,00
```

### Exemplo 2: Grafo com 5 vértices
```
Ciclo Hamiltoniano: 1 -> 3 -> 5 -> 4 -> 2 -> 1
Custo Total: 87,00
```

## Decisões de Implementação

### 1. **Representação do Grafo**
- Uso de `Dictionary<int, List<int>>` para adjacências
- `Dictionary<int, Dictionary<int, double>>` para pesos
- Suporte a multigrafos necessário para o algoritmo

### 2. **Algoritmo de Kruskal**
- Implementação com Union-Find otimizada
- Compressão de caminho e união por rank

### 3. **Emparelhamento Perfeito**
- Algoritmo guloso simples para demonstração
- Pode ser substituído por biblioteca mais eficiente (como sugerido no trabalho)

### 4. **Ciclo Euleriano**
- Algoritmo de Hierholzer modificado para multigrafos
- Tratamento correto de arestas múltiplas

## Testes e Validação

### Teste 1: Árvore Geradora Mínima
- ✅ Algoritmo de Kruskal testado com grafos pequenos
- ✅ Verificação manual dos custos
- ✅ Validação da estrutura de árvore (n-1 arestas)

### Teste 2: Ciclo Euleriano
- ✅ Verificação de graus pares em multigrafos
- ✅ Teste do algoritmo de Hierholzer
- ✅ Validação do shortcutting

### Teste 3: Desigualdade Triangular
- ✅ Validação automática para todos os trios de vértices
- ✅ Detecção de violações com mensagens claras

## Bibliotecas Utilizadas

- **System.Collections.Generic** - Estruturas de dados
- **System.Linq** - Operações LINQ
- **System.IO** - Manipulação de arquivos

### Observação sobre Emparelhamento
Conforme sugerido no trabalho, a etapa de emparelhamento perfeito pode ser implementada com bibliotecas externas como:
- **OR-Tools** (Google)
- **COIN-OR** 
- **Gurobi** (com licença acadêmica)

## Análise de Complexidade

- **Kruskal**: O(E log V)
- **Emparelhamento**: O(V³) com algoritmo guloso
- **Ciclo Euleriano**: O(E)
- **Complexidade Total**: O(V³) dominada pelo emparelhamento

## Limitações e Melhorias Futuras

1. **Emparelhamento**: Implementação atual é gulosa, não garantindo otimalidade
2. **Interface**: Apenas console, poderia ter interface gráfica
3. **Escala**: Testado com grafos pequenos (< 20 vértices)

## Autor

Trabalho desenvolvido para a disciplina de Grafos.
Implementação do Algoritmo de Christofides em C#.

## Referências

- ANDRETTA, M. Algoritmos de Aproximação. São Carlos: ICMC-USP, 2019.
- KRYMGAND, A. The Christofides Algorithm. Disponível em: https://alon.kr/posts/christofides
- CHRISTOFIDES, N. Worst-case analysis of a new heuristic for the travelling salesman problem. 1976.