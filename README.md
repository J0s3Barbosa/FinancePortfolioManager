# Investment Portfolio Management System

## Description

This project was developed for a financial consulting firm and consists of a backend API in .NET Core for managing investment portfolios, as well as an Azure Function for sending email notifications about investment products nearing their expiration dates.

## Features

### Backend API

- **Financial Product Management**: Allows adding, editing, listing, and deleting financial products.
- **Investment Management**: Enables clients to buy and sell financial products and view their investment statements.
- **Optimized Performance**: Queries for available products and statements are optimized to support a high volume of requests with a response time below 100ms.

### Azure Function

- **Email Notifications**: Sends daily emails to administrators about products expiring within 7 days.

## How to Run the Application

### Prerequisites

1. .NET SDK 8
2. Docker
3. [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local) for local development of the Azure Function.
4. [SQL Server]

### Running the API

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/InvestmentPortfolioManagement.git
    cd InvestmentPortfolioManagement/API
    ```

2. Configure your `appsettings.json` with the necessary settings (e.g., database connection string).

3. Restore dependencies and create migrations:
    ```sh
    dotnet restore
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

4. Run the application:
    ```sh
    dotnet run
    ```

5. The API will be available at `https://localhost:5001` (or the configured port).

### Running the Azure Function Locally

1. Navigate to the functions folder:
    ```sh
    cd InvestmentPortfolioManagement/Functions
    ```

2. Configure the `local.settings.json` file with email settings:
    ```json
    {
      "IsEncrypted": false,
      "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet"
      },
      "EmailSettings": {
        "SmtpServer": "smtp.example.com",
        "SmtpPort": 587,
        "SmtpUser": "user@example.com",
        "SmtpPass": "yourpassword",
        "FromEmail": "noreply@example.com"
      }
    }
    ```

3. Run the Azure Function locally:
    ```sh
    func start
    ```

4. The function will run daily at 9 AM UTC to send email notifications.

### Deploying Docker

- find docker-compose.yaml
    - add ur info on environment   
- run docker compose up

## Observability patterns

- Monitoring and Logs
- three types of telemetry data 
    - Metrics,
    - logs 
    - Traces
- Azure Observability and Monitoring Tools
    - Azure Monitor
    - Application Insights
    - Log Analytics
    - Azure Advisor

- **API**: Use Application Insights or the Azure App Service logging service.
- **Azure Function**: Use the monitoring section in the Azure portal to view logs and diagnostics in real-time.

---


## Tech Stack 

- Async await Tasks
    - mechanism for writing asynchronous code
    - easier to develop responsive and scalable applications
    - Tasks are objects that encapsulate asynchronous operations and provide features such as cancellation, continuation, and error handling
    - does not block the calling thread while waiting for asynchronous operations to complete.
- Concurrency
    - handle multiple tasks simultaneously
- Data Streaming
    - IAsyncEnumerable 
        - asynchronous counterpart of IEnumerable
        - IEnumerable iterates synchronously and can cause the thread to block, 
        - IAsyncEnumerable allows for asynchronous iteration without blocking the thread.
- Logging
- Validation
    - FluentValidation
    - uses a fluent interface and lambda expressions for building validation rules
    - lightweight and it supports all kinds of custom validation rules

- Middleware
    - allowing you to handle requests and responses at various stages of the HTTP request lifecycle
-  dependency injection DI
    - Common IoC technique where dependencies are "injected" into objects.
    - achieves that by decoupling the usage of an object from its creation
    - helps you to follow SOLID's dependency inversion and single responsibility principles
- Service Lifetimes
    - Transient
        - Created every time they are requested
    - Scoped
        - single instance of the service is created and shared within the scope of a single operation or request. 
        - this typically corresponds to the duration of an HTTP request
    - Singleton
        - A single instance of the service is created and shared across all requests. This means that the same instance is reused every time the service is requested.
- ORM Entity Framework;
    - Working with code-first migration
    - LINQ
        - LINQ queries uses extension methods for classes that implement IEnumerable or IQueryable interface
        - two basic ways to write a LINQ query
            - Query Syntax or Query Expression Syntax
            - similar to SQL (Structured Query Language)
        - Method Syntax or Method Extension Syntax or Fluent
            - also known as fluent syntax
            - is like calling extension method
    - IEnumerable
        - suitable for in-memory collections and supports simple iteration
        - Exposes the enumerator, which supports a simple iteration over a collection of a specified type
    - IQueryable
        - querying data from out-memory (like remote database, service) collections
        - While querying data from a database, IQueryable executes the select query on the server side with all filters. 
        - IQueryable is suitable for LINQ to SQL queries
        - extends this capability for composing queries against remote data sources, offering deferred execution
        - evaluation of an expression is delayed until its realized value is actually required or called like ToList()
        - improve performance when you have to manipulate large data collections
        - especially when it contains a series of chained queries or manipulations

          - RestFull API's
