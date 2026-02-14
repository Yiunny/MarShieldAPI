# --- Giai đoạn 1: Build ---
# Dùng bộ SDK của .NET 8 để biên dịch code
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy file .csproj và tải các thư viện về
COPY *.csproj ./
RUN dotnet restore

# Copy toàn bộ code còn lại và build ra thư mục 'out'
COPY . ./
RUN dotnet publish -c Release -o out

# --- Giai đoạn 2: Run ---
# Dùng bộ Runtime nhẹ hơn để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Mở port 8080 (Mặc định của .NET 8)
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

# Chạy file DLL (Tên này phải trùng với tên Project của bạn)
ENTRYPOINT ["dotnet", "MarShield.API.dll"]