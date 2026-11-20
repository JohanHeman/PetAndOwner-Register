#this image includes tools to build and publish the project
FROM mcr.microsoft.com/dotnet/sdk:10.0 as build
WORKDIR /app
COPY . ./ 
RUN dotnet restore
RUN dotnet publish -o /app/out

# image containing all the tools for runtime purposes
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

#copies from the directory /app/out (the build ready for publish)
COPY --from=build /app/out . 


# the command to run when the container starts, to execute the .NET application
ENTRYPOINT [ "dotnet", "PetAndOwner.Api.dll" ]


