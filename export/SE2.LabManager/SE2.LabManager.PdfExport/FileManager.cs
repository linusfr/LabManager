using SE2.LabManager.PdfExport.Contracts.DTOs;
using System;
using System.IO;

namespace SE2.LabManager.PdfExport {
    internal class FileManager {

        /// <summary>
        /// creates a file at the given path and fills it with the given content
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        private void CreateFile(string filePath, string content) {
            File.WriteAllText(filePath, content);
        }

        /// <summary>
        /// takes a file location and removes it, if it exists
        /// </summary>
        /// <param name="filePath"></param>
        private void RemoveFile(string filePath) {
            try {
                // check if file exists with its full path    
                if (File.Exists(filePath)) {
                    // if file found, delete it    
                    File.Delete(filePath);
                }
            } catch (IOException ioExp) {
                Console.WriteLine(ioExp.Message);
            }
        }

        /// <summary>
        /// creates a pdf for a given html string
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="labNumber"></param>
        /// <param name="htmlString"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string CreatePDF(string courseName, string labNumber, string htmlString, string type) {

            // get the users home directory
            var userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            #region createDirectories
            // Specify the directory you want to manipulate.
            string docPath = $"{userDirectory}\\Documents";
            string labPath = $"{docPath}\\Labormanager";
            string coursePath = $"{labPath}\\{courseName}".Replace(" ", "");

            try {
                // Determine whether the directory exists.
                if (!Directory.Exists(docPath)) {
                    // Try to create the directory.
                    Directory.CreateDirectory(docPath);
                }
            } catch (Exception e) {
                return $"ERROR, {e.Message}";
            }
            try {
                // Determine whether the directory exists.
                if (!Directory.Exists(labPath)) {
                    // Try to create the directory.
                    Directory.CreateDirectory(labPath);
                }
            } catch (Exception e) {
                return $"ERROR, {e.Message}";
            }

            try {
                // Determine whether the directory exists.
                if (!Directory.Exists(coursePath)) {
                    // Try to create the directory.
                    Directory.CreateDirectory(coursePath);
                }
            } catch (Exception e) {
                return $"ERROR, {e.Message}";
            }
            #endregion

            // define paths for the files
            var htmlPath = ($"{coursePath}\\{courseName}_Labor_{labNumber}_{type}.html").Replace(" ", "");
            var pdfPath = ($"{coursePath}\\{courseName}_Labor_{labNumber}_{type}.pdf").Replace(" ", "");

            // create html file from the html string
            CreateFile(htmlPath, htmlString);

            // create pdf from html file
            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();

            try {
                htmlToPdf.GeneratePdfFromFile(htmlPath, null, pdfPath);
            } catch (Exception e) {
                return $"ERROR, {e.Message}";
            }

            // remove html file which was used for pdf creation
            RemoveFile(htmlPath);

            // return the path to the created pdf
            return pdfPath;
        }
    }
}
