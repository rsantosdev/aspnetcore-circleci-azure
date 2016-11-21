FROM microsoft/dotnet:latest

COPY src/WebApi/bin/Release/netcoreapp1.1/publish/ /root/

EXPOSE 5000/tcp

WORKDIR /root

ENTRYPOINT dotnet WebApi.dll