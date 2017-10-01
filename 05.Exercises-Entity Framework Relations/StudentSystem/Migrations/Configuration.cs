namespace StudentSystem.Migrations
{
    using System;
    using Models;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystem.StudentSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(StudentSystem.StudentSystemContext context)
        {
            var rnd = new Random();
            string[] studentNames =
            {
                "Pesho", "Gosho", "David", "Goliath",
                "Martina", "Martin", "Vesko"
            };

            string[] studentPhones =
            {
                "0841234523", "0877963412", "2096883123",
                "0031239412", "3214324552", "2321343124", null
            };


            string[] coursesNames =
            {
                "Programirane", "Surbane s vilica", "Topologiq",
                "Matematika", "Kak da znaem kakvo ne znaem",
                "Mikrotehnika", "AEI"
            };

            string[] coursesDescriptions =
            {
                "Elate da vi vzemem parata", "Vsichki shte ostanem dovolni",
                "Nqma takova neshto", "mn qk", "sredna rabota", "idvaite", "mn lesen"
            };

            string[] resourcesNames =
            {
                "Chushki", "domati", "kasetki", "bira", "salata",
                "Programming Basics", "DBFundamentas", "Web Basics", "HTML", "Fun"
            };

            string[] resourcesUrls =
            {
                "http://zdrasti.com", "www.HelloWorld.random", "neznam.bg",
                "http://abv.bg", "http://microsoft.com", "gmail.com",
                "http://stackoverflow.com", "https://www.youtube.com",
                "https://softuni.bg", "vbox7.bg"
            };

            string[] hwcontents = {"za 6", "mn tegavo no uspqh", "super e", ".", "a + b = c"};

            string[] dates =
            {
                "2009-02-15 00:00:00.000", "2015-08-13 00:00:00.000",
                "2012-04-18 00:00:00.000", "2015-07-08 00:00:00.000",
                "2015-05-20 00:00:00.000", "2010-07-06 00:00:00.000",
                "2010-10-23 00:00:00.000"
            };
 
            for (int i = 0; i < 7; ++i)
            {
                context.Students.AddOrUpdate(s => s.Name, new Student()
                {
                    Name = studentNames[i],
                    PhoneNumber = studentPhones[i],
                    BirthDay = new DateTime(rnd.Next(1980, 1990), rnd.Next(1, 12), rnd.Next(1, 28)),
                    RegistrationDate = new DateTime(rnd.Next(2002, 2008), rnd.Next(1, 12), rnd.Next(1, 28))
                });

                DateTime courseStartDate;
                DateTime.TryParse(dates[i], out courseStartDate);

                context.Courses.AddOrUpdate(c => c.Name, new Course()
                {
                    Name = coursesNames[i],
                    Description = coursesDescriptions[i],
                    StartDate = courseStartDate,
                    EndDate = courseStartDate.AddDays(i * 100),
                    Price = rnd.Next(1000, 3000) / 5.4m
                });
            }

            context.SaveChanges();

            for (int i = 0; i < 5; i++)
            {
                context.Resources.AddOrUpdate(r => r.Name, new Resource()
                {
                    Name = resourcesNames[i],
                    URL = resourcesUrls[i],
                    Type = (ResourceType)Enum.GetValues(typeof(ResourceType))
                        .GetValue(rnd.Next(0, Enum.GetValues(typeof(ResourceType)).Length)),
                    CourseId = i + 1
                });

                context.Homeworks.AddOrUpdate(hw=> hw.Content ,new Homework()
                {
                    Content = hwcontents[i],
                    Type = (HomeworkContentType)Enum.GetValues(typeof(HomeworkContentType))
                        .GetValue(rnd.Next(0, Enum.GetValues(typeof(HomeworkContentType)).Length)),
                    SubmissionDate = new DateTime(rnd.Next(2010, 2016), rnd.Next(1, 12), rnd.Next(1, 28)),
                    StudentId = i + 1,
                    CourseId = i + 1
                });
  
            }

            for (int i = 5; i <= 9; i++)
            {
                context.Resources.AddOrUpdate(r => r.Name, new Resource()
                {
                    Name = resourcesNames[i],
                    URL = resourcesUrls[i],
                    Type = (ResourceType)Enum.GetValues(typeof(ResourceType))
                      .GetValue(rnd.Next(0, Enum.GetValues(typeof(ResourceType)).Length)),
                    CourseId = 1
                });
            }


            context.SaveChanges();

            for (int i = 1; i <= 5; i++)
            {
                context.Students.Find(i)
                    .Courses
                    .Add(context.Courses.Find(i));
            }

            context.SaveChanges();
        }
    }
}
