# JobOffer API

The JobOffer API is a RESTful API designed to manage job offers, applications, and user interactions between job seekers and managers.

## Features

- **Job Offer Management**: Managers can create, update, and delete job offers.
- **Application Submission**: Job seekers can apply for job offers.
- **Application Viewing**: Managers can view applicants for their job offers.
- **Offer Retrieval**: Seekers can retrieve all available job offers.
- **Applied Offers**: Seekers can retrieve all job offers they have applied for.
- **Pagination**: The Get offers endpoint allows to paginate the results, choosing page size and page number

## Endpoints

### Job Offers

- `GET /api/offers?searchPhrase=offers&pageSize&pageNumber`: Retrieve all available job offers.
- `POST /api/offers`: Create a new job offer.
- `PATCH /api/offers/{offerId}`: Update an existing job offer.
- `DELETE /api/offers/{offerId}`: Delete a job offer.


## Authentication

The API utilizes token-based authentication. Users (both managers and seekers) must obtain an access token by registering or logging into the system.

## Technologies Used

- .NET Core: Framework for building the API endpoints.
- C#: Programming language for implementing business logic and controllers.
- **JWT**: JSON Web Tokens for secure authentication.
- Entity Framework Core: Object-Relational Mapping (ORM) framework for interacting with SQL Server database.
- SQL Server: Relational database for storing job offers, applications, and user data.
- NLog library (logging into files)


in progress:
-Integral tests
