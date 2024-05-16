# FinancePortfolioManager Sistema de Gestão de Portfólio de Investimentos

## Descrição

 ```sh
    Este projeto foi desenvolvido para uma empresa de consultoria financeira e consiste em uma API backend em .NET para gerenciamento de portfólios de investimentos, bem como uma Função Azure para envio de notificações por e-mail sobre produtos de investimento próximos ao vencimento.

 ```

## Funcionalidades
```sh

    API Backend
    Gestão de Produtos Financeiros: Permite adicionar, editar, listar e excluir produtos financeiros.
    Gestão de Investimentos: Permite que os clientes comprem e vendam produtos financeiros, além de consultar o extrato de seus investimentos.
    Desempenho Otimizado: Consultas de produtos disponíveis e extratos são otimizadas para suportar um grande volume de requisições com tempo de resposta abaixo de 100ms.
    Função Azure
    Notificações por E-mail: Envia e-mails diários para administradores sobre produtos que expiram em até 7 dias.
```

## Como Executar a Aplicação

### Pré-requisitos

1. .NET SDK 8.
1. Docker
3. [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local) para desenvolvimento local da Função Azure.
4. [SQL Server]

### Executando a API

1. Clone o repositório:
    ```sh
    git clone https://github.com/seuusuario/InvestmentPortfolioManagement.git
    cd InvestmentPortfolioManagement/API
    ```

2. Configure as suas `appsettings.json` com as configurações necessárias (por exemplo, string de conexão com o banco de dados).

3. Restaure as dependências e crie as migrações:
    ```sh
    dotnet restore
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

4. Execute a aplicação:
    ```sh
    dotnet run
    ```

5. A API estará disponível em `https://localhost:5001` (ou a porta configurada).

### Executando a Função Azure Localmente

1. Navegue até a pasta das funções:
    ```sh
    cd InvestmentPortfolioManagement/Functions
    ```

2. Configure o arquivo `local.settings.json` com as configurações de e-mail:
    ```json
    {
      "IsEncrypted": false,
      "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet"
      },
      "EmailSettings": {
        "SmtpServer": "smtp.exemplo.com",
        "SmtpPort": 587,
        "SmtpUser": "usuario@exemplo.com",
        "SmtpPass": "suasenha",
        "FromEmail": "noreply@exemplo.com"
      }
    }
    ```

3. Execute a Função Azure localmente:
    ```sh
    func start
    ```

4. A função será executada diariamente às 9h UTC para enviar notificações por e-mail.

### Implantando no Azure

#### API

1. Publique a API no Azure App Service via Visual Studio:
    - Clique com o botão direito no projeto API e selecione `Publicar`.
    - Siga o assistente para selecionar sua conta Azure e o grupo de recursos.

#### Função Azure

1. Publique a Função Azure via Visual Studio:
    - Clique com o botão direito no projeto de Função Azure e selecione `Publicar`.
    - Siga o assistente para selecionar sua conta Azure e o grupo de recursos.

2. Configure as `Configurações de Aplicativo` no portal do Azure:
    - Navegue até a Função App no portal Azure.
    - Em `Configurações de Aplicativo`, adicione as configurações de e-mail conforme `local.settings.json`.

## Monitoramento e Logs

- **API**: Utilize Application Insights ou o serviço de logs do Azure App Service.
- **Função Azure**: Utilize a seção de monitoramento no portal do Azure para visualizar logs e diagnósticos em tempo real.


---
