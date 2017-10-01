namespace OOP_Intro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Startup
    {
        static void Main(string[] args)
        {
            //exercise 1
            //createPeopleAndDisplay();
            
            //exercise 2
            //parseInputAndCreatePerson();

            //exercise 3
            //createFamilyAndGetOldest();

            //exercise 4
            //creatStudentsAndGetCount();

            //exercise 5
            //getReducedPlanckconstant();

            //exercise 6
            //readAndSolve();

        }

        private static void readAndSolve()
        {
            while (true)
            {
                var input = Console.ReadLine().Split(' ').ToArray();

                if (input[0] == "End") break;

                switch (input[0])
                {
                    case "Sum":
                        Console.WriteLine($"{MathUtil.Sum(double.Parse(input[1]), double.Parse(input[2])):F2}");
                        break;
                    case "Subtract":
                        Console.WriteLine($"{MathUtil.Subtract(double.Parse(input[1]), double.Parse(input[2])):F2}");
                        break;
                    case "Multiply":
                        Console.WriteLine($"{MathUtil.Multiply(double.Parse(input[1]), double.Parse(input[2])):F2}");
                        break;
                    case "Divide":
                        Console.WriteLine($"{MathUtil.Divide(double.Parse(input[1]), double.Parse(input[2])):F2}");
                        break;
                    case "Percentage":
                        Console.WriteLine($"{MathUtil.Percentage(double.Parse(input[1]), double.Parse(input[2])):F2}");
                        break;
                }
            }
        }

        private static void getReducedPlanckconstant()
        {
            Console.WriteLine(Calculation.reducedPlanckConstant());
        }

        private static void creatStudentsAndGetCount()
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (input == "End")
                {
                    Console.WriteLine(Student.Count);
                }
                else
                {
                    Student current = new Student(input);
                }
            }
        }

        private static void createFamilyAndGetOldest()
        {
            Family myFamily = new Family();

            int N = int.Parse(Console.ReadLine());

            for (int i = 0; i < N; i++)
            {
                var input = Console.ReadLine().Split(' ').ToArray();

                Person current = new Person(input[0], int.Parse(input[1]));

                myFamily.AddMember(current);
            }

            Console.WriteLine(myFamily.GetOldestMember());
        }

        private static void parseInputAndCreatePerson()
        {
            var input = Console.ReadLine().Split(',').ToArray();

            Person person;
            if (input[0] == string.Empty)
            {
                person = new Person();
            }
            else if (input.Length == 1)
            {
                if (input[0].All(char.IsDigit))
                {
                    person = new Person(int.Parse(input[0]));
                }
                else
                {
                    person = new Person(input[0]);
                }
            }
            else
            {
                person = new Person(input[0], int.Parse(input[1]));
            }

            Console.WriteLine(person);
        }

        private static void createPeopleAndDisplay()
        {
            Person pesho = new Person();
            pesho.Name = "Pesho";
            pesho.Age = 20;

            Person gosho = new Person();
            gosho.Name = "Gosho";
            gosho.Age = 18;

            Person stamat = new Person();
            stamat.Name = "Stamat";
            stamat.Age = 43;

            Console.WriteLine($"{pesho.Name} is {pesho.Age} years old.");
            Console.WriteLine($"{gosho.Name} is {gosho.Age} years old.");
            Console.WriteLine($"{stamat.Name} is {stamat.Age} years old.");
        }
    }
}
