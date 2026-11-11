# SolOricle-

SolOricle

SolOricle is a full-stack journaling application built with C#, .NET MAUI, ASP.NET Core, Entity Framework Core, and SQLite.

The project was developed to explore full-stack application architecture, RESTful API design, persistent data storage, and cross-platform user interface development within the .NET ecosystem.

Features

Cross-platform journaling interface built with .NET MAUI

Create, read, update, and delete journal entries

ASP.NET Core backend with RESTful API endpoints

Persistent SQLite database storage

Entity Framework Core for data access and object-relational mapping

Structured client-server architecture

Object-oriented design and separation of concerns

Input handling, debugging, and iterative UI development

Tech Stack

Area

Technology

Language

C#

Client

.NET MAUI

Backend

ASP.NET Core

API

REST

Database

SQLite

ORM

Entity Framework Core

Architecture

SolOricle follows a client-server structure:

.NET MAUI Client
       |
       | HTTP / REST
       v
ASP.NET Core API
       |
       v
Entity Framework Core
       |
       v
SQLite Database

The MAUI client is responsible for the user interface and interaction logic, while the ASP.NET Core backend handles API requests and application data. Entity Framework Core provides the data-access layer between the API and SQLite database.

Project Goals

The main goals of SolOricle were to:

Build a complete application spanning frontend, backend, API, and database layers

Gain practical experience designing RESTful services

Implement persistent data storage using Entity Framework Core

Apply object-oriented programming principles in a larger application

Develop a maintainable project structure with separation of responsibilities

Strengthen debugging and full-stack .NET development skills

Getting Started

Prerequisites

To run the project locally, you will need:

.NET SDK

Visual Studio 2022 with the .NET Multi-platform App UI development workload

.NET MAUI tooling

SQLite support

Clone the Repository

git clone https://github.com/Harryprice1910/SolOricle.git
cd SolOricle

If the repository name or URL differs, update the command above accordingly.

Restore Dependencies

From the relevant solution or project directory:

dotnet restore

Run the Backend

Navigate to the ASP.NET Core API project directory and run:

dotnet run

The terminal will display the local address used by the API.

Run the MAUI Client

Open the solution in Visual Studio, select the appropriate MAUI target platform, and run the client application.

If required, update the client API base URL so that it points to the locally running ASP.NET Core backend.

Development Notes

SolOricle was developed as a portfolio and learning project, with emphasis on understanding how multiple application layers work together rather than relying on a single monolithic implementation.

Key areas explored during development included:

REST API design

CRUD operations

Entity Framework Core models and database access

Client-server communication

Cross-platform UI development

Application architecture

Debugging and iterative implementation

Future Improvements

Possible future development includes:

User authentication and individual accounts

Improved search and filtering of journal entries

Tags, categories, and richer journal organisation

Analytics or mood-tracking features

Improved validation and error handling

Automated testing

Cloud-hosted API and database deployment

AI-assisted journal analysis or reflection features

Author

Harry Price McCullagh

GitHub: Harryprice1910

LinkedIn: harry-price-253a6536b

License

This project is currently intended for portfolio and educational use.
