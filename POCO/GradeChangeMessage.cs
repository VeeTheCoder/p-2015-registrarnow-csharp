namespace POCO
{
    using System.Collections.Generic;

    public class GradeChangeMessage
    {
        public int GradeChangeId { get; set; }

        public string StudentName { get; set; }

        public string StudentId { get; set; }

        public string CourseName { get; set; }

        public string InstructorName { get; set; }

        public string MessageBody { get; set; }

        public string Status { get; set; }

        public override string ToString()
        {
            return this.MessageBody;
        }
    }
}
