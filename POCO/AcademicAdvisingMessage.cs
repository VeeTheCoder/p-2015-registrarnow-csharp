namespace POCO
{
    using System.Collections.Generic;

    public class AcademicAdvisingMessage
    {
        public int AcademicAdvisingId { get; set; }

        public string StudentId { get; set; }

        public string StudentName { get; set; }

        public bool SendToInstructor { get; set; }

        public string MessageSubject { get; set; }

        public string MessageBody { get; set; }

        public override string ToString()
        {
            return "Subject: " + this.MessageSubject + "\n\n" + this.MessageBody;
        }
    }
}
