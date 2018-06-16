FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app
COPY ./ .
WORKDIR /app/ConsoleTest
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-runtime AS runtime
WORKDIR /app
COPY --from=build /app/ConsoleTest/out ./
ENTRYPOINT ["dotnet", "ConsoleTest.dll"]