- Client-server
- Stateless
- Use HTTP methods
- Use clear and consistent resource naming conventions
- Use HTTP status codes correctly
- Use JSON
- Pagination
- Versioning
- sort and filter
- Error handling
- Caching
    - Be aware of Concurrency and locking issues
- use HTTPS and SSL
- Validate input parameters
- Make use of the proper mechanisms for authentication and authorization
    - Implement OAuth 2.0 or JWT-based authentication and define roles/permissions for authorized users.
 
- API resilience patterns
    - APIs that can withstand various challenges and continue to function effectively
    - Error handling
    Use HTTP status codes effectively, and provide descriptive error responses to help with troubleshooting
    - Rety pattern
        automatically retries failed operations
        handle transient failures
        can improve the stability
        specific fault reported is unusual or rare
        make communication more resilient
        common pattern for recovering from transient errors, which are errors that only last a short time
        Transient faults are usually self-correcting and can include:
        Network connectivity loss,
        Service unavailability,
        Timeouts,
        Component overload
    - Circuit-breaker
        to prevent cascading failures in distributed systems. 
        safety mechanism to detect and handle failures
        temporarily stops sending requests to the failing component and redirects them to a fallback or an alternative service.
        helps to isolate the failure,
        improve the stability and resiliency and prevent overload on the failing component

- use of Records for transferring data
provide a modern, concise, and expressive approach to handling immutable data structures. They reduce boilerplate code, enhance readability, and seamlessly integrate with language features like pattern matching
Immutability: C# records are immutable by default, ensuring that once a record object is created, its properties cannot be modified. This immutability guarantees data consistency, simplifies debugging, and enhances code reliability. In contrast, DTO classes are mutable, allowing properties to be changed after instantiation.
Conciseness and Readability: C# records are more concise, requiring less boilerplate code. They enhance code readability by providing a clear and expressive representation of data structures. DTO classes, while functional, tend to be more verbose due to the need for property definitions, constructors, and equality methods.
Pattern Matching: C# records integrate seamlessly with pattern matching, making them powerful for conditional logic based on data shapes. This feature simplifies complex branching in code. While DTOs can be used in pattern-matching scenarios, records offer a more elegant and succinct solution.

- DevOps
    - CI-CD
    -     
    - Docker
        - Dockerfile
        - Docker Compose
            - basically a composed service deploying the api, database, frontend
        - Docker Networks
            - can isolate containers or enable selective communication   
        - trunk-based development
            - merge every new feature, bug fix, or other code change to one central branch in the version control system
- Tests
    - Functional test or integration test
        Using xunit, httpRequest lib,
        in memory or test databse
    - Playwright

- Event-Driven
    -  Embrace an event-driven approach for inter-service communication.
    - This enhances decoupling and allows services to react to events asynchronously.

- optimizing an API endpoint
    - high-level process
        - Identify the bottlenecks
            - Calling the database from a loop
            - Calling an external service many times
            - Duplicate calculations with the same parameters
         - Fix the database queries
            - Don't call the database from a loop
            - Return multiple results in one query
        - Fix the external API calls
            - 𝗖𝗼𝗻𝗰𝘂𝗿𝗿𝗲𝗻𝘁 𝗲𝘅𝗲𝗰𝘂𝘁𝗶𝗼𝗻 𝗶𝘀 𝘆𝗼𝘂𝗿 𝗳𝗿𝗶𝗲𝗻𝗱
            - multiple asynchronous calls to different services
            - called these services concurrently and aggregated the results
        - Add caching as a final touch
            - 𝗖𝗮𝗰𝗵𝗶𝗻𝗴 𝗮𝘀 𝗮 𝗹𝗮𝘀𝘁 𝗿𝗲𝘀𝗼𝗿𝘁
            - IMemoryCache (uses server RAM)
            - IDistributedCache (Redis, Azure Cache for Redis)
    - Measuring performance is also a crucial step in the optimization process:
        - Logging execution times with a Timer/Stopwatch
        - If you have detailed application metrics, even better
        - Use a performance profiler tool to find slow code





