# Books Managment

This project is a system for manage books.

---

## Technologies Used

- **Backend**: `C#` .NET Core 8.0
- **Database**: `MySQL`
- **Frontend**: Angular

---

## Prerequisites

To run this project, you need the following installed:

- **Node.js** (for the frontend)
- **Docker** (for the database and phpMyAdmin)
- .NET SDK 8.0 (for the backend)

---

## How to Run the Project

### 1. Set Up the Database

Navigate to the `database` directory:

```bash
cd database
docker-compose up
```

This command will run the MySQL and phpMyAdmin containers. You can use phpMyAdmin to manage the database. The access credentials for phpMyAdmin are available in the `.env` file.

### 2. Set Up the Backend

Navigate to the backend directory:

```bash
cd backend
```

#### Run Database Migration:

Follow these steps to create migration based on models:

```bash
dotnet ef migrations add InitialCreate
```

Then run the following command to update the database:

```bash
dotnet ef database update
```

If you receive an error related to `dotnet-ef`, please install it with the following command:

```bash
dotnet tool install --global dotnet-ef --version 8.0
```

Follow these steps to run the backend:

#### 1. Restore dependencies:

```bash
dotnet restore
```

#### 2. Build and run the project:

```bash
dotnet run
```

The backend will run on the default address `http://localhost:5140`.

#### Backend Testing

Navigate to the backend directory:

```bash
cd Backend.Tests
```

To run unit tests, use the following command:

```bash
dotnet build
dotnet test
```

This will execute all the tests and display their success or failure status.

### 3. Set Up the Frontend

Navigate to the frontend directory:

```bash
cd frontend
```

Install the required dependencies using:

```bash
npm install
```

Once the dependencies are installed, start the frontend with:

```bash
ng serve
```

The frontend will run on the default address `http://localhost:4200`, and you can access the project in your browser.

---

## Project Features

1. Manage Books.
2. Database Management: Uses MySQL and phpMyAdmin for easy data management.
3. Test-Driven Development: Includes unit tests for repositories and controllers.

---

## Additional Information

- For any questions or issues, feel free to contact me.
- phpMyAdmin and database access credentials are available in the .env file.

---

## License

This project is released under the MIT License.
