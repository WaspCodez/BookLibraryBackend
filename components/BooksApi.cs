namespace AhmedSabirPerraiTechnicalTest.components
{
    public class BooksApi
    {
        private readonly BookRepo _bookRepo;
        public bool HasError = false;
        public string LastErrorMessage = "";


        public BooksApi()
        {
            _bookRepo = new BookRepo();
        }

        public async Task<IEnumerable<Book>> BookListGetAsync()
        {
            try
            {
                var bookList = await _bookRepo.GetAllBooksAsync();
                return bookList;
            }
            catch (Exception ex)
            {
                HasError = true;
                LastErrorMessage = ex.Message;
                return Enumerable.Empty<Book>();
            }
        }

        public async Task<IEnumerable<Book>> BookListGetById(string id)
        {
            try
            {
                var bookListById = await _bookRepo.GetAllBooksByIdAsync(id);
                return bookListById;
            }
            catch (Exception ex) {
                HasError = true;
                LastErrorMessage = ex.Message;
                return Enumerable.Empty<Book>();
            }
        }

        public async Task AddToBookList(Book bookDetails)
        {
            try
            {
                await _bookRepo.AddToBookList(bookDetails);
            }
            catch(Exception ex)
            {
                HasError = true;
                LastErrorMessage = ex.Message;
            }
        }

        public async Task UpdateBookDetailsInList(Book bookDetails)
        {
            try
            {
                await _bookRepo.UpdateBookDetailsInList(bookDetails);
            }
            catch(Exception ex)
            {
                HasError = true;
                LastErrorMessage = ex.Message;
            }
        }

        public async Task DeleteBookFromList(string id)
        {
            try
            {
                await _bookRepo.DeleteBookFromList(id);
            }
            catch(Exception ex)
            {
                HasError = true;
                LastErrorMessage = ex.Message;
            }
        }
    }
}



