using System.Text.Json;

namespace AhmedSabirPerraiTechnicalTest.components
{
    public class BookRepo
    {
        private readonly string _jsonFilePath = "C:\\Users\\onest\\source\\repos\\AhmedSabirPerraiTechnicalTest\\AhmedSabirPerraiTechnicalTest\\bookLibrary.json";
        public BookRepo() { }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            using FileStream fs = new FileStream(_jsonFilePath, FileMode.Open, FileAccess.Read);

            var bookList = await JsonSerializer.DeserializeAsync<IEnumerable<Book>>(fs);

            return bookList;
        }

        public async Task<IEnumerable<Book>> GetAllBooksByIdAsync(string id)
        {
            using FileStream fs = new FileStream(_jsonFilePath, FileMode.Open, FileAccess.Read);

            var bookList = await JsonSerializer.DeserializeAsync<IEnumerable<Book>>(fs);

            var bookById = bookList?.FirstOrDefault(b => b.id == id);

            return bookById != null ? new[] { bookById } : Enumerable.Empty<Book>();
        }

        public async Task AddToBookList(Book bookDetails)
        {

            List<Book> bookList;

            using(FileStream fs = new FileStream(_jsonFilePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (fs.Length == 0)
                {
                    bookList = new List<Book>();
                } 
                else 
                {
                    bookList = await JsonSerializer.DeserializeAsync<List<Book>>(fs);
                }
            }
         
            bool bookExists = bookList.Any(b => b.title.Equals(bookDetails.title, StringComparison.OrdinalIgnoreCase) &&
                                                b.author.Equals(bookDetails.author, StringComparison.OrdinalIgnoreCase));

            if (!bookExists)
            bookList.Add(bookDetails);

            using (FileStream fs = new FileStream(_jsonFilePath, FileMode.Create, FileAccess.Write))
            {
                await JsonSerializer.SerializeAsync(fs, bookList, new JsonSerializerOptions { WriteIndented = true });
            }
        }

        public async Task UpdateBookDetailsInList(Book bookDetails)
        {
            List<Book> bookList;

            // Read existing data from the JSON file
            using (FileStream fs = new FileStream(_jsonFilePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (fs.Length == 0)
                {
                    bookList = new List<Book>();
                }
                else
                {
                    bookList = await JsonSerializer.DeserializeAsync<List<Book>>(fs);
                }
            }

            var bookToUpdate = bookList.FirstOrDefault(b => b.id == bookDetails.id);

            if (bookToUpdate != null)
            {
                // Update the book details
                bookToUpdate.title = bookDetails.title;
                bookToUpdate.author = bookDetails.author;
                bookToUpdate.publishedDate = bookDetails.publishedDate;
                bookToUpdate.ISBN = bookDetails.ISBN;

                // Write the updated data back to the JSON file
                using (FileStream fs = new FileStream(_jsonFilePath, FileMode.Create, FileAccess.Write))
                {
                    await JsonSerializer.SerializeAsync(fs, bookList, new JsonSerializerOptions { WriteIndented = true });
                }
            }
            else
            {
                throw new KeyNotFoundException("Book not found.");
            }
        }

        public async Task DeleteBookFromList(string id)
        {
            List<Book> bookList;

            using (FileStream fs = new FileStream(_jsonFilePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (fs.Length == 0)
                {
                    bookList = new List<Book>();
                }
                else
                {
                    bookList = await JsonSerializer.DeserializeAsync<List<Book>>(fs);
                }
            }

            var bookToRemove = bookList.FirstOrDefault(b => b.id == id);

            if (bookToRemove != null)
            {
                bookList.Remove(bookToRemove);

                using (FileStream fs = new FileStream(_jsonFilePath, FileMode.Create, FileAccess.Write))
                {
                    await JsonSerializer.SerializeAsync(fs, bookList, new JsonSerializerOptions { WriteIndented = true });
                }
            }
            else
            {
                throw new KeyNotFoundException("Book not found.");
            }

        }
    }
}
