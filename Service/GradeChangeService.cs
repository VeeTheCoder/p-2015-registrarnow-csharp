namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class GradeChangeService
    {
        private readonly IGradeChangeRepository repository;

        public GradeChangeService(IGradeChangeRepository repository)
        {
            this.repository = repository;
        }

        public void InsertGradeChangeMessage(GradeChangeMessage message, ref List<string> errors)
        {
            if (message == null)
            {
                errors.Add("Grade change message cannot be null");
                throw new ArgumentException();
            }

            if (message.StudentId == null)
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (message.MessageBody == null)
            {
                errors.Add("Invalid message body");
                throw new ArgumentException();
            }

            this.repository.InsertGradeChangeMessage(message, ref errors);
        }

        public void UpdateGradeChangeMessage(GradeChangeMessage message, ref List<string> errors)
        {
            if (message == null)
            {
                errors.Add("Grade change message cannot be null");
                throw new ArgumentException();
            }

            if (message.StudentId == null)
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (message.MessageBody == null)
            {
                errors.Add("Invalid message body");
                throw new ArgumentException();
            }

            this.repository.UpdateGradeChangeMessage(message, ref errors);
        }

        public void DeleteGradeChangeMessage(int id, ref List<string> errors)
        {
            this.repository.DeleteGradeChangeMessage(id, ref errors);
        }

        public GradeChangeMessage GetGradeChangeMessageDetail(int id, ref List<string> errors)
        {
            return this.repository.GetGradeChangeMessageDetail(id, errors);
        }

        public List<GradeChangeMessage> GetGradeChangeMessages(string studentId, bool isInstructor, ref List<string> errors)
        {
            return this.repository.GetGradeChangeMessages(studentId, isInstructor, ref errors);
        }
    }
}
