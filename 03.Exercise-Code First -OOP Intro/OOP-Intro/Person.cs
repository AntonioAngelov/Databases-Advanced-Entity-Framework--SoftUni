namespace OOP_Intro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Person
    {
        private string name;

        private int age;

        public Person() : this("No name", 1) {}

        public Person(int newAge): this("No name", newAge)
        {
            this.age = newAge;
        }

        public Person(string newName): this(newName, 1)
        {
            this.name = newName;
        }

        public Person(string newName, int newAge)
        {
            this.name = newName;
            this.age = newAge;
        }

        public string Name
        {
            get
            {               
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid Name!");
                }
                    this.name = value;
            }
        }

        public int Age
        {
            get
            {
                return this.age;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Invalid Age!");
                }
                this.age = value;
            }
        }

        public override string ToString()
        {
        return $"{this.Name} {this.Age}";
        }
    }
}
