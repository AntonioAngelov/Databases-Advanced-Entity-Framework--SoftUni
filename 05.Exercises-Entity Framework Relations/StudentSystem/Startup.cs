namespace StudentSystem
{
    using System.Data.Entity;
    using System;
    using System.Globalization;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;

    class Startup
    {
        static void Main()
        {
            var context = new StudentSystemContext();
            
            //exercise 3.1
            //allStudentsWithTheirHws(context);

            //exercise 3.2
            //allCoursesWithTheirResources(context);

            //exercise 3.3
            //coursesWithMoreThat5resources(context);

            //exercise 3.4
            //coursesActiveOnGivenDate(context);

            //exercise 3.5
            //studentsWithMoneySpend(context);


        }

        private static void studentsWithMoneySpend(StudentSystemContext context)
        {
            var students = context.Students
                .Select(s => new
                {
                    Name = s.Name,
                    NumberOfCourses = s.Courses.Count,
                    TotalPrice = s.Courses.Count != 0 ? s.Courses.Select(c => c.Price).Sum() : 0,
                    AveagePrice = s.Courses.Count != 0 ? s.Courses.Select(c => c.Price).Sum() / s.Courses.Count : 0
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.NumberOfCourses)
                .ThenBy(s => s.Name)
                .ToArray();

            foreach (var student in students)
            {
                Console.WriteLine($"Student name: {student.Name}");
                Console.WriteLine($"Number of courses: {student.NumberOfCourses}");
                Console.WriteLine($"Total price for courses: {student.TotalPrice:F2}");
                Console.WriteLine($"Average price per course: {student.AveagePrice:F2}");
                Console.WriteLine("=================================");
            }
        }

        private static void coursesActiveOnGivenDate(StudentSystemContext context)
        {
            Console.Write("Enter Date: ");
            var input = Console.ReadLine();
            DateTime date;
            DateTime.TryParse(input, out date);

            var courses = context.Courses
                .Where(c => DbFunctions.TruncateTime(c.StartDate) <= date &&
                            DbFunctions.TruncateTime(c.EndDate) >= date)
                .Select(c => new
                {
                    Name = c.Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Duration = DbFunctions.DiffHours(c.StartDate, c.EndDate),
                    StudentsEnrolled = c.Students.Count
                })
                .OrderByDescending(c => c.StudentsEnrolled)
                .ThenByDescending(c => c.Duration)
                .ToArray();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course name: {course.Name}");
                Console.WriteLine($"Course start date: {course.StartDate}");
                Console.WriteLine($"Course end date: {course.EndDate}");
                Console.WriteLine($"Course duration in days: {course.Duration}");
                Console.WriteLine($"Students nrolled for this course: {course.StudentsEnrolled}");
                Console.WriteLine("=================================");
            }
        }

        private static void coursesWithMoreThat5resources(StudentSystemContext context)
        {
            var courses = context.Courses
                .Where(c => c.Resources.Count > 5)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new
                {
                    Name = c.Name,
                    ResourceCount = c.Resources.Count
                })
                .ToArray();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course name: {course.Name}");
                Console.WriteLine($"Numbers of resources: {course.ResourceCount}");
                Console.WriteLine("=================================");
            }

            if(courses.Length == 0)
                Console.WriteLine("There are no courses with more than 5 resources!");
        }

        public static void allCoursesWithTheirResources(StudentSystemContext context)
        {
            foreach (var course in context.Courses.OrderBy(c => c.StartDate).ThenByDescending(c => c.EndDate))
            {
                Console.WriteLine($"Course: {course.Name}");
                Console.WriteLine($"Description: {course.Description}");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Resources: ");
                Console.WriteLine(".................................");
                foreach (var resource in course.Resources)
                {
                    Console.WriteLine("Name: " + resource.Name);
                    Console.WriteLine("Type: " + resource.Type);
                    Console.WriteLine("URL: " + resource.URL);
                    Console.WriteLine();
                }

                if (course.Resources.Count == 0)
                    Console.WriteLine("(No resource!)");
                Console.WriteLine();
                Console.WriteLine("=================================");
            }
        }

        public static void allStudentsWithTheirHws(StudentSystemContext context)
        {
            foreach (var student in context.Students)
            {
                Console.WriteLine($"Homeworks of {student.Name}");
                Console.WriteLine("---------------------------------");

                foreach (var homework in student.Homeworks)
                {
                    Console.WriteLine("Content-type: " + homework.Type);
                    Console.WriteLine("Content: " + homework.Content);
                    Console.WriteLine();
                }

                if(student.Homeworks.Count == 0)
                    Console.WriteLine("(No homeworks!)");
                Console.WriteLine();
                Console.WriteLine("=================================");
            }
        }
    }
}
