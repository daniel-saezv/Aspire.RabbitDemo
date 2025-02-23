# Aspire RabbitMQ Weather Demo

This repository contains a .NET Aspire project that demonstrates the integration of RabbitMQ with an ASP.NET Core Web API and a Blazor Web frontend.
All the implementation is based on the .NET Aspire App template from Microsoft, which provides a structured approach to building distributed applications with .NET.

## Project Structure

- **Aspire.RabbitDemo.Publisher.ApiService**: Contains the ASP.NET Core Web API that publishes weather events to RabbitMQ.
- **Aspire.RabbitDemo.Consumer.Web**: Contains the Blazor Web frontend that consumes RabbitMQ events and displays the weather data.
- **Aspire.RabbitDemo.AppHost**: Contains the app host configuration using .NET Aspire.
- **Aspire.RabbitDemo.ServiceDefaults**: Contains shared service defaults and configurations.

 ![Image](https://github.com/user-attachments/assets/0eaf23eb-755b-40d7-83a8-0bea9eba9d66)

## Prerequisites

- .NET 8.0 SDK
- Docker Desktop (for running RabbitMQ container)

## Getting Started

### Building and Running the Project

1. Clone the repository:
```
git clone https://github.com/your-repo/aspire-rabbitmq-weather-demo.git cd aspire-rabbitmq-weather-demo
```

2. Build and run the project:
```
dotnet build dotnet run --project Aspire.RabbitDemo.AppHost
```
3. Use the Aspire Dashboard to navigate to the entry points of the applications and start to play. You can publish and visualize the event data, while you are monitoring all the traces and metrics from the .NET Aspire Dashboard.

## Project Details

### ASP.NET Core Web API

The API is defined in the `Aspire.RabbitDemo.Publisher.ApiService` project. It includes an endpoint that publishes weather events to RabbitMQ. If the request body is empty, it generates random weather data.

### Blazor Web Frontend

The frontend is defined in the `Aspire.RabbitDemo.Consumer.Web` project. It consumes RabbitMQ events and displays the weather data in a table.

### .NET Aspire App Host

The app host is defined in the `Aspire.RabbitDemo.AppHost` project. It uses the .NET Aspire framework to configure and run the distributed application. The app host sets up the RabbitMQ container with the management plugin, the ASP.NET Core Web API, and the Blazor Web frontend. It ensures that the services are started in the correct order and that the necessary dependencies are available.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
