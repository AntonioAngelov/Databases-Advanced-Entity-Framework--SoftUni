using System.Data.Entity.Infrastructure;
using Microsoft.SqlServer.Server;
using PhotographersDB.Models;

namespace PhotographersDB.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhotographersDB.PhotographersDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PhotographersDB.PhotographersDBContext context)
        {
            string[] names =
            {
                "Gosho", "Pesho", "Dani", "Mariika", "Todora" 
            };

            string[] passwords =
            {
                "12345", "azsumgoten", "otsofiatadeninosht", "nzkoisum", "tupoime"
            };

            string[] regDates =
           {
                "2009-02-15 00:00:00.000", "2015-08-13 00:00:00.000",
                "2012-04-18 00:00:00.000", "2015-07-08 00:00:00.000",
                "2015-05-20 00:00:00.000"
            };

            string[] birthDates =
          {
                "1996-02-15 00:00:00.000", "1990-08-13 00:00:00.000",
                "1870-04-18 00:00:00.000", "1999-07-08 00:00:00.000",
                "2000-05-20 00:00:00.000"
            };

            string[] albumNames =
            {
                "best of 2014", "best summer","hanging with friends",
                "good days", "wazzaaaa", "Pancharevo 2016",
                "more 2013"
            };


            for (int i = 0; i < 5; i++)
            {
                DateTime regDate;
                DateTime.TryParse(regDates[i], out regDate);
                DateTime birthDate;
                DateTime.TryParse(birthDates[i], out birthDate);
                context.Photographers.AddOrUpdate(p => p.Username, new Photographer()
                {
                    Username = names[i],
                    Password = passwords[i],
                    RegisterDate = regDate,
                    BirthDate = birthDate

                });
            }
            context.SaveChanges();

            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    context.Albums.AddOrUpdate(a => a.Name, new Album()
                    {
                        Name = albumNames[i + 5],
                        IsPublic = true
                    });

                    context.Albums.AddOrUpdate(a => a.Name, new Album()
                    {
                        Name = albumNames[i + 6],
                        IsPublic = true
                    });
                }

                context.Albums.AddOrUpdate(a => a.Name, new Album()
                {
                    Name = albumNames[i],
                    IsPublic = false
                });

            }

            context.SaveChanges();

            for (int i = 1; i <= 5; i++)
            {
                context.Albums.Find(i).Photographers.Add(context.Photographers.Find(i));

                if(i % 2 == 0)
                    context.Albums.Find(i).Photographers.Add(context.Photographers.Find(i + 1));
            }


            for (int i = 65; i <= 90; i++)
            {
                context.Pictures.AddOrUpdate(p => p.Title, new Picture()
                {
                    Title = Convert.ToChar(i).ToString(),
                    PathToPicture = Convert.ToChar(i).ToString()
                });
            }

            context.SaveChanges();

            for (int i = 65; i <= 90; i++)
            {
                context.Albums.Find((i % 7) + 1).Pictures.Add(context.Pictures.Find(i - 64));
            }


            string[] tagNames =
            {
                "#NewYear2016", "HolidaySummer",
                "#Summer", "#Friend", "#goodmoments",
                "#coolday", "#greatweather"
            };

            for (int i = 0; i < 7; i++)
            {
                context.Tags.AddOrUpdate(t => t.Name, new Tag()
                {
                    Name = tagNames[i]
                });
            }
            context.SaveChanges();

            for (int i = 1; i <= 7; i++)
            {
                context.Albums.Find(i).Tags.Add(context.Tags.Find(i));
                if(i != 7)
                    context.Albums.Find(i).Tags.Add(context.Tags.Find(i + 1));
            }

        }
    }
}
