# Star Waze

This is a console app that calculates the number of resuply stops needed for a Star Wars spaceship to travel a given distance.
The user must enter the distance in MGLTs and the app will list all the spacecrafts from SWAPI.co and the number of stops required.

## Premises
The following premises were considered for the business rules
 - Ships with unknown MGLT or Consumables are displayed with # of required stops as "unknown"
 - Consumables measurement units:
 - - Month - 30 days
 - - Week - 7 days
 - - Year - 365 days
 - - Day - 24 hours

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

The application runs in .NET Core 3.0.1.

### Running

Just clone the app, open in Visual Studio and run the Star Waze console app. You must enter a valid decimal value for MGLT.

## Running the tests

The unit tests were developed using xUnit. The unit testing for the Gateway layer is a ToDo :)


## Frameworks, tools and packages used

* .Net Core 3.0.1
* xUnit
* Visual Studio for Mac

## Authors

* **Vitor Cunha** - *Initial work* - (https://github.com/vpopolin)

## License

This project is licensed under the MIT License