namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class AcademicAdvisingService
    {
        private readonly IAcademicAdvisingRepository repository;

        public AcademicAdvisingService(IAcademicAdvisingRepository repository)
        {
            this.repository = repository;
        }

        public void InsertAdvisingMessage(AcademicAdvisingMessage message, ref List<string> errors)
        {
            if (message == null)
            {
                errors.Add("Academic advising message cannot be null");
                throw new ArgumentException();
            }

            if (message.StudentId == null)
            {
                errors.Add("Academic advising message student id cannot be null");
                throw new ArgumentException();
            }

            if (message.MessageBody == null)
            {
                errors.Add("Academic advising message body cannot be null");
                throw new ArgumentException();
            }

            if (message.StudentId.Length > 20)
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (message.MessageSubject.Length > 50)
            {
                errors.Add("Invalid message subject");
                throw new ArgumentException();
            }

            if (message.MessageBody.Length > 256)
            {
                errors.Add("Message body too long!");
                throw new ArgumentException();
            }

            this.repository.InsertAdvisingMessage(message, ref errors);
        }

        public void UpdateAdvisingMessage(AcademicAdvisingMessage message, ref List<string> errors)
        {
            if (message == null)
            {
                errors.Add("Academic advising message cannot be null");
                throw new ArgumentException();
            }

            if (message.StudentId == null)
            {
                errors.Add("Academic advising message student id cannot be null");
                throw new ArgumentException();
            }

            if (message.MessageBody == null)
            {
                errors.Add("Academic advising message body cannot be null");
                throw new ArgumentException();
            }

            if (message.StudentId.Length > 20)
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (message.MessageSubject.Length > 50)
            {
                errors.Add("Invalid message subject");
                throw new ArgumentException();
            }

            if (message.MessageBody.Length > 256)
            {
                errors.Add("Message body too long!");
                throw new ArgumentException();
            }

            this.repository.UpdateAdvisingMessage(message, ref errors);
        }

        public void DeleteAdvisingMessage(int id, ref List<string> errors)
        {
            this.repository.DeleteAdvisingMessage(id, ref errors);
        }

        public AcademicAdvisingMessage GetAdvisingMessageDetail(int id, ref List<string> errors)
        {
            return this.repository.GetAdvisingMessageDetail(id, ref errors);
        }

        public List<AcademicAdvisingMessage> GetAdvisingMessages(string studentId, bool isInstructor, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId) && isInstructor == false)
            {
                errors.Add("Invalid academic advising message id");
                throw new ArgumentException();
            }

            return this.repository.GetAdvisingMessages(studentId, isInstructor, ref errors);
        }
    }
}
