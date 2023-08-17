# STL File Analysis

## **Sobre**

Esta API permite que você faça upload e analise arquivos STL em formato ASCII. Ao fazer o upload de um arquivo STL, a API irá retornar o número de triângulos contidos e a área superficial do modelo 3D.

## **Pré-requisitos**

- .NET Core 3.1

## **Configuração**

1. **Instalar .NET Core 3.1 SDK**:
   - Acesse [downloads do .NET Core](https://dotnet.microsoft.com/download/dotnet/3.1).
   - Baixe e instale o SDK adequado para o seu OS.

2. **Obter o Código**:
   ```bash
   git clone [STLReader]

3. **Acessar projeto**:
   ```bash
    cd [STLReader]

4. **Restaurar os pacotes**:
    ```
    dotnet restore

## **Uso**

1. **Compilar**:
    ```bash
    dotnet build

2. **Executar**:
    ```bash
    dotnet run

3. **Acesso**:
    A API estará rodando, normalmente no endereço http://localhost:44393/. Você pode usar ferramentas como o Postman ou cURL para fazer POST na rota http://localhost:44393/api/stl/upload com o arquivo STL como form data.

## **Decisões de design**:
- A API foi desenvolvida em C# usando ASP.NET Core 3.1. Foi escolhido um modelo de API para permitir a fácil integração com outras ferramentas e plataformas.
- Usamos StreamReader para ler o arquivo devido à sua eficiência em lidar com grandes arquivos.
- Para obter a área de um triângulo, foi utilizada a fórmula do produto vetorial entre dois vetores.

## **Melhorias de desempenho potenciais**:
1. Processamento em paralelo: Para modelos com milhões de triângulos, poderá ser usado um processamento em paralelo para analisar diferentes partes do arquivo simultaneamente.
2. Otimização de Memória: Usando estruturas de dados mais eficientes ou evitando o armazenamento desnecessário.
3. GPU Acceleration: Para cálculos matemáticos intensivos, como a área de triângulos, podemos considerar a aceleração da GPU.

## **Nota**:
Foi feita uma suposição de que todos os arquivos STL de entrada são corretamente formatados.

## **Bibliotecas e dependências**:
Nenhuma biblioteca de terceiros foi usada para analisar diretamente os arquivos STL, conforme solicitado. O código principal foi escrito do zero.
