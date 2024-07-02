namespace POCO
{
    using System.Collections.Generic;

    public class ForeignStudent : Student
    {
        public string Sid { get; set; }

        public float Under_gpa { get; set; }

        public float Toefl { get; set; }

        public override string ToString()
        {
            return this.Sid + "-" + this.Under_gpa + "-" + this.Toefl;  
        }
    }
}
