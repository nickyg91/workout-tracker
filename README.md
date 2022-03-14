# workout-tracker

# Running EF Migrations
Change directory to WorkoutTracker.Api. The migrations run from the API executable.
## Running Upgrades
Run dotnet ef database update in a terminal.
If you do not have the tools installed, run dotnet tool install --global dotnet-ef

## Adding Migrations
To add a migration, run the following command: dotnet ef migrations add <name>
