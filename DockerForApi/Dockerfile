FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build

# Add Azure Artifacts Feed
ARG FEED_URL
ARG PAT
ARG USERNAME="Token"
ARG NUGET_PACKAGE_FEED_URL="https://api.feed.org/index.json"

RUN apk update                                                                                                                                                                                                                         
RUN apk add wget                                                                                                                                                                                                       
RUN apk add curl
RUN wget -qO- https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | /bin/sh

ENV NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED true
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS "{\"endpointCredentials\": [{\"endpoint\":\"${FEED_URL}\", \"username\":\"${USERNAME}\", \"password\":\"${PAT}\"}]}"

# Copy Files and Folders Across
WORKDIR /app
COPY . .

# Setup test image to run all tests except acceptance tests
FROM build AS testrunner
ARG FEED_URL
ARG NUGET_PACKAGE_FEED_URL
WORKDIR /app
RUN rm -R Project.AcceptanceTests
RUN dotnet sln Project.sln remove Project.AcceptanceTests/Project.AcceptanceTests.csproj
RUN dotnet restore --source ${NUGET_PACKAGE_FEED_URL} --source ${FEED_URL}
ENTRYPOINT ["dotnet", "test", "--logger", "trx", "--results-directory", "/app/testresults"]

# Setup runtime image
FROM build AS publish
ARG FEED_URL
ARG NUGET_PACKAGE_FEED_URL
WORKDIR /app/Project.Api
RUN dotnet restore --source ${NUGET_PACKAGE_FEED_URL} --source ${FEED_URL}
RUN dotnet build -c Release
RUN dotnet publish -c Release -o out

# Setup runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine AS runtime
COPY --from=publish /app/Project.Api/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "Project.Api.dll"]

# Setup acceptance tests image
FROM build AS acceptancetestrunner
ARG FEED_URL
ARG NUGET_PACKAGE_FEED_URL
WORKDIR /app/Project.AcceptanceTests
RUN dotnet restore --source ${NUGET_PACKAGE_FEED_URL} --source ${FEED_URL}
ENTRYPOINT ["dotnet", "test", "--logger", "trx", "--results-directory", "/app/acceptancetestresults"]

# Run the following to build the Navigation Api Docker image named asos/Project:0.1 
# docker build -f .\Project.Api\Dockerfile --build-arg PAT=<insert-value> --build-arg FEED_URL=<insert-value> --target runtime -t asos/Project:0.1 .

# Run the following an instance of the Navigation Api Docker image listening on port 5531 locally
# docker run -it --rm -p 5531:80 --env Logging__LogLevel__Default="Debug" --env ASPNETCORE_ENVIRONMENT="Development" --env Caching__RedisConnectionString="<insert-value>" asos/Project:0.1

# Run the following to build the Navigation Api Unit Tests Docker image named asos/Project.unittests:0.1 
# docker build -f .\Source\Project.Api\Dockerfile --build-arg PAT=<insert-value> --build-arg FEED_URL=<insert-value> --target testrunner -t asos/Project.unittests:0.1 .\Source

# Run the following to run the unitetsts container
# docker run -it --rm asos/Project.unittests:0.1