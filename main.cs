using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using System.Xml;

namespace EBook_Reader_by_purplerain
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        public string bookText;
        public string formattedText;
        public string selectedFile;

        private void Form1_Load(object sender, EventArgs e)
        {
            string curDir = Directory.GetCurrentDirectory();
            webBrowser1.Url = new Uri(String.Format("file:///{0}/html_templates/start.html", curDir));
            webBrowser1.ScrollBarsEnabled = true;
            Console.WriteLine("Debug output console OK.");
        }

        //tsundouku
        private void fullScreen_Click(object sender, EventArgs e)
        {
            //Change Icon
            if (fullScreen.ImageIndex == 1) fullScreen.ImageIndex = 0; else fullScreen.ImageIndex = 1;

            //Apply fullscreen/shrink
            if (fullScreen.ImageIndex == 1)
            {
                WindowState = FormWindowState.Normal;
                TopMost = true;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Maximized;
            }
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "FantasyBook2 (*.fb2)|*.fb2|HTML files (*.html*)|*.html*";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ShowDialog();
        }

        private void fontSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not supported in this version. It will appear in the next update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void openSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not supported in this version. It will appear in the next update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void about_Click(object sender, EventArgs e)
        {
            Form aboutForm = new about();
            var dialogResult = aboutForm.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            selectedFile = openFileDialog1.FileName;
            bookText = File.ReadAllText(selectedFile);
            if (Path.GetExtension(selectedFile) == ".fb2")
            {
                fb2Render();
            }
            else
            {
                htmlCopy();
            }
            

        }
        private void htmlCopy()
        {
            bookText = File.ReadAllText(selectedFile);
            // After finishing, write to file
            string curDir = Directory.GetCurrentDirectory();
            File.WriteAllText(String.Format("{0}/html_templates/book.html", curDir), bookText);
            // Load the file to the web browser
            webBrowser1.Url = new Uri(String.Format("file:///{0}/html_templates/book.html", curDir));

        }
        private void fb2Render()
        {
            bool skipTagContent = false;    // If the tag is omitted, the content of the tag will not be printed
            XmlTextReader xmlReader = new XmlTextReader(openFileDialog1.FileName);
            // Add HTML template code
            formattedText = formattedText + "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n  <meta charset=\"utf-8\">\r\n  <title></title>\r\n  <link href=\"default.css\" rel=\"stylesheet\" />\r\n</head>\r\n<body style=\"overflow:auto;\">";
            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        //Check tag and convert to HTML
                        switch (xmlReader.Name)
                        {
                            // Check which tag it is and convert to HTML tag
                            case "p":
                                formattedText = formattedText + "<p>&emsp;&emsp;";
                                skipTagContent = false;
                                break;
                            case "emphasis":
                                formattedText = formattedText + "<emphasis>";
                                skipTagContent = false;
                                break;
                            case "sup":
                                formattedText = formattedText + "<sup>";
                                skipTagContent = false;
                                break;
                            case "strong":
                                formattedText = formattedText + "<strong>";
                                skipTagContent = false;
                                break;
                            case "section":
                                formattedText = formattedText + "<div>";
                                skipTagContent = false;
                                break;
                            default:
                                formattedText = formattedText + "";
                                skipTagContent = true;
                                break;
                        }
                        break;
                    case XmlNodeType.Text:
                        // Delete text from mitted tags: genre, author etc.
                        if (skipTagContent == false)
                        {
                            formattedText = formattedText + xmlReader.Value;     // Print value inside tag
                        }
                        else
                        {
                            formattedText = formattedText + "";     // Print empty value
                        }
                        
                        break;
                    case XmlNodeType.EndElement:
                        switch (xmlReader.Name)
                        {
                            case "section":
                                formattedText = formattedText + ("</div>");
                                formattedText = formattedText + "</br>";
                                break;
                            // Omitted tags: genre, author etc.
                            case "genre":
                                formattedText = formattedText + "";
                                break;
                            case "first-name":
                                formattedText = formattedText + "";
                                break;
                            case "last-name":
                                formattedText = formattedText + "";
                                break;
                            case "book-title":
                                formattedText = formattedText + "";
                                break;
                            case "author":
                                formattedText = formattedText + "";
                                break;
                            case "lang":
                                formattedText = formattedText + "";
                                break;
                            case "keywords":
                                formattedText = formattedText + "";
                                break;
                            case "date":
                                formattedText = formattedText + "";
                                break;
                            case "id":
                                formattedText = formattedText + "";
                                break;
                            case "version":
                                formattedText = formattedText + "";
                                break;
                            case "document-info":
                                formattedText = formattedText + "";
                                break;
                            case "publisher":
                                formattedText = formattedText + "";
                                break;
                            case "year":
                                formattedText = formattedText + "";
                                break;
                            case "isbn":
                                formattedText = formattedText + "";
                                break;
                            case "publish-info":
                                formattedText = formattedText + "";
                                break;
                            // Any other tag
                            default:
                                formattedText = formattedText + "</" + xmlReader.Name + ">"; // Print end of tag HTML
                                break;
                        }
                        break;
                }
            }
            // Add last lines of HTML template
            formattedText = formattedText + "</body>\r\n</html>";
            // After finishing, write to file
            string curDir = Directory.GetCurrentDirectory();
            File.WriteAllText(String.Format("{0}/html_templates/book.html", curDir), formattedText);

            // Load the file to the web browser
            webBrowser1.Url = new Uri(String.Format("file:///{0}/html_templates/book.html", curDir));
            this.Text = "Kyokumi Reader - " + selectedFile;
        }

        
    }
}
