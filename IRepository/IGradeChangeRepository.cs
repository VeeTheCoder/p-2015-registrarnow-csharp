namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IGradeChangeRepository
    {
        void InsertGradeChangeMessage(GradeChangeMessage message, ref List<string> errors);

        void UpdateGradeChangeMessage(GradeChangeMessage message, ref List<string> errors);

        void DeleteGradeChangeMessage(int id, ref List<string> errors);

        GradeChangeMessage GetGradeChangeMessageDetail(int id, List<string> errors);

        List<GradeChangeMessage> GetGradeChangeMessages(string studentId, bool isInstructor, ref List<string> errors);
    }
}
