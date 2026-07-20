namespace SpireAgentOffice.Services;

/// <summary>
/// Shared helper for MIME types and file extensions across all document types.
/// </summary>
public static class DocumentFormatHelper
{
    #region MIME Types

    public static string GetMimeType(string saveFormat)
    {
        return saveFormat?.ToLowerInvariant() switch
        {
            // Common
            "pdf" => "application/pdf",
            "html" => "text/html",
            "xps" => "application/vnd.ms-xpsdocument",
            "txt" => "text/plain",
            "xml" => "application/xml",
            "md" or "markdown" => "text/markdown",
            "json" => "application/json",
            "png" => "image/png",
            "csv" => "text/csv",

            // Excel
            "xlsx" or "excel" or "xlsx2007" or "xlsx2010" or "xlsx2013" or "xlsx2016" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "xls" or "xls97" or "version97to2003" => "application/vnd.ms-excel",
            "xlsb" or "xlsb2007" or "xlsb2010" => "application/vnd.ms-excel.sheet.binary.macroEnabled.12",
            "xlsm" => "application/vnd.ms-excel.sheet.macroEnabled.12",
            "ods" => "application/vnd.oasis.opendocument.spreadsheet",
            "xlt" => "application/vnd.ms-excel",
            "xltx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.template",
            "xltm" => "application/vnd.ms-excel.template.macroEnabled.12",

            // Word
            "docx" or "word" or "word2007" or "word2010" or "word2013" or "word2016" or "word2019" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "doc" or "doc97" or "version97to2003" => "application/msword",
            "rtf" => "application/rtf",
            "odt" => "application/vnd.oasis.opendocument.text",
            "epub" => "application/epub+zip",
            "dotx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.template",
            "dotm" => "application/vnd.ms-word.template.macroEnabled.12",
            "docm" => "application/vnd.ms-word.document.macroEnabled.12",

            // PowerPoint
            "pptx" or "ppt" or "powerpoint" or "pptx2010" or "pptx2013" or "pptx2016" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            "pptm" => "application/vnd.ms-powerpoint.presentation.macroEnabled.12",
            "ppsx" => "application/vnd.openxmlformats-officedocument.presentationml.slideshow",
            "ppsm" => "application/vnd.ms-powerpoint.slideshow.macroEnabled.12",
            "potx" => "application/vnd.openxmlformats-officedocument.presentationml.template",
            "potm" => "application/vnd.ms-powerpoint.template.macroEnabled.12",
            "odp" => "application/vnd.oasis.opendocument.presentation",
            "tiff" or "tif" => "image/tiff",

            // Image / other
            "bmp" or "bitmap" => "image/bmp",
            "ps" or "postscript" => "application/postscript",
            "ofd" => "application/ofd",
            "pcl" => "application/vnd.hp-pcl",
            "svg" => "image/svg+xml",
            "emf" => "image/emf",
            "wmf" => "image/wmf",

            _ => "application/octet-stream"
        };
    }

    #endregion

    #region File Extensions

    public static string GetExtension(string saveFormat)
    {
        return saveFormat?.ToLowerInvariant() switch
        {
            // Common
            "pdf" => ".pdf",
            "html" => ".html",
            "xps" => ".xps",
            "txt" => ".txt",
            "xml" => ".xml",
            "md" or "markdown" => ".md",
            "json" => ".json",
            "png" => ".png",
            "csv" => ".csv",

            // Excel
            "xlsx" or "excel" or "xlsx2007" or "xlsx2010" or "xlsx2013" or "xlsx2016" => ".xlsx",
            "xls" or "xls97" or "version97to2003" => ".xls",
            "xlsb" or "xlsb2007" or "xlsb2010" => ".xlsb",
            "xlsm" => ".xlsm",
            "ods" => ".ods",
            "xlt" => ".xlt",
            "xltx" => ".xltx",
            "xltm" => ".xltm",

            // Word
            "docx" or "word" or "word2007" or "word2010" or "word2013" or "word2016" or "word2019" => ".docx",
            "doc" or "doc97" or "version97to2003" => ".doc",
            "rtf" => ".rtf",
            "odt" => ".odt",
            "epub" => ".epub",
            "dotx" => ".dotx",
            "dotm" => ".dotm",
            "docm" => ".docm",

            // PowerPoint
            "pptx" or "ppt" or "powerpoint" or "pptx2010" or "pptx2013" or "pptx2016" => ".pptx",
            "pptm" => ".pptm",
            "ppsx" => ".ppsx",
            "ppsm" => ".ppsm",
            "potx" => ".potx",
            "potm" => ".potm",
            "odp" => ".odp",
            "tiff" or "tif" => ".tiff",

            // Image / other
            "bmp" or "bitmap" => ".bmp",
            "ps" or "postscript" => ".ps",
            "ofd" => ".ofd",
            "pcl" => ".pcl",
            "svg" => ".svg",
            "emf" => ".emf",
            "wmf" => ".wmf",

            _ => $".{saveFormat?.TrimStart('.')}"
        };
    }

    #endregion

}
