**Office Parking Management System**

Overview

The Office Parking Management System is a modern web application designed to solve the common office problem of identifying and contacting vehicle owners in a shared car park. It replaces outdated paper-based systems with a secure, real-time, and self-service digital solution. The application features an interactive parking map, allowing employees to see available spots and mark their own, providing a complete and live overview of the car park's status.

This project was developed as part of the Work-Based Learning module (IMAT1405).

Key Features

User Authentication: Secure registration and login for all employees.

Vehicle Management: Users can add and remove their personal vehicles. Vehicle details (make, model, color) are automatically fetched and verified using the 

DVSA API to ensure data accuracy.

Interactive Parking Map: A real-time visual grid of the car park shows which spots are available (green) and occupied (red).

Occupy & Vacate Spots: Employees can click a spot to park their specific vehicle, and the system enforces a one-spot-per-user rule.


Quick Vehicle Search: Instantly search for a vehicle by its registration number to retrieve the owner's name and contact phone number.


Admin Panel: A secure area for administrators to manage the user base, including deleting users and resetting passwords.

Technology Stack
Backend
Framework: ASP.NET Core Web API (.NET 8)

Language: C#

Database: SQLite with Entity Framework Core

Authentication: ASP.NET Core Identity for secure cookie-based authentication


Architecture: Follows a professional, layered architecture (Controllers, Services, Repositories) for maintainability and separation of concerns.

Frontend
Framework: Vanilla JavaScript (no heavy frameworks)

Styling: Tailwind CSS for a modern and responsive UI

Structure: Static HTML files (login.html, dashboard.html, etc.) that communicate with the backend API.

External Services

DVSA (Driver and Vehicle Standards Agency): Used to verify vehicle details from a registration number, ensuring data integrity.

Setup and Installation
To run this project locally, you will need the .NET 8 SDK installed.

1. Configure API Keys
The application requires API credentials for the DVSA service. These are stored securely and should not be committed to source control.

Navigate to the OfficeParking directory in your terminal.

Initialize user secrets:

Bash

dotnet user-secrets init
Set the required secrets. You will need to obtain these from the DVSA API developer portal.

Bash

dotnet user-secrets set "DvsaApiSettings:ClientId" "YOUR_CLIENT_ID"
dotnet user-secrets set "DvsaApiSettings:ClientSecret" "YOUR_CLIENT_SECRET"
dotnet user-secrets set "DvsaApiSettings:ApiKey" "YOUR_API_KEY"
2. Run the Backend API
Navigate to the OfficeParking directory.

Restore the dependencies:

Bash

dotnet restore
Run the application:

Bash

dotnet run
The API will start, typically on https://localhost:7017 and http://localhost:5188. The application will also automatically apply database migrations and seed the initial data (admin user and parking spots) on the first run.

3. Run the Frontend
The frontend consists of static HTML files. You can open them directly in your browser, but for the best experience (and to avoid CORS issues), it's recommended to serve them using a lightweight server.

If you have Python installed, navigate to the frontend directory and run:

Bash

python -m http.server 8000
If you have Node.js installed, you can use the serve package:

Bash

npx serve -l 8000
Open your web browser and navigate to http://localhost:8000/login.html.

Usage
Admin Login will be provided separately.

Employee: Register a new account through the registration page.
