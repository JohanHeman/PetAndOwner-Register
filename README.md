# PetAndOwner-Register

A register for viewing people and their pets!

## How to Run the Application

There are two ways to run this application: using a local development environment or using Docker.

### 1. Running Without Docker

#### Prerequisites
Make sure you have the following installed:

- PostgreSQL
- .NET SDK
- Node.js

#### Setup Instructions

1. Clone the repository.
2. Open `appsettings.json` and configure the connection string to connect to your PostgreSQL database.
3. Open a terminal and navigate to the API project.
4. Run the following command to create/update the database using the Code First approach:

   ```bash
   dotnet ef database update --project ../PetAndOwner.Infrastructure/PetAndOwner.Infrastructure.csproj
   ```

5. Navigate to the presentation project:

   ```bash
   cd ../PetAndOwner.Presentation
   ```

6. Install the dependencies:

   ```bash
   npm install
   ```

7. Navigate to the API project:

   ```bash
   cd ../PetAndOwner.Api
   ```

8. Start the API using:

   ```bash
   dotnet run
   ```

   Alternatively, you can start the API directly from your IDE.

9. Navigate back to the presentation project and start the frontend:

   ```bash
   cd ../PetAndOwner.Presentation
   npm start
   ```

   Alternatively, you can start the presentation project directly from your IDE.

### 2. Running With Docker

#### Setup Instructions

1. Create a `.env` file in the solution's root folder.
2. Copy the following environment variables into the file and replace the placeholders with your own values. Remember to remove the brackets.

   ```env
   POSTGRES_DB=DBNAME
   POSTGRES_USER=POSTGRES_USER
   POSTGRES_PASSWORD=PASSWORD
   ```

3. Open a terminal in the solution's root folder.
4. Run Docker Compose:

   ```bash
   docker compose up -d
   ```

5. Check your containers in Docker Desktop. The API, database and angular project should now be running in Docker.


The application should now be up and running.
