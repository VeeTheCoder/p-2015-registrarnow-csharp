namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IAcademicAdvisingRepository
    {
        void InsertAdvisingMessage(AcademicAdvisingMessage message, ref List<string> errors);

        void UpdateAdvisingMessage(AcademicAdvisingMessage message, ref List<string> errors);

        void DeleteAdvisingMessage(int id, ref List<string> errors);

        AcademicAdvisingMessage GetAdvisingMessageDetail(int id, ref List<string> errors);

        List<AcademicAdvisingMessage> GetAdvisingMessages(string studentId, bool sendToInstructor, ref List<string> errors);
    }
}
