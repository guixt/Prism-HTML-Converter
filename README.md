HTML Code Formatter for PrismJS
Overview
We created this tool to efficiently format and highlight code snippets on our website. Our documentation contains many code examples, including GuiXT, ABAP, and other languages that are not well-supported by existing syntax highlighters. To ensure consistent and readable formatting, we use PrismJS for syntax highlighting and needed a way to semi-automate the conversion of code within HTML files.

This tool allows you to select code in an HTML preview and automatically convert it into properly formatted PrismJS syntax with line numbers and copy buttons.

🔹 Key Features
✔ Live HTML Preview – Select code directly in the integrated WebView2 browser
✔ PrismJS Integration – Converts selected code into PrismJS-compatible HTML
✔ Supports Multiple Languages – Works with GuiXT, ABAP, JavaScript, HTML, and VB.NET
✔ Backup & Restore – Automatically creates backups before modifications
✔ File Management – Move processed files to a "done" folder, restore previous versions, and delete files
✔ Keyboard Shortcuts – Fast editing with configurable hotkeys

🚀 How It Works
Select an HTML file from the file list
Highlight a code section in the preview window
Click "Replace Code" to format the selection with PrismJS syntax
Save & manage files – move completed files, restore backups, or delete unwanted content
If a placeholder (<div name="prism_placeholder"></div>) exists, it can be replaced with formatted code from a text input.

🛠 Installation & Setup
Clone the repository:
bash
Kopieren
Bearbeiten
git clone https://github.com/your-repo-name.git
Open the project in Visual Studio
Ensure WebView2 is installed
Run the application (F5)


💡 Summary
This tool streamlines the process of formatting GuiXT, ABAP, and other scripts for PrismJS. It allows users to select code inside an HTML preview, format it properly, and save changes with backups—saving time and ensuring a consistent display across our website.

Let me know if you'd like any refinements! 😊
