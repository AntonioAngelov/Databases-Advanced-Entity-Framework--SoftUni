namespace OOP_Intro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    class Student
    {
        private string name;

        private static int count = 0;

        public Student(string newName)
        {
            Student.count += 1;
            this.Name = newName;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public static int Count
        {
            get { return Student.count; }
        }
    }
}
