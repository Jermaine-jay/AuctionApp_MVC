# Jermaine Auction Car Auction Application
____
#### The Application is a web application built using ASP.NET MVC, Entity Framework Core, AutoMapper, and SQL Server. This application allows users to participate in online car auctions, providing a platform for buyers to bid on vehicles.


## Technologies Used
____
* ASP.NET MVC: The web framework used to build the application's architecture and handle user requests.

* AutoMapper: A mapping library used to simplify the process of mapping between different data models.

* SQL Server: The relational database management system used to store and manage application data.
  
* Entity Framework Core (EF Core): A powerful and flexible Object-Relational Mapping (ORM) tool for working with the application's database.
  > - *Microsoft Entity Framework core Design V 6.0.1*
  > - *Microsoft Entity Framework core Tools V 6.0.1*
  > - *Microsoft Entity Framework core SqlServer V 6.0.1*


## Features
____
* User Authentication and Authorization: Secure user registration and login system, ensuring that only authorized users can access and participate in auctions,
  Ensure only admins can make changes to the system. Note, that you cannot register with an invalid email address.

* User Update: Only confirmed users will be able to log in, place bids and change details. An Email with a token will be sent when a change of password is initiated.

* Auction Listings: Browse through a comprehensive list of available vehicles up for auction, including detailed information such as vehicle specifications, images, and starting bids.

* Real-Time Bidding: Engage in real-time bidding on your desired vehicles, with instant updates on current bids and countdown timers for the next auction.

* User Profiles: Users can view the status of previous bids and ongoing bids, as well as the status of cars they placed a bid. Users can change profile pictures as well as other details.

* Admin Profile: Admin can easily view vehicles for auction, set starting bids, track the progress of their auctions and change the status of an auction.

## How it works
_____
* Registration: The app uses an email system to confirm a user's email when they register, an email system when they request to change their password, and an email system that sends a link to their mail for password reset. The application also has well-secured authentication and authorization mechanisms.
  
* Email system: The email system uses a token-based approach to confirm a user's email address. When a user registers, a token is generated and sent to their email address. The user must then click on the link in the email to confirm their email address. This helps to prevent unauthorized users from creating accounts.
  
* Authentication and authorization: The application uses a combination of username and password authentication, as well as role-based authorization. This ensures that only authorized users can access the application.

## Getting Started
_____
* Clone the repository: git clone https://github.com/Germaine-jay/AunctionApp_MVC.git

* Install required packages: dotnet restore

* Update the database connection string in the appsettings.json file to point to your SQL Server instance.

* Apply database migrations: dotnet ef database update

* Run the application: dotnet run

* Access the application in your web browser at http://localhost:7121


## Contributing
_____
If you'd like to contribute to the Car Auction App, please follow these steps:

* Fork the repository.

* Create a new branch for your feature or bug fix: git checkout -b feature/your-feature-name

* Make your changes and test thoroughly.

* Commit your changes: git commit -m "Add your commit message here"

* Push to your forked repository: git push origin feature/your-feature-name

* Create a pull request, describing your changes and the problem they solve.

## Default Users
___
| UserName   | Password   | Role       |
| ---------- | ---------- | ---------- |
| jota10     | 12345qwert | User       |
| jermaine10 | 12345qwert | SuperAdmin |
| idan10     | 12345qwert | Admin      |  
