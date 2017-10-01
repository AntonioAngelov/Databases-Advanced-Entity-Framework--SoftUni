namespace BookShopSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Data;
    using System.Globalization;
    using System.IO;
    using BookShopSystem.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BookShopSystem.Data.BookShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BookShopSystem.Data.BookShopContext";
        }

        protected override void Seed(BookShopSystem.Data.BookShopContext context)
        {

            SeedAuthors(context);
            SeedBooks(context);
            SeedCategories(context);

            context.SaveChanges();

        }

        public void SeedCategories(BookShopContext context)
        {
            int booksCount = context.Books.Local.Count;
            string[] categories = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "../../Import/categories.csv");

            for (int i = 1; i < categories.Length; i++)
            {
                string[] data = categories[i]
                    .Split(',')
                    .Select(c => c.Replace("\"", string.Empty))
                    .ToArray();

                string categoryName = data[0];

                Category category = new Category()
                {
                    Name = categoryName
                };
                var books = context.Books.Local;
                int bookIndex = (i * 30) % booksCount;

                for (int j = 0; j < bookIndex; j++)
                {
                    Book book = context.Books.Local[j];
                    category.Books.Add(book);
                }

                context.Categories.AddOrUpdate(c => c.Name, category);

                

            }
        }

        private void SeedBooks(BookShopContext context)
        {
            int authoCount = context.Authors.Local.Count;
            string[] books = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "../../Import/books.csv");

            for (int i = 1; i < books.Length; i++)
            {               
                string[] data = books[i].Split(',').Select(arg => arg.Replace("\"", string.Empty)).ToArray();

                int authorIndex = i % authoCount;
                Author author = context.Authors.Local[authorIndex];
                EditionType edition = (EditionType) int.Parse(data[0]);
                DateTime resleaseDate = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InvariantCulture);
                int copies = int.Parse(data[2]);
                decimal price = decimal.Parse(data[3]);
                AgeRestriction ageRestriction = (AgeRestriction) int.Parse(data[4]);
                string title = data[5];

                context.Books.AddOrUpdate(b => new {b.Title, b.AuthorId}, new Book()
                {
                    Author = author,
                    AuthorId = author.Id,
                    EditionType = edition,
                    ReleaseDate = resleaseDate,
                    Copies = copies,
                    Price = price,
                    AgeRestriction = ageRestriction,
                    Title = title
                });

            }

            

        }

        private void SeedAuthors(BookShopContext context)
        {
            string[] authors = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "../../Import/authors.csv");

            for (int i = 1; i < authors.Length; i++)
            {
                string[] data = authors[i].Split(',');
                string firstName = data[0].Replace("\"", string.Empty);
                string lastName = data[1].Replace("\"", string.Empty);

                context.Authors.AddOrUpdate(a => new {a.FirstName, a.LastName}, new Author()
                {
                    FirstName = firstName,
                    LastName = lastName
                });
            }
         
        }
    }
}
