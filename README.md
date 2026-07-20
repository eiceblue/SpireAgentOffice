## **Embed Office AI Agents Directly Into Your Software**

[Product Page](https://www.e-iceblue.com/introduce/spire-agent-xls.html) | [Tutorials](https://www.e-iceblue.com/Tutorials.html) | [Demo](https://www.e-iceblue.com/Introduce/spire-office-for-net/demo.html) | [Examples](https://github.com/eiceblue) | [Forum](https://www.e-iceblue.com/forum/) | [Blog](https://www.e-iceblue.com/blog.html)  | [Customized Demo](https://www.e-iceblue.com/freedemo.html)  | [Temporary License](https://www.e-iceblue.com/TemLicense.html)

### Transforms natural language into fixed document processing workflows. Our enterprise-level component libraries enable smooth API integration and support Word, Excel, presentations, PDF and more file formats.

### **About Spire.Agent.Office**

**Spire.Agent.Office** empowers users to create, edit, analyze, and automate spreadsheets, Word documents, presentation slides, and PDFs via natural language. Build your AI Agent with Spire.Agent.Office today.


### Why Enterprises Choose Spire.Agent.Office

- **AI-Native Experience**
Interact with Excel via natural language, eliminating complicated development workflows.

- **Enterprise Reliability**
Built on mature, verified spreadsheet processing technology optimized for production use cases.

- **Easy Integration**
Embed Excel AI functions into your current applications and services with ease.

- **Flexible AI Model Support**
Fully compatible with state-of-the-art AI models and enterprise AI infrastructures.

- **Scalable Processing**
Efficiently process single requests and large-volume automated workloads alike.

- **Accelerated Productivity**
Slash document processing time by 60% and maintain over 95% accuracy in code generation.

### **Supported Platforms & File Formats**

Supported Development Platforms

- .NET 10.0
- Cross-platform: Windows, macOS, Linux, Docker container deployment

Supported Document Formats

- Excel: .xls, .xlsx, .xlsm, .xlsb
- Word: .doc, .docx, .docm
- Presentation: .ppt, .pptx, .pptm
- PDF: Standard PDF, text-based PDF (encrypted PDF with password supported)

| Parameter | Description |
| ---- | ---- |
| SpireToken | Authorization key for activating AI features, obtained from official website |


### AI Agent Example for Excel Processing
```C#
//XLS
static AIResult ExecuteDemoXls(string instruction, string inputPath, string savePath, string key, string[] attachmentPaths)
{
    AIOptions options = new AIOptions();
    options.SpireToken = key;

    using (Workbook workbook = new Workbook())
    {
        // Load the document if the input path exists and the file is accessible
        if (!string.IsNullOrEmpty(inputPath) && File.Exists(inputPath))
        {
            workbook.LoadFromFile(inputPath);
        }
        // Otherwise, use an empty Workbook (adjust behavior as needed for your business logic)

        AIDocumentProcessor processor = workbook.AI(options);
        return processor.ExecuteInstruction(workbook, instruction, savePath, attachmentPaths);
    }
}
```

### AI Agent Example for Word Processing
```C#
//doc
static AIResult ExecuteDemoWord(string instruction, string inputPath, string savePath, string key, string[] attachmentPaths)
{
    AIOptions options = new AIOptions();
    options.SpireToken = key;

    using (Document doc = new Document())
    {
        // Load the document if the input path exists and the file is accessible
        if (!string.IsNullOrEmpty(inputPath) && File.Exists(inputPath))
        {
            doc.LoadFromFile(inputPath);
        }
        // Otherwise, use an empty Document (adjust behavior as needed for your business logic)

        AIDocumentProcessor processor = doc.AI(options);
        return processor.ExecuteInstruction(doc, instruction, savePath, attachmentPaths);
    }
}
```

### AI Agent Example for PDF Processing

```C#
//PDF
static AIResult ExecuteDemoPDF(string instruction, string inputPath, string savePath, string key, string[] attachmentPaths)
{
    AIOptions options = new AIOptions();
    options.SpireToken = key;

    using (PdfDocument pdf = new PdfDocument())
    {
        // Load the document if the input path exists and the file is accessible
        if (!string.IsNullOrEmpty(inputPath) && File.Exists(inputPath))
        {
            pdf.LoadFromFile(inputPath);
        }
        // Otherwise, use an empty PdfDocument (adjust behavior as needed for your business logic)

        AIDocumentProcessor processor = pdf.AI(options);
        return processor.ExecuteInstruction(pdf, instruction, savePath, attachmentPaths);
    }
}
```

### AI Agent Example for Presentation slides Processing

```C#
//PPT generation 
static PPTGenerationResult GeneratPPT(string input, string instruction, string savePath, string key)
{
    AIOptions options = new AIOptions();
    options.SpireToken = key;
    using (Presentation ppt = new Presentation())
    {
        AIDocumentProcessor processor = ppt.AI(options);
        return processor.GeneratePresentation(input, instruction, savePath);
    }
}

//Based on existing PPT processing
static AIResult ExecuteDemoPPT(string inputPath, string instruction, string savePath, string key, string[] attachmentPaths)
{
    AIOptions options = new AIOptions();
    options.SpireToken = key;

    using (Presentation ppt = new Presentation())
    {
        // Load the document if the input path exists and the file is accessible
        if (!string.IsNullOrEmpty(inputPath) && File.Exists(inputPath))
        {
            ppt.LoadFromFile(inputPath);
        }
        // Otherwise, use an empty Presentation (adjust behavior as needed for your business logic)

        AIDocumentProcessor processor = ppt.AI(options);
        return processor.ExecuteInstruction(ppt, instruction, savePath, attachmentPaths);
    }
}
```
### **Frequenly Asked Questions/ FAQ**

- **Token Invalid Error**

  Check whether the SpireToken is copied completely without extra spaces.

  Temporary token expires after a certain period, re-apply a new license.

- **File Loading Failed**

  Confirm the file path is correct and the file is not occupied by other software.

  Encrypted files need to input password before loading.

- **AI Task Timeout**

  Large-size documents or complex logic will take longer; split batch tasks to improve stability.

- **Low Code Generation Accuracy**

  Optimize natural language instructions with clear, detailed requirements.

  Attach reference documents to provide more data context for AI.

[Product Page](https://www.e-iceblue.com/introduce/spire-agent-xls.html) | [Tutorials](https://www.e-iceblue.com/Tutorials.html) | [Demo](https://www.e-iceblue.com/Introduce/spire-office-for-net/demo.html) | [Examples](https://github.com/eiceblue) | [Forum](https://www.e-iceblue.com/forum/) | [Blog](https://www.e-iceblue.com/blog.html)  | [Customized Demo](https://www.e-iceblue.com/freedemo.html)  | [Temporary License](https://www.e-iceblue.com/TemLicense.html)