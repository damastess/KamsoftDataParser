
# Kamsoft Data Parser API

A robust, minimal Web API built with .NET 8 designed to receive, decode, and parse Base64 encoded payloads containing either CSV or internal JSON data. The application utilizes a Strategy Pattern architecture for clean format parsing and includes a unified response model.

## 🚀 Technologies Used
* **Framework:** .NET 8.0 (Minimal APIs)
* **Language:** C# 12
* **CSV Parsing:** CsvHelper
* **Testing:** xUnit, Microsoft.AspNetCore.Mvc.Testing (In-Memory Integration Testing)
* **API Documentation:** Swashbuckle (Swagger)

---

## 🛠️ Getting Started

### Prerequisites
Make sure you have the [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your machine.

### Running the Application

1. **Clone the repository:**
   ```bash
   git clone https://github.com/damastess/KamsoftDataParser.git
   cd KamsoftDataParser

2. **Restore dependencies and build the solution:**
```bash
dotnet restore
dotnet build

```


3. **Run the API:**
   Navigate to the API project folder and run it:
```bash
cd DataParserApi
dotnet run

```


The terminal will output the local address where the application is listening (e.g., `http://localhost:5000` or `https://localhost:5001`).

**Sample request:**
```bash
curl -X POST http://localhost:XXXX/api/v1/parse-content \
-H "Content-Type: application/json" \
-d '{
  "type": "CSV",
  "content": "aWQsbmFtZQoxLFRlc3Q="
}'

```

## 📖 API Documentation (Swagger)

This API includes an interactive Swagger UI to explore and test the endpoints directly from your browser.

Once the application is running, open your web browser and navigate to:

`http://localhost:<port>/swagger`

*(Replace `<port>` with the actual port number shown in your terminal).*

Here, you can test the `POST /api/v1/parse-content` endpoint by sending a JSON payload containing the Base64 encoded string and the selected content type (`CSV` or `INTERNAL_JSON`).

## 🧪 Testing

This project maintains a high standard of quality assurance by separating tests into two distinct layers: Unit Tests and Integration Tests.

### Unit Tests

Located in the `DataParserApi.Tests` project, the unit tests cover:

* **Format Parsers (`CsvFormatParser`, `JsonFormatParser`):** Ensuring that both valid and malformed data structures are handled correctly, exceptions are caught, and unified objects are returned.
* **Service Logic (`DataParserService`):** Validating Base64 decoding, strategy selection, and proper error handling in absolute isolation using Dependency Injection.

### Integration Tests

The repository includes robust integration testing utilizing `WebApplicationFactory`. This spins up an in-memory test server to verify the entire request-response lifecycle. It ensures that HTTP status codes (e.g., 200 OK, 400 Bad Request), endpoint validation, and model bindings work together flawlessly as a complete system.

### How to Run the Tests

To execute the entire test suite, run the following command from the root directory of the solution:

```bash
dotnet test

```

## ⚠️ Troubleshooting

### NU1202 Error (Microsoft.AspNetCore.Mvc.Testing)

**The Issue:**

When adding the integration testing package via `dotnet add package`, NuGet might attempt to download the latest available version (e.g., 10.0.x) intended for a newer framework. This results in an incompatibility error with the .NET 8.0 target framework.

**The Solution:**

Explicitly specify the package version that aligns with the project's target .NET version. Run the following command inside the `DataParserApi.Tests` directory:

```bash
dotnet add package Microsoft.AspNetCore.Mvc.Testing --version 8.0.0

```

