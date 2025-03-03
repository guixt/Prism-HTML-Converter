# **HTML Code Formatter & Highlighter Converter**  

## **Overview**  
This Windows Forms application (built with VB.NET) is designed to streamline the process of modifying HTML files by enhancing code blocks with proper syntax highlighting. It supports the automatic replacement of existing formatted code (such as those generated with external tools like `hilite.me`) with a unified PrismJS-based syntax highlighter.

The tool allows users to:
- **Automatically insert PrismJS script and stylesheets** into HTML files.
- **Identify and replace preformatted code blocks**, even when they come from different sources.
- **Select and replace code sections directly** from the WebView2 preview.
- **Synchronize scrolling** between two WebView2 instances to maintain visual consistency.
- **Keep track of modifications** with a built-in logging system.

---

## **Features**  

✅ **Batch Processing:** Load and modify multiple HTML files at once.  
✅ **PrismJS Integration:** Automatically replaces old syntax-highlighted blocks with PrismJS-based formatting.  
✅ **Smart Code Detection:** Supports structured replacements using different detection methods, including:
  - `hilite.me` generated sections (by detecting the associated comment)
  - User-selected code regions in the WebView2 preview  
✅ **Backup System:** Creates `.old` backups before modifying files.  
✅ **Real-time Preview:** See your modifications instantly in WebView2.  
✅ **Scroll Synchronization:** Keeps two WebView2 instances aligned when scrolling.  
✅ **Logging & Debugging:** Built-in log output to track changes and replacements.  

---

## **How It Works**  

1. **Select an HTML File:** Choose an HTML document from a directory.  
2. **Preview the File:** The embedded WebView2 displays the content.  
3. **Choose a Code Block:**  
   - Select a code section directly in the preview.  
   - If a `hilite.me` comment is detected, the tool will automatically find and replace the associated `<div>`.  
4. **Apply the Replacement:**  
   - The selected block is replaced with PrismJS-compatible HTML.  
   - The `hilite.me` comment (if present) is removed to avoid duplicate replacements.  
5. **Save & View:** The file is updated, and the changes are reflected in the preview.  

---

## **Installation & Requirements**  

### **Prerequisites**
- Windows OS  
- .NET Framework (4.8 or later)  
- WebView2 Runtime installed  

### **Setup**
1. Clone this repository:  
   ```sh
   git clone https://github.com/yourusername/yourrepository.git
   ```
2. Open the project in **Visual Studio**.  
3. Restore NuGet packages (e.g., HtmlAgilityPack, Newtonsoft.Json).  
4. Compile and run the application.  

---

## **Screenshots**  

📌 *[Add some screenshots here for better understanding]*  

---

## **Planned Features**  
🔄 **Drag & Drop Support** – Quickly add HTML files by dragging them into the interface.  
📂 **Directory Monitoring** – Watch a folder for new HTML files and process them automatically.  
🌐 **Cloud Sync** – Option to sync processed files to a cloud storage service.  

---

## **Contributing**  
Contributions are welcome! Please follow these steps:  
1. Fork the repository  
2. Create a new branch (`feature-xyz`)  
3. Commit your changes  
4. Open a pull request  

---

## **License**  
This project is licensed under the MIT License – feel free to use and modify it!  

---

Would you like me to customize anything further, such as adding installation instructions, example screenshots, or expanding on certain features? 😊
