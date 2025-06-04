﻿using chapter4_demo2.Models;

namespace chapter4_demo2
{
    namespace chapter4_demo2
    {
        public static class DataSource
        {
            private static IList<Book> _books { get; set; }

            public static IList<Book> GetBooks()
            {
                if (_books != null)
                {
                    return _books;
                }

                _books = new List<Book>();

                // Book #1
                Book book = new Book
                {
                    Id = 1,
                    ISBN = "978-0-321-87758-1",
                    Title = "Essential C# 5.0",
                    Author = "Mark Michaelis",
                    Price = 59.99m,
                    Location = new Address
                    {
                        City = "HCM City",
                        Street = "D2, Thu Duc District"
                    },
                    Press = new Press
                    {
                        Id = 1,
                        Name = "Addison-Wesley",
                        Category = Category.Book
                    }
                };

                _books.Add(book);

                return _books;
            }
        }
    }
}
