# Spire.Agent.Office Multi-Document Processing Demo

The **ASP.NET Core Web Program** built on .NET 10 enables natural language-driven processing of Word, Excel, PDF, and PowerPoint documents — supporting conversion, editing, and automated operations with zero manual formatting.

---

## Project Structure

```
SpireAgentOffice/
|-- lib/                                # This project imports the "Spire.Agent.Office*.dll" core library locally, also supports Nuget import
|   |-- Spire.Agent.Office.dll
|   |-- Spire.Agent.Office.Core.dll
|   |-- Spire.Agent.Office.Abstractions.dll
|   |-- Spire.Agent.Office.Utils.dll
|   |-- Spire.Agent.Office.AutoFix.dll
|   |-- ...
|
|-- Data/                               # Preset document templates
|   |-- Sample.xlsx
|   |-- Sample.docx
|   |-- Sample.pdf
|   |-- Sample.pptx
|
|-- Program.cs                          # Application entry point, service registration & middleware configuration
|-- SpireAgentOffice.csproj            # Project file (target framework: net10.0)
|-- SpireAgentOffice.http              # API test file (VS Code REST Client)
|-- appsettings.json                    # Application configuration (includes SpireAI:ApiKey)
|-- appsettings.Development.json        # Development environment configuration
|
|-- Controllers/
|   |-- AIAgentController.cs            # API controller - receive/process/return for 4 document types
|
|-- Models/
|   |-- AIExecuteRequest.cs             # Request model (Instruction + SaveFormat)
|
|-- Services/
|   |-- DocumentFormatHelper.cs         # Shared utility - MIME type & extension mapping
|   |-- SpireXlsService.cs              # Excel AI processing service (Sample.xlsx)
|   |-- SpireWordService.cs             # Word AI processing service (Sample.docx)
|   |-- SpirePdfService.cs              # PDF AI processing service (Sample.pdf)
|   |-- SpirePptService.cs              # PowerPoint AI processing service (Sample.pptx)
|
|-- wwwroot/
|   |-- test.html                       # Frontend visual test page
|
|-- Properties/
|   |-- launchSettings.json             # Launch configuration (port: 5184)
```

---

## Configuration

### appsettings.json Configuration

```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",
    "SpireAI": {
        "ApiKey": "your-spire-api-key-here"  
    }
}
```

## Quick Start

### 1. Run the Project

**Using startup scripts (recommended):**
Windows:Double-click `run.bat` or run `run.bat` in terminal 
Linux:Run `./run.sh` in terminal (If you are using it in a Docker environment, please make sure to install the corresponding fonts to ensure the correct effect)

### 2. Get API Key

Please contact our sales team (sales@e-iceblue.com) to obtain a trial/commercial API Key. You can also request it from the following URL:
https://www.e-iceblue.com/TemLicense.html

### 3. Test Page

```
http://localhost:5184/test.html
```

### 4. AI-Powered Document Processing

Set SpireToken Key → Choose Output Format → Enter Instruction → Click Process

---
