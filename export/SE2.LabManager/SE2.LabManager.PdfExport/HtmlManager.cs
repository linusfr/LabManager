using System.Collections.Generic;
using SE2.LabManager.PdfExport.Contracts.DTOs;
namespace SE2.LabManager.PdfExport {

    internal class HtmlManager {
        /// <summary>
        /// creates a html string for a specific lab showing
        /// information about the presence of each student at each labdate
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="lecturerFullName"></param>
        /// <param name="labNumber"></param>
        /// <param name="presences"></param>
        /// <returns>html string</returns>
        public string CreatePresenceHtmlString(string courseName, string lecturerFullName, string labNumber, List<PdfPresence> presences) {
            // create html for file header
            var htmlString = CreateHeader(courseName, lecturerFullName, labNumber);

            // define neccesary headers
            var headers = new List<string> { "Vorname", "Nachname", "Matrikelnummer", "Datum", "Anwesenheit", "Anmerkung" };

            // create header columns for the defined headers
            htmlString += CreateTableHeader(headers, "Laboranwesenheiten");

            presences.ForEach(p => {

                htmlString += "<tr>";
                htmlString += CreateTd(p.FirstName);
                htmlString += CreateTd(p.LastName);
                htmlString += CreateTd(p.MatricelNumber.ToString());
                htmlString += CreateTd(p.DateString);
                htmlString += CreateTd(p.WasPresentString);
                htmlString += CreateTd(p.Note);
                htmlString += "</tr>";

            });

            htmlString +=
                            "</table>" +
                        "</div>" +
                    "</body>" +
                "</html>";

            // return the created html string
            return htmlString;
        }

        /// <summary>
        /// create a html string for a specific lab 
        /// showing information about each task
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="lecturerFullName"></param>
        /// <param name="labNumber"></param>
        /// <param name="tasks"></param>
        /// <returns>html string</returns>
        public string CreateTaskHtmlString(string courseName, string lecturerFullName, string labNumber, List<PdfTask> tasks) {
            // create html for file header
            var htmlString = CreateHeader(courseName, lecturerFullName, labNumber);

            // define neccesary headers
            var headers = new List<string> { "Vorname", "Nachname", "Matrikelnummer", "Aufgabe", "Abgabedatum", "Erledigt" };

            // create header columns for the defined headers
            htmlString += CreateTableHeader(headers, "Laboranwesenheiten");

            tasks.ForEach(p => {

                htmlString += "<tr>";
                htmlString += CreateTd(p.FirstName);
                htmlString += CreateTd(p.LastName);
                htmlString += CreateTd(p.MatricelNumber.ToString());
                htmlString += CreateTd(p.TaskNumber.ToString());
                htmlString += CreateTd(p.DueDateString);
                htmlString += CreateTd(p.IsDoneString);
                htmlString += "</tr>";

            });

            htmlString +=
                            "</table>" +
                        "</div>" +
                    "</body>" +
                "</html>";

            // return the created html string
            return htmlString;
        }

        /// <summary>
        /// create html string showing detailed information about each student
        /// in a specific lab
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="lecturerFullName"></param>
        /// <param name="labNumber"></param>
        /// <param name="students"></param>
        /// <returns>html string</returns>
        public string CreateInfoHtmlString(string courseName, string lecturerFullName, string labNumber, List<PdfStudent> students) {
            // create html for file header
            var htmlString = CreateHeader(courseName, lecturerFullName, labNumber);

            // define neccesary headers
            var headers = new List<string> { "Anrede", "Vorname", "Nachname", "Matrikelnummer", "Studiengang", "Semester", "Email" };

            // create header columns for the defined headers
            htmlString += CreateTableHeader(headers, "Studenten-Informationen");

            students.ForEach(p => {

                htmlString += "<tr>";
                htmlString += CreateTd(p.Salutation);
                htmlString += CreateTd(p.FirstName);
                htmlString += CreateTd(p.LastName);
                htmlString += CreateTd(p.MatricelNumber.ToString());
                htmlString += CreateTd(p.StudyCourse);
                htmlString += CreateTd(p.Semester.ToString());
                htmlString += CreateTd(p.Email);
                htmlString += "</tr>";

            });

            htmlString +=
                            "</table>" +
                        "</div>" +
                    "</body>" +
                "</html>";

            // return the created html string
            return htmlString;
        }

        #region helper attributes and methods
        // defines the style used for td and th html elements
        private readonly string tdThStyle = "border: 1px solid #ddd; text-align: left; padding: 0.5rem;";

        /// <summary>
        /// creates a div with the given style and content
        /// </summary>
        /// <param name="style"></param>
        /// <param name="content"></param>
        /// <returns>the created div</returns>
        private string CreateDiv(string style, string content) {
            return $"<div style=\"{style}\">{content}</div>";
        }

        /// <summary>
        /// create a th table element for the given content using tdThStyle for the Style
        /// </summary>
        /// <param name="content"></param>
        /// <returns>created th element</returns>
        private string CreateTh(string content) {
            return $"<th style=\"{tdThStyle}\">{content}</th>";
        }

        /// <summary>
        /// create a td table element for the given content using tdThStyle for the Style
        /// </summary>
        /// <param name="content"></param>
        /// <returns>created td element</returns>
        private string CreateTd(string content) {
            return $"<td style=\"{tdThStyle}\">{content}</td>";
        }

        /// <summary>
        /// creates header for the pdf showing general information 
        /// about the course, the lecturer and the lab
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="lecturerFullName"></param>
        /// <param name="labNumber"></param>
        /// <returns>created html string</returns>
        private string CreateHeader(string courseName, string lecturerFullName, string labNumber) {
            var htmlString =
                "<html>" +
                    "<div style=\"font-family:'CourierNew',Courier,monospace\">" +
                        "<div style=\"text-align:center; margin: 0 auto;\">" +
                                CreateDiv("font-size:40px; text-align:center; margin-bottom: 10px; font-weight:bold;", courseName) +
                                CreateDiv("", "Lehrender: " + lecturerFullName) +
                                CreateDiv("", "Labornummer: " + labNumber) +
                        "</div>";

            return htmlString;
        }

        /// <summary>
        /// create a table header with a column for each element in headers
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="pdfType"></param>
        /// <returns>created html string</returns>
        private string CreateTableHeader(List<string> headers, string pdfType) {
            var tableHeader =
                $"<div style=\"margin-top:50px; margin-bottom:10px; font-size: 20px; text-align:center; font-weight:bold;\">{pdfType}</div>" +
                    "<table style=\"border-collapse: collapse; margin: 0 auto; border-spacing: 10px; border: 1px solid #ddd; text-align: left; \">" +
                        "<tr style=\"font-size: 20px;\" style=\"padding: 15px;\">";

            headers.ForEach((string header) => tableHeader += CreateTh(header));

            tableHeader += "</tr>";

            return tableHeader;
        }
        #endregion

    }
}
