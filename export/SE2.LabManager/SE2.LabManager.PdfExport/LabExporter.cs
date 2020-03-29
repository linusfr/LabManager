
using SE2.LabManager.PdfExport.Contracts;
using SE2.LabManager.PdfExport.Contracts.DTOs;
using System;
using System.Collections.Generic;

namespace SE2.LabManager.PdfExport {
    public class LabExporter : ILabExporter {

        #region properties and Constructor
        private readonly HtmlManager htmlCreator;
        private readonly FileManager fileManager;

        public LabExporter() {
            htmlCreator = new HtmlManager();
            fileManager = new FileManager();
        }
        #endregion

        /// <summary>
        /// create PDF for the presence of each student in each labdate for a specific lab
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="lecturerFullName"></param>
        /// <param name="labNumber"></param>
        /// <param name="presences"></param>
        /// <returns>pdf file location</returns>
        public string CreatePresenceListForStuds(string courseName, string lecturerFullName, string labNumber, List<PdfPresence> presences) {

            // create html string from the data
            var htmlString = htmlCreator.CreatePresenceHtmlString(courseName, lecturerFullName, labNumber, presences);

            // create pdf and return the path of it
            return fileManager.CreatePDF(courseName, labNumber, htmlString, "Anwesenheit");

        }

        /// <summary>
        /// create PDF for the tasks of each student in each labdate for a specific lab
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="lecturerFullName"></param>
        /// <param name="labNumber"></param>
        /// <param name="tasks"></param>
        /// <returns>pdf file location</returns>
        public string CreateTaskListForStuds(string courseName, string lecturerFullName, string labNumber, List<PdfTask> tasks) {

            // create html string from the data
            var htmlString = htmlCreator.CreateTaskHtmlString(courseName, lecturerFullName, labNumber, tasks);

            // create pdf and return the path of it
            return fileManager.CreatePDF(courseName, labNumber, htmlString, "Abgaben");

        }

        /// <summary>
        /// create PDF with detailed information about each studente attending a specific lab
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="lecturerFullName"></param>
        /// <param name="labNumber"></param>
        /// <param name="students"></param>
        /// <returns>pdf file location</returns>
        public string CreateInformationListForStuds(string courseName, string lecturerFullName, string labNumber, List<PdfStudent> students) {

            // create html string from the data
            var htmlString = htmlCreator.CreateInfoHtmlString(courseName, lecturerFullName, labNumber, students);

            // create pdf and return the path of it
            return fileManager.CreatePDF(courseName, labNumber, htmlString, "StudentenInformationen");

        }
    }
}
