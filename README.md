# TicketHub

Welcome to **TicketHub**, your one-stop solution for buying and managing event tickets. This project is built with a focus on delivering a seamless ticket purchasing experience.

## Overview

TicketHub leverages the Ports and Adapters architecture (also known as Hexagonal Architecture) to ensure a well-structured, maintainable, and scalable application. This architectural pattern separates the core business logic from external concerns, making it easier to adapt and integrate with various external systems.

## Features

- Browse and search for events
- View event details
- Purchase tickets securely
- Manage and view your ticket history
- Receive email confirmations and reminders

## Architecture

### Ports and Adapters

TicketHub follows the Ports and Adapters architecture to achieve:

- **Separation of Concerns**: Clearly defined boundaries between the core application logic and external systems.
- **Flexibility**: Easy integration with different technologies and services.
- **Testability**: Simplified testing of core business logic without dependencies on external systems.

#### Core Components

1. **Core Application**: Contains the core business logic and domain models. It is designed to be independent of any specific frameworks or technologies.
2. **Ports**: Interfaces that define the operations the core application can perform and the external systems it needs to interact with.
3. **Adapters**: Implementations of the ports, enabling communication between the core application and external systems such as databases, APIs, and user interfaces.

## Getting Started

### Prerequisites

- [.NET 8.0](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or another database of your choice)
- [Docker](https://www.docker.com/get-started) (for containerization, optional)

### Installation

1. Clone the repository:

    ```bash
    https://github.com/marcelog5/TicketHubBackend.git
    ```

2. Navigate to the project directory:

    ```bash
    cd tickethub
    ```

3. Restore the dependencies:

    ```bash
    dotnet restore
    ```

4. Build the project:

    ```bash
    dotnet build
    ```

5. Run the application:

    ```bash
    dotnet run
    ```

### Configuration

Configuration settings are available in `appsettings.json`. Modify this file to set up your database connection, API keys, and other settings.

### Running Tests

To run the unit tests for the core application logic, use:

```bash
dotnet test
