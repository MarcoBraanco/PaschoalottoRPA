# Fast Fingers RPA

## Descrição do Projeto

O Fast Fingers RPA é um programa que registra métricas de desempenho ao digitar e armazena os resultados em um banco de dados PostgreSQL.

## Pré-requisitos

Antes de executar o programa, certifique-se de ter os seguintes requisitos instalados:
- PostgreSQL


## Configuração arquivo appconfig.json
Altere a ConnectionString dentro do arquivo appconfig.json para os dados do seu Banco de dados.

## Configuração do Banco de Dados

1. Crie um banco de dados no PostgreSQL com o nome "PaschoalottoRPA".
2. Execute o seguinte SQL para criar a tabela necessária:

```sql
CREATE TABLE FastFingersData (
    ID SERIAL PRIMARY KEY,
    DATA_EXECUCAO TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    WPM INTEGER,
    KEYSTROKES INTEGER,
    ACCURACY FLOAT,
    CORRECT_WORDS INTEGER,
    WRONG_WORDS INTEGER
);
```
## Compilando o Programa no Visual Studio

Se você deseja compilar o programa utilizando o Visual Studio, siga as instruções abaixo:

1. **Instale o Visual Studio:**
   - Faça o download e instale o [Visual Studio](https://visualstudio.microsoft.com/).

2. **Abra o Projeto no Visual Studio:**
   - Abra o Visual Studio.
   - No menu, clique em "File" -> "Open" -> "Project/Solution" e selecione o arquivo do projeto Fast Fingers RPA (geralmente com a extensão `.sln`).

3. **Configuração do Banco de Dados:**
   - Siga as instruções fornecidas no README para configurar o banco de dados antes de compilar o programa.

4. **Compilar e Executar o Programa:**
   - Certifique-se de que o projeto esteja selecionado no Solution Explorer.
   - Pressione `Ctrl+Shift+B` ou vá para o menu "Build" e escolha "Build Solution" para compilar o projeto.
   - Pressione `F5` para iniciar o programa
