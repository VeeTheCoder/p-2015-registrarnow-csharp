namespace POCO
{
    public class CourseGrade
    {
        public char Grade { get; set; }

        public int ScheduleId { get; set; }

        public override string ToString()
        {
            return this.ScheduleId + "-" + this.Grade;
        }
    }
}
