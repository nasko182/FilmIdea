# FilmIdea-Web-Application
ASP .NET Core MVC application designed for peoples who want to find the best movie to watch.

Project Introduction: FilmIdea
FilmIdea is a dynamic ASP.NET Core MVC project that I developed as part of my coursework at SoftUni. This web application is designed to cater to individuals looking for the perfect movie to watch or critics seeking to share their movie reviews with the community. By combining essential features such as movie recommendations, group interactions, and watchlists, FilmIdea offers a rich and engaging movie-lover experience.

Key Features:
Movie Recommendations: FilmIdea assists users in discovering the best movies to watch. The platform also give users opportunity for becoming criticas and leave their reviews to any movie.

Group Interactions: Users can create and join groups to connect with fellow movie enthusiasts. Each group provides a dedicated space for members to discuss movies, exchange reviews, and share their cinematic experiences.

Group Watchlist: Every group has a unique watchlist that compiles movies favored by all members. This collective watchlist ensures that only the most highly recommended movies make the cut, simplifying the decision-making process for the entire group.

Individualized Swipe Page: Users can access their personal swipe page, where they can effortlessly explore movie options. They can swipe through movie suggestions, either adding them to their watchlist or passing on them.

Technical Excellence:
FilmIdea adheres to the principles of Object-Oriented Design, emphasizing a modular and maintainable codebase. By following best practices, the project ensures a high-quality and efficient web application. With its foundation in ASP.NET Core MVC.

Built With
FilmIdea is crafted using a diverse range of cutting-edge technologies and tools to deliver a robust and user-friendly movie-related experience. The project is built upon:

ASP.NET Core 6 MVC: Leveraging the latest version of ASP.NET Core MVC framework for building dynamic and responsive web applications.

ASP.NET Core Areas: Utilizing ASP.NET Core's area feature to modularize and organize the application into separate functional sections.

MSSQL Server: Employing Microsoft SQL Server as the backend database management system to efficiently store and manage data.

Entity Framework Core: Harnessing the power of Entity Framework Core for seamless data access and management through an object-oriented approach.

AJAX: Incorporating AJAX (Asynchronous JavaScript and XML) for enhancing user interactions and providing a smoother browsing experience.

jQuery: Integrating jQuery, a fast and feature-rich JavaScript library, to simplify DOM manipulation and event handling.

NUnit: Implementing NUnit, a unit testing framework for .NET applications, to ensure code reliability and maintainability.

SignalR: Utilizing SignalR to enable real-time communication between users, facilitating interactive discussions and engagement.

Dropbox: Integrating Dropbox API for efficient storage and retrieval of files, contributing to the seamless sharing of resources.

Bootstrap: Incorporating Bootstrap framework for responsive and visually appealing UI design, ensuring compatibility across various devices.

Font Awesome Icons: Enhancing the visual aesthetics with a wide range of Font Awesome icons for intuitive navigation and visual cues.

Functionality
FilmIdea delivers an array of functionalities that cater to movie enthusiasts and critics alike:

User Registration: Seamless user registration process allowing individuals to create accounts and access the application's features.

Personalized Watchlist: Empowering users to curate their own movie watchlists, making it easy to keep track of desired films.

Movie Ratings: Enabling users to rate movies, providing valuable insights into the community's preferences.

Critic Reviews: Facilitating critics to contribute in-depth reviews for movies, enhancing the platform's diversity of perspectives.

With these features, FilmIdea creates a comprehensive and engaging platform that enriches the movie-watching experience and encourages meaningful interactions within the community.

Project Architecture
FilmIdea is structured using a straightforward yet effective architecture, comprising nine distinct projects that work in harmony to create a seamless and scalable web application:

FilmIdea.Web: This project serves as the backbone of the application, built on the ASP .NET Core Web App MVC framework. It encapsulates the user interface and user experience, offering a dynamic and responsive front-end for interaction.

FilmIdea.Web.Infrastructure: Housed in a class library, this project is responsible for holding extensions and middlewares. It contributes to the project's modularity by providing reusable components that enhance the application's functionality.

FilmIdea.Web.ViewModels: Also in a class library, this project holds the view models used to transfer data between the controller and views. These view models streamline data handling and presentation, promoting a separation of concerns.

FilmIdea.Data: Operating as a class library, this project encompasses crucial database-related components. It hosts the DBContext, migrations, configurations, and seeds, ensuring a structured and organized approach to data management.

FilmIdea.Data.Models: As another class library, this project holds the DB models that define the structure of the database entities. These models establish a clear representation of the application's data, aiding in consistency and clarity.

FilmIdea.Services: This class library project is dedicated to hosting services. Services encapsulate business logic and operations, promoting a modular and maintainable architecture.

FilmIdea.Services.Models: Housed in a class library, this project is responsible for holding service models. These models are specifically designed to facilitate data exchange between different layers of the application.

FilmIdea.Test: In this NUnit Test Project, comprehensive service tests are conducted to ensure the reliability and accuracy of the services provided by the application. These tests validate the functionality and stability of critical components.

This architecture fosters clear separation of concerns, promotes code reusability, and supports ease of maintenance and future expansion. By dividing the application into distinct layers and modules, FilmIdea is poised to accommodate growth and changes while maintaining a high standard of code quality.


Quick Start
To quickly get started with FilmIdea, follow these steps:

Log in using the following pre-seeded user credentials:

Administrator
Username: Administrator
Password: 123456

User
Username: user182
Password: 123456

Critic
Username: criticCriticov
Password: 123456

Explore the various features and functionalities of FilmIdea, including movie recommendations, watchlists, reviews, and group interactions. You can also register new users if desired.

Database Diagram
![Database Diagram](ReadMeImages/diagram.png)

Test Coverage
![Test Coverage](ReadMeImages/testCoverage.png)

App Images

Home Page
![Home Page](ReadMeImages/home.png)

All Movies Page
![All Movies Page](ReadMeImages/all.png)

Roulette Page
![Alt Text](ReadMeImages/roulette.png)

Swipe Page
![Roulette Page](ReadMeImages/swipe.png)

User Watchlist
![User Watchlist](ReadMeImages/watchlist.png)

User All Groups Page
![User All Groups Page](ReadMeImages/allGroups.png)

Group Page
![Group Page](ReadMeImages/group.png)

Movie Details Page
![Movie Details Page](ReadMeImages/movieDetails.png)

Actor Details Page
![Actor Details Page](ReadMeImages/actorDetails.png)

Director Details Page
![Director Details Page](ReadMeImages/directorDetails.png)