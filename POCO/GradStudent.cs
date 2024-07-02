namespace POCO
{
    using System.Collections.Generic;

    public class GradStudent : Student
    {
        public string Sid { get; set; }

        public float Under_gpa { get; set; }

        public int Gre { get; set; }  

        public override string ToString()
        {
            return this.Sid + "-" + this.Gre + "-" + this.Under_gpa;
        }
    }
}
