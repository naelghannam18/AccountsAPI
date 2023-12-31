# Nael Ghannam RJP - BSynchro.

# Solutions: 
- *AccountsApi* using .Net 6, Entity Framework, Generic Repository, and SQL Server.
- *AccountsFrontEnd* Using Angular 13.

# Backend
## Architecture
The Application Backend Structure was created following Clean-Architecture Approach.
```
├───AccountsApi
│   ├───Controllers
│   ├───ExceptionHandler
│   └───Properties
├───Context
│   ├───Context
│   ├───Migrations
│   └───Context
├───Core
│   └───Services
│       ├───Contracts
│       └───Implementations
├───core.tests
├───Domain
│   ├───DTOs
│   ├───Enums
│   ├───Exceptions
│   └───Models
├───Extensions
│   └───ListExtensions
└───Infrastructure
    ├───Mappings
    └───Repositories
```


## Database Design

I Took the liberty to Create A database design to accommodate an Accounts Application that allows to create customers, create Accounts and monetary transactions between accounts. 
The CreatedDate property is used as a time stamp for all the entities.
The IsRemoved Property Is Used for Soft Deletion.

|Customer|  |
|--|--|
| Id | int |
| Name | nvarchar(30) |
| Surname | Nvarchar(30) |
| CreatedDate | Datetime2 |
| IsRemoved | bit |

<br>

| Accounts |  |
|--|--|
| Id | int (PK) |
| Balance | decimal |
| CustomerId | int |
| CreatedDate | datetime2 |
| IsRemoved | bit |

<br/>

|Transactions|  |
|--|--|
| Id | int |
| Amount | decimal |
| SenderId | int |
| ReceiverId | int |
| CreatedDate | Datetime2 |
| IsRemoved | bit |

### Generic Repository
Generic Repository and Unit of work design patterns are used to provide data access to the database and accommodate fault-tolerant transaction to the database.
I decided to use A generic Repository only for the sake of the application because there are really few entities and minimum transaction. Although I did handle Saving Changes to the database with concurrency issues in mind.

## Api Documentation
Customer
```
GET /api/customer      => returns all Customers with their accounts and transactions.
GET /api/customer/{id} => returns customer with the provided Id.
POST /api/customer     => Create A Customer and returns the Id of the Created Customer.
PUT /api/customer      => Update A Customer.
DELETE /api/customer   => Delete Customers by providing an array of ids. 
```
Account
```
GET /api/account/customer/{CustomerdId} => Returns All Accounts for the customer Id.
GET /api/account/{accountId}            => Returns Account by Id.
POST /api/account                       => Create Account and return its ID.
DELETE /api/account                     => Delete Account providing an Array of Ids.
```

Transaction
```
GET /api/accounts/{accountId}/Transactions => Get Account Transactions.
GET /api/transactions/{Id}                 => Get Transaction By Id.
POST /api/transaction                      => Create Transaction.
DELETE /api/transaction                    => Delete Transaction.
```

# User Interface

The User Interface Was Created Using Angular 13.
It comprises of a simple Design to showcase the intended purposes of the backend.

The UI is comprised of two main Components: Home and Customer Components.

## Home Component
Contains a form that allows for the creation of customers and list containing the created users as well as a button to navigate to the customer dashboard.

## Customer Component
Dashboard-Like component that showcases the account details, transactions, and allows to make transactions between customers wit real-time changes to data.

# Portability
The backend project supports docker with a basic Configuration;

# CI/CD
A Basic CI/CD Pipeline can be created using github action.
We would need a Cloud Provider such as AWS or Azure to rent a virtual machine to host the solution.
Github actions can help in building, testing and deploying the solution.

# Testability 
I have little Knowledge of Unit Tests in .Net and No Professional Experience with it. 
Nevertheless I did a brief research in respect with the Exercise Deadline and tried to create some Unit Tests for the Customer Service to showcase what I learned from this brief research

# Launching the solution:

## Backend

- Clone the repo using <b>git clone https://github.com/naelghannam18/AccountsAPI.git </b>
- If using Visual studio, build and run the web api using <b>F5</b>.
- Otherwise you can you the CLI using the command <b>>> dotnet run</b>
- The web API will run on <b>Https://localhost:7009</b>
- Swagger Developer page will also be available in development mode.

## Frontend
-	Clone the Repo using <b>git clone https://github.com/naelghannam18/AccountsFrontEnd.git</b>
-	Inside the repo run <b>npm i</b> to install packages
-	run <b>ng serve</b> to run the application in development mode.
-	run <b>npm run build</b> to create an optimized build file and run it using <b>serve -s ./build</b>

# Final Notes
Scaling such an application would require a few more features to implement such as:
- More robust and fault-tolerant approach to the data access layer such Unit of Work design pattern and mediator pattern in to ensure more decoupling.
- Caching Service: Caching helps by saving data in memory rather than always querying from the database, this ensures faster response time and less database overhead. We can use .Net In-Memory Caching or Redis for distributed cache.
- Add Authentication and Authorization. We Can use .Net Identity using JWT Tokens to ensure Endpoint Security.
- More advanced approach into Error Handling. 
- Logging. 

# Looking forward for you feedback!


