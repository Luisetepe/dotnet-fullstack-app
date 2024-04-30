# Fullstack Angular SPA App + .Net WebAPI

## :construction: Work in progress :construction:

This repository contains an always-improving fullstack [Angular](https://angular.io/) app backed by a [.Net](https://dotnet.microsoft.com/) REST Api and a [PostgreSQL](https://www.postgresql.org/) database.

## Editor configuration

The repository is configured so it remains as cross-platform as possible (I personally hate Windows and Visual Studio for development, and specially hate propietary dev pipelines that only work in that environment). Right now I'm mainly developing this on macOS.

Right now I'm using Visual Studio Code for the entire codebase, and there is a `.vscode` folder with the recommended configurations and extensions. These extensions are:

- [Angular templates](https://marketplace.visualstudio.com/items?itemName=Angular.ng-template)
- [Better Comments](https://marketplace.visualstudio.com/items?itemName=aaron-bond.better-comments)
- [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
- [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [.Net Install Tool](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-runtime)
- [Prettier](https://marketplace.visualstudio.com/items?itemName=esbenp.prettier-vscode)
- [Biome](https://marketplace.visualstudio.com/items?itemName=biomejs.biome)
- [CSharpier](https://marketplace.visualstudio.com/items?itemName=csharpier.csharpier-vscode)
- [TailwindCSS IntelliSense](https://marketplace.visualstudio.com/items?itemName=bradlc.vscode-tailwindcss)
- [NuGet Package Manager GUI](https://marketplace.visualstudio.com/items?itemName=aliasadidev.nugetpackagemanagergui)

These extensions should appear in the `Extensions` menu inside VSCode if you type `'@recommended'` in the search bar.

For linting and formating, I'm using `BiomeJS` and `Prettier` for frontend (Prettier for HTML files, BiomeJS for anything else) and `CSharpier` for the C# backend. You only need to have the previously mentioned VSCode extensions installed, and code should format every time you save the file.

**For `CSharpier`, you must install the formatter as a `dotnet tool`** (you may see a popup on the corner when you install the VSCode extension asking you to install it):

```powershell
dotnet tool install --global csharpier
```

## Dev environment configuration

### Frontend

The Angular part of the application needs `NodeJS` and `pnpm` for package management (`npm`is just too slow):

- [NodeJS 20.X LTS](https://nodejs.org/en/download)
- [pnpm 8.X](https://pnpm.io/installation)

### Backend

For the dotnet REST Api you only need the latest `.Net SDK`:

- [.Net 8 SDK](https://dotnet.microsoft.com/en-us/download)

### Database

For database, you just need to have a PostgreSQL database available wherever you want and configure the Connection String as an environment variable as its described in the `Local Development` section.

I usually use docker for these things, so install a Docker engine if you haven't already (I recommend using [Rancher Desktop](https://rancherdesktop.io/)), create a virtual volume to save the database data, and spin up a PostgreSQL container:

```powershell
docker volume create pgData
```

```powershell
docker run -d --name local-postgres -p 55000:5432 -e POSTGRES_PASSWORD=postgrespw -e PGDATA=/var/lib/postgresql/data/pgdata -v pgData:/var/lib/postgresql/data postgres
```

## Local development

First, we need to prepare de Database with example data. For this kind of things, I've created a C# console app inside the dotnet Solution that will run some tasks depending of the parameter we pass when executing it. For now it only reacts to the `seed` parameter, so once you have your PostgreSQL accessible, configure the connection string inside both files (I'm using the ones that I set up earlier with docker, the '`Database`' field is the name of tyour database, use whatever you want):

```jsonc
// file -> api/src/WebApp.Template.Tools/appsettings.json
{
  "WebAppDb": "Host=localhost;Port=55000;Database=WebAppDb;Username=postgres;Password=postgrespw",
}
```

```jsonc
// file -> api/src/WebApp.Template.Api/appsettings.Development.json
"ConnectionStrings": {
  "WebAppDb": "Host=localhost;Port=55000;Database=WebAppDb;Username=postgres;Password=postgrespw"
}
```

Then you would be able to run the tool and seed the database:

```powershell
dotnet run --project api/src/WebApp.Template.Tools seed
```

The tool should ask you to confirm you want to earse whatever there was in the database and re-create it.

Now we can move on into starting both our Angular dev server and .Net dev server. I'm using scripts inside the `package.json` file to centralize these common tasks, so you dont have to go changing directory in order to run the frontend or the backend.

First we install all the dependencies:

```powershell
pnpm install
```

And then in separate terminals we can run:

```powershell
# For the Angular dev server
pnpm run start:app
```

```powershell
# For the .Net web api
pnpm run start:api
```

You can access the web app at `http://localhost:4200` and the swagger page for the REST Api at `http://localhost:5224/swagger`

## Tests

For now, there are only backend integration tests, which will spin up a postgres docker container using [TestContainers](https://testcontainers.com/) library. To run them:

```powershell
pnpm run test:api
```