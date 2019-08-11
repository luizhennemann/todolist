
# **ToDo List Application**

### **Requirements:**

You are required to build a simple online TODO list with a web interface that can be used in all popular web browsers. The application has to Support multiple users, store necessary data in an in-memory database and be built with Microsoft .Net (either core or standard framework). Use of open-source frameworks is also allowed.

##### Minimal functionality:

  •	User can sign in using unique login and password securely (this can be hard coded to a default user list, at list one user e.g. with username: test, password: pwd123)
  •	User can view her/his task list
  •	User can add/remove task
  •	All changes can be persistent to allow view them in next sign in by the same user
  •	Each task should display the date of last updates and description
  •	User can check/uncheck any task on their list
  •	Consider performance

### **Approach:**

The application was build using C#, .Net Core and MVC. It is divided in two tiers: Presentation and API. This design was chosen in order to provide a service for various UI applications be able to consume the service. So, all the data and business logic are in only one place.

#### API:

The API contains the repository (data access layer), business processes and also the interface to expose the CRUD features to other applications. The repository pattern is used to provide an interface to the business layer containing all relevant database operations. The database operations (DDL/DML) are executed using Entity Framework and Migrations. 

The business layer is where all the business logic is executed. Currently, it is being tested automatically by another project where the Unit Tests reside. The unit tests were build using xUnit and Moq.

The API is exposed to the clients through Rest calls. All Http/MVC standard methods are being offered as the main entrance to the API (GET/POST/PUT/DELETE). 

All layers are being injected using the embedded dependency injection offer by .Net Core. This approach was chosen in order to allow the injected components to be mocked when writing unit tests. Also to remove the dependencies from inside the classes, applying the inversion of control concept. And finally, to be able to change the layers without changing the whole solution.

#### UI - Presentation:

The UI was built using Asp.Net Mvc, Razor Views and a service interface to consume the API. The service interface is being injected using the embedded .net core dependency injection. The service is responsible for supply the controllers with all the CRUD operations offered by the API. The UI is pretty standard, the boiler plate UI offered on Visual Studio was used, because the focus here was in the architecture and not the user experience.

## **Improvements (not done because of lack of time):**

#### **LOGIN:**

The application is not secured. The login page is just a mock and currently two users are hard coded in the controller (feel free to add more on the list in the HomeController):

  •	User: deloitte - Password: 123
  •	User: luizmarcelo - Password: 789

The WebApi also is not secured. I have in mind to implement security using Identity offered by .Net Core. I decided not go for it this time because of the complexity it could add and I am not sure I would be able to finish everything in three days.

#### **UI TESTING:**

I have in mind to add another project in the UI solution to write system tests for the application. Using Spec Flow and Selenium WebDriver it would be possible to automate the interactions with the UI and have it recorded to run Regression before deployment to production.

#### **AZURE:**

I have in mind to deploy the applications to azure to make sure they are running correctly in production. Also, once the API is up and running on Azure, I would like to create a different UI (maybe using Angular) to access the backend layer and expose the features in a more friendly UI.

