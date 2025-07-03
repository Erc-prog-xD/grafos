# INSTRUÇÕES DE EXECUÇÃO

## Como executar o projeto

### Opção 1: Execução básica
```bash
cd GrafosApp
dotnet run
```

### Opção 2: Com arquivo específico
```bash
cd GrafosApp
dotnet run exemplos/exemplo_5_vertices.txt
```

### Opção 3: Usando Visual Studio
1. Abra o arquivo `GrafosApp.sln` no Visual Studio
2. Pressione F5 ou clique em "Iniciar"

## Arquivos de Exemplo Incluídos

- `exemplos/exemplo_5_vertices.txt` - Grafo com 5 vértices
- `exemplos/exemplo_6_vertices.txt` - Grafo com 6 vértices
- `exemplo.txt` - Criado automaticamente na primeira execução

## Saída Esperada

```
===============================================
    ALGORITMO DE CHRISTOFIDES PARA TSP
    Implementação do Trabalho Prático
===============================================

Carregando grafo do arquivo: exemplo.txt
Grafo carregado com sucesso:
- Vértices: 4
- Arestas: 6

Grafo Ponderado Não Direcionado:
Vértice 1: 2(10,0), 3(15,0), 4(20,0)
Vértice 2: 1(10,0), 3(25,0), 4(30,0)
Vértice 3: 1(15,0), 2(25,0), 4(35,0)
Vértice 4: 1(20,0), 2(30,0), 3(35,0)

Validando desigualdade triangular...
Desigualdade triangular validada com sucesso!

=== ALGORITMO DE CHRISTOFIDES ===

Etapa I: Construindo árvore geradora mínima...
AGM construída com custo: 45,00
Arestas da AGM: 3

Etapa II: Identificando vértices de grau ímpar...
Vértices de grau ímpar: [1, 2, 3, 4]
Quantidade: 4

Etapa III: Construindo emparelhamento perfeito...
Emparelhamento construído com custo: 45,00
Pares emparelhados: 2

Etapa IV: Unindo AGM e emparelhamento...
Multigrafo construído com todos os vértices de grau par

Etapa V: Encontrando ciclo euleriano...
Ciclo euleriano encontrado com 11 vértices

Etapa VI: Aplicando shortcutting...
Ciclo hamiltoniano obtido com custo: 90,00

===============================================
              RESULTADO FINAL
===============================================
Ciclo Hamiltoniano: 1 -> 2 -> 3 -> 4 -> 1
Custo Total: 90,00
===============================================

Resultado salvo em: exemplo.resultado.txt
```

## Arquivos Gerados

Após a execução, o programa cria:
- `exemplo.resultado.txt` - Arquivo com o resultado da execução

## Resolução de Problemas

Se encontrar erros:
1. Verifique se o .NET 6.0+ está instalado: `dotnet --version`
2. Restaure as dependências: `dotnet restore`
3. Recompile o projeto: `dotnet build`
