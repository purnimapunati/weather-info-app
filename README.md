# WeatherInfo Project
This is a full-stack web application that provides weather data (description).
 The frontend is built with **React**, and backend is **.NET Core** (C#). The application takes city and country as imputs from user and displays Weather description.
 weather data is fetched from third part API called OpenWeatherMap.Before calling OpenWeatherMap there are certain validations that will run like Authentication, rate limiting, and if inputs given are not empty.
 Unit test cases are written to ensure the backend logic is functioning as expected.

## Table of Contents
1. [Features](#features)
2. [Technologies Used](#technologies-used)
3. [Setup and Installation](#setup-and-installation)
   - [Frontend Setup](#frontend-setup)
   - [Backend Setup](#backend-setup)
   - [Running Unit Tests](#running-unit-tests)
4. [Configuration](#configuration)
5. [Folder Structure](#folder-structure)

## Features

- **Real-time Weather Data**: Fetches and displays weather details (description).
- **Authentication**: 5 APIKEYs are configured to validate.
- **RateLimiting**: Each key has rate limit of 5 reuests with in an hour.
- **Interactive UI**: Frontend built with **React** for a dynamic user experience.
- **Backend API**: Built using **.NET Core** (C#) to manage weather requests and logic.
- **Unit Tests**: Written in **XUnit** to test backend functionality, ensuring reliable performance.

## Technologies Used

- **Frontend**: React, Axios
- **Backend**: .NET Core, C#
- **Testing**: XUnit, Moq

## Setup and Installation

### Prerequisites

Make sure you have the following installed:
- **Node.js** for the React frontend
- **.NET Core SDK** for the backend API
- other necessary dependencies like Visual Studio

### Frontend Setup

1. open the **frontend** directory weatherinfo.ui (Preferably Visual Studio code).
2. Install the required dependencies by running npm install.
3. Start the development server npm start.
4. Your React frontend will now be available at `http://localhost:3000` or  `http://localhost:3001`.
 (Most likely if not we need to add the running localhost url in progracm.cs in backend at line number 44, 45 ).

### Backend Setup

1. Open the Backend solution on your Prefered IDE (Preferably Visual Studio 2022).
2. Run the backend application.
3. we can run this in different modes like https, http, IIS Express etc.
4. Your backend API will now be running at `http://localhost:5227` or `https://localhost:7237` or other.
5. This running localhost url need to be updated in frontend project in weather.js file on line number 22 and in unit test project at AuthenticationTests.cs line 8 and ApiBaseUrl line 8.

### Running Unit Tests

1. Open the Backend solution on your Prefered  IDE (Preferably Visual Studio  2022).
2. Run the unit tests (ensure BAckend API is running and URl is configured).
3. This will run all the unit tests and display the results all the unit should pass.

## Configuration
1. 5 APIKEYs are configured in Authentication -> ApiKeys.
2. OpenWeatherMap config in OpenWeatherMapAPI -> {EndPointUrl,ApiKeys(OpenWeatherMap api keys)}.
3. ClientRateLimiting defines RateLimiting config.
4. ClientRateLimitPolicies define client policies for example requests per api key.
Not much configuration is required. all are pretty much set up in appsetting.json file.

## Folder Structure

weather-info-app/
│
├── weatherinfo.ui/                 # React frontend application main folder are src and package.json
│   
└── WeatherInfo.Api/                # .NET Core backend application
    ├── WeatherInfo.Api/            # Controllers, Swagger Extensions, Models, Authentication, RateLimiting  
    ├── WeatherInfo.Infra/          # OpenWeatherMap API integration 
    ├── WeatherInfo.Service/        # Business logic
    └── WeatherInfo.Api.sln         # .NET solution file
    └── WeatherInfo.Tests/          # Unit tests

