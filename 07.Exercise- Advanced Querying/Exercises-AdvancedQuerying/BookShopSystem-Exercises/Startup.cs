using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace BookShopSystem_Exercises
{
    using System;
    using Data;
    using Models;

    class Startup
    {
        static void Main()
        {
            var context = new ShopSystemContext();

            //exercise 1. Books Titles by Age Restriction
            //BooksTitlesByAgeRestriction(context);

            //exercise 1. Golden Books
            //GetGoldEditionBooks(context);

            //exercise 2. Books by Price
            //GetBooksWithPrice(context);

            //exercise 3. Not Released Books
            //GetBooksNOTRealeasedInYear(context);

            //exercise 4. Book Titles by Category
            //GetBooksByCategories(context);

            //exercise 5. Books Released Before Date
            //GetBooksRealeasedBeforeDate(context);

            //exercise 6. Authors Search
            //AuthorsWithNameEnding(context);

            //exercise 7. Books Search
            //BooksByTitle(context);

            //exercise 8. Book Titles Search
            //BooksByAuthorName(context);

            //exercise 9. Count Books
            //BooksWithTitleLongerThan(context);

            //exercise 10. Total Book Copies
            //NumberOfBooksByAuthor(context);

            //exercie 11. Find Profit
            //ProfitPerCategory(context);

            //exercise 12. Most Recent Books
            //MostRecenBookPerCategory(context);

            //exercise 13. Increase Book Copies
            //IncreaseBooksCopies(context);

            //exercise 14. Remove Books
            //RemoveBooksWithFewCopies(context);

            //exercise 15. Stored Procedure
            //GetBooksWIthProcedure(context);

        }

        private static void GetBooksWIthProcedure(ShopSystemContext context)
        {
            var names = Console.ReadLine().Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);

            SqlParameter firstName = new SqlParameter("@firstName", SqlDbType.VarChar);
            SqlParameter lastName = new SqlParameter("@lastName", SqlDbType.VarChar);
            firstName.Value = names[0];
            lastName.Value = names[1];

            var booksCount = context.Database.ExecuteSqlCommand("BooksByAutor @firstName, @lastName", firstName,
                lastName);

            Console.WriteLine($"{names[0]} {names[1]} has written {booksCount} books");
        }

        private static void RemoveBooksWithFewCopies(ShopSystemContext context)
        {
            var booksToRemove = context.Books.Where(b => b.Copies < 4200);
            var count = booksToRemove.Count();

            context.Books
                .RemoveRange(booksToRemove);

            context.SaveChanges();

            Console.WriteLine($"{count} books were deleted");
        }

        private static void IncreaseBooksCopies(ShopSystemContext context)
        {
            var books = context.Books
                .ToList();

            int count = 0;

            foreach (var book in books)
            {
                if (book.ReleaseDate > DateTime.Parse("06/01/2013"))
                {
                    book.Copies += 44;
                    count += 44;
                }
            }

            context.SaveChanges();

            Console.WriteLine(count);
        }

        private static void MostRecenBookPerCategory(ShopSystemContext context)
        {
            var categories = context.Categories
                .Where(c => c.Books.Count > 35)
                .OrderByDescending(c => c.Books.Count)
                .ToList();

            foreach (var category in categories)
            {
                Console.WriteLine($"--{category.Name}: {category.Books.Count} books");

                category.Books
                    .OrderByDescending(b => b.ReleaseDate)
                    .ThenBy(b => b.Title)
                    .Take(3)
                    .ToList()
                    .ForEach(b => Console.WriteLine($"{b.Title} ({b.ReleaseDate.Value.Year})"));


            }

        }

        private static void ProfitPerCategory(ShopSystemContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    TotalProfit = c.Books.ToList().Count != 0 ?  c.Books.Select(b => b.Price * b.Copies).Sum() : 0
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.CategoryName)
                .ToList();

            categories.ForEach(c => Console.WriteLine($"{c.CategoryName} - {c.TotalProfit}"));

        }

        private static void NumberOfBooksByAuthor(ShopSystemContext context)
        {
            context.Authors
                .Select(a => new
                {
                    Name = a.FirstName + " " + a.LastName,
                    booksCount = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.booksCount)
                .ToList()
                .ForEach(a => Console.WriteLine($"{a.Name} - {a.booksCount}"));
        }

        private static void BooksWithTitleLongerThan(ShopSystemContext context)
        {
            int titleLength = int.Parse(Console.ReadLine());

            int numberOfBooks = context.Books
                .Where(b => b.Title.Length > titleLength)
                .Count();

            Console.WriteLine($"There are {numberOfBooks} books with longer title than {titleLength} symbols");

        }

        private static void BooksByAuthorName(ShopSystemContext context)
        {
            string searchStart = Console.ReadLine().ToLower();

            var books = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(searchStart))
                .OrderBy(b => b.Id)
                .Select(b => new
                {
                    Title = b.Title,
                    AuthorName = b.Author.FirstName + " " + b.Author.LastName 
                }).ToList();

            books.ForEach(b => Console.WriteLine($"{b.Title} ({b.AuthorName})"));
        }

        private static void BooksByTitle(ShopSystemContext context)
        {
            var searchString = Console.ReadLine().ToLower();

            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(searchString))
                .Select(b => b.Title)
                .ToList();

            books.ForEach(b => Console.WriteLine(b));
        }

        private static void AuthorsWithNameEnding(ShopSystemContext context)
        {
            var inputEnd = Console.ReadLine();

            var authros = context.Authors
                .Where(a => a.FirstName.EndsWith(inputEnd))
                .Select(a => new
                {
                    Name = a.FirstName + " " + a.LastName
                }).ToList();

            authros.ForEach(a => Console.WriteLine(a.Name));
        }

        private static void GetBooksRealeasedBeforeDate(ShopSystemContext context)
        {
            DateTime date = DateTime.ParseExact(Console.ReadLine(), "dd-MM- yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate < date)
                .Select(b => new
                {
                    Title = b.Title,
                    Edition = b.EditionType.ToString(),
                    Price = b.Price
                }).ToList();

            books.ForEach(b => Console.WriteLine($"{b.Title} - {b.Edition} - {b.Price}"));

        }

        private static void GetBooksByCategories(ShopSystemContext context)
        {
            List<string> categories = Console.ReadLine()
                .Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)
                .ToList();



            var titles = context.Books
                .Where(b => b.Categories.Select(c => c.Name).Any(c => categories.Any(c1 => c1 == c)))
                .OrderBy(b => b.Id)
                .Select(b => b.Title)
                .ToList();

            titles.ForEach(t => Console.WriteLine(t));

        }

        private static void GetBooksNOTRealeasedInYear(ShopSystemContext context)
        {
            int inputYear = int.Parse(Console.ReadLine());

            var titles = context.Books
                .Where(b => b.ReleaseDate != null
                            && b.ReleaseDate.Value.Year != inputYear)
                .OrderBy(b=> b.Id)
                .Select(b => b.Title)
                .ToList();

            titles.ForEach(t => Console.WriteLine(t));

        }

        private static void GetBooksWithPrice(ShopSystemContext context)
        {
            var books = context.Books
                .Where(b => b.Price < 5 || b.Price > 40)
                .Select(b => new
                {
                    Title = b.Title,
                    Price = b.Price
                }).ToList();

            books.ForEach(b => Console.WriteLine($"{b.Title} - {b.Price}"));

        }

        private static void GetGoldEditionBooks(ShopSystemContext context)
        {
            var titles = context.Books
                .Where(b => b.Copies < 5000 && b.EditionType.ToString() == "Gold")
                .Select(b => b.Title)
                .ToList();
            titles.ForEach(t => Console.WriteLine(t));
        }

        private static void BooksTitlesByAgeRestriction(ShopSystemContext context)
        {
            string restriction = Console.ReadLine().ToLower();

            var titles = context.Books
                .Where(b => b.AgeRestriction.ToString() == restriction)
                .Select(b => b.Title)
                .ToArray();

            foreach (var title in titles)
            {
                Console.WriteLine(title);
            }


        }
    }
}
