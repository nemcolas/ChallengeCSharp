# OdontoPrevCSharp

## Descrição do Projeto

Este projeto foi criado para resolver um problema importante da Odontoprev relacionado à análise e gestão de sinistros odontológicos. O sistema permite que a equipe da Odontoprev gerencie os dados de pacientes, dentistas, consultas e tratamentos. A finalidade principal é dar mais clareza sobre esses dados e fornecer informações que permitam determinar se uma consulta deve ser considerada um sinistro ou não. O uso desse sistema permite à equipe otimizar a tomada de decisões e reduzir fraudes ou abusos no uso de serviços odontológicos.

## Funcionalidades

O sistema oferece um conjunto de funcionalidades robustas que inclui:

- **Gerenciamento de Consultas**: Agendar, visualizar, editar e excluir consultas.
- **Gerenciamento de Pacientes**: Adicionar, listar, editar e remover pacientes.
- **Gerenciamento de Dentistas**: Adicionar, listar, editar e remover dentistas.
- **Gerenciamento de Endereços**: Manter o cadastro de endereços vinculados a pacientes e dentistas.
- **Gerenciamento de Sinistros**: Adicionar e atualizar sinistros odontológicos.
- **Gerenciamento de Tratamentos**: Definir tipos de tratamentos e associá-los às consultas.

### Novas Funcionalidades (Sprint 4)

- **Integração com API Externa (ViaCEP)**: Implementado um serviço e endpoint (`/api/ExternalServices/cep/{cep}`) para consultar endereços brasileiros a partir de um CEP, utilizando a API pública ViaCEP.
- **Predição de Risco de Sinistro com ML.NET**: Implementado um modelo de classificação utilizando ML.NET (FastTree) para prever a probabilidade de uma consulta ser considerada um sinistro com base em dados históricos (Idade, Número de Sinistros Anteriores, Tempo Desde Último Sinistro, Custo Médio Sinistro Anterior). Um endpoint (`/api/Prediction/sinistro-risk`) foi criado para receber os dados de entrada e retornar a predição.
- **Testes Unitários e de Integração**: Adicionados testes automatizados (xUnit, Moq) para as novas funcionalidades, cobrindo os serviços ViaCEP, Prediction e os endpoints da API.

## Requisitos Funcionais e Não Funcionais

(Conforme descrito anteriormente, focando em cadastro, gerenciamento, análise, relatórios, segurança, desempenho, manutenibilidade, escalabilidade e compatibilidade).

## Tecnologias Utilizadas

- **ASP.NET Core Web API**: Framework principal da aplicação (.NET 8).
- **Entity Framework Core**: Usado para mapeamento objeto-relacional (ORM) e interagir com o banco de dados Oracle.
- **Oracle Database**: Sistema de gerenciamento de banco de dados.
- **ML.NET**: Framework de Machine Learning da Microsoft (incluindo `Microsoft.ML` e `Microsoft.ML.FastTree`) para o modelo de predição.
- **xUnit & Moq**: Frameworks para testes unitários e de integração.
- **Rider IDE / Visual Studio / VS Code**: Ambientes de desenvolvimento integrado (IDE).

## Estrutura do Projeto

A solution (`OdontoPrevSolution.sln`) está organizada da seguinte forma:

- **ChallengeCSharp/**: Contém o projeto principal da API ASP.NET Core.
    - **Models/**: Entidades de negócio (Paciente, Dentista, etc.), DTOs (ViaCepResponse) e modelos ML.NET (SinistroInput, SinistroPrediction).
    - **Repositories/**: Interfaces e implementações para acesso a dados (Entity Framework Core).
    - **Services/**: Lógica de negócio, incluindo `ViaCepService`, `ModelTrainerService` e `PredictionService`.
    - **Controllers/**: Controladores da API (`ExternalServicesController`, `PredictionController`) e MVC.
    - **Data/**: Contém dados auxiliares, como o dataset de exemplo para ML (`sinistro_data.csv`).
    - **MLModels/**: Diretório onde o modelo treinado (`sinistro_model.zip`) é salvo (gerado na primeira execução ou quando o serviço `ModelTrainerService` é invocado).
    - **wwwroot/**, **Views/**, **Properties/**, **appsettings.json**, **Program.cs**: Configurações e arquivos padrão do ASP.NET Core.
- **Tests/**: Contém os projetos de teste.
    - **Unit/**: Projeto de testes unitários (`OdontoPrevCSharp.Tests.Unit.csproj`) para os serviços.
    - **Integration/**: Projeto de testes de integração (`OdontoPrevCSharp.Tests.Integration.csproj`) para os endpoints da API.

## Como Executar

### Pré-requisitos

- **.NET 8.0 SDK**: Certifique-se de que o SDK do .NET 8 está instalado.
- **Banco de Dados Oracle**: O projeto está configurado para se conectar a um banco de dados Oracle (ver `appsettings.json`). A funcionalidade principal da Sprint 3 depende disso. As funcionalidades da Sprint 4 (ViaCEP e ML.NET) não dependem diretamente do banco de dados para funcionar.

### Build e Execução

1.  **Restaurar Dependências**: Navegue até o diretório raiz (`/home/ubuntu/odontoprev_sprint4` ou onde a solution foi extraída) e execute:
    ```bash
    dotnet restore OdontoPrevSolution.sln
    ```
2.  **Construir a Solution**: Ainda no diretório raiz, execute:
    ```bash
    dotnet build OdontoPrevSolution.sln --no-restore
    ```
3.  **Executar a Aplicação**: Navegue até o diretório do projeto principal e execute:
    ```bash
    cd ChallengeCSharp
    dotnet run
    ```
    A aplicação estará disponível em `http://localhost:<porta>` ou `https://localhost:<porta>`, conforme configurado.

### Executar os Testes

1.  **Executar Todos os Testes**: Navegue até o diretório raiz da solution (`/home/ubuntu/odontoprev_sprint4`) e execute:
    ```bash
    dotnet test OdontoPrevSolution.sln
    ```
    Isso executará tanto os testes unitários quanto os de integração.

## Endpoints da API (Sprint 4)

- **GET /api/ExternalServices/cep/{cep}**: Consulta o endereço correspondente ao CEP fornecido.
    - Exemplo: `GET /api/ExternalServices/cep/01001000`
- **POST /api/Prediction/sinistro-risk**: Recebe dados de entrada e retorna a predição de risco de sinistro.
    - Corpo da Requisição (JSON):
      ```json
      {
        "Idade": 40,
        "NumeroSinistrosAnteriores": 2,
        "TempoDesdeUltimoSinistro": 5,
        "CustoMedioSinistroAnterior": 150
      }
      ```
    - Resposta (JSON):
      ```json
      {
        "Prediction": true, // ou false
        "Score": 0.85 // Exemplo de score
      }
      ```

## Membros do Projeto

- **Luana Sousa Matos RM: 552621**
- **Nicolas Martins RM: 553478**

