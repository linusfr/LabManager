using SE2.LabManager.PdfExport.Contracts.DTOs;
using System.Collections.Generic;

namespace SE2.LabManager.PdfExport.Contracts {
    public interface ILabExporter {
        string CreatePresenceListForStuds(string courseName, string lecturerFullName, string labNumber, List<PdfPresence> presences);
        string CreateTaskListForStuds(string courseName, string lecturerFullName, string labNumber, List<PdfTask> tasks);
        string CreateInformationListForStuds(string courseName, string lecturerFullName, string labNumber, List<PdfStudent> students);
    }
}
