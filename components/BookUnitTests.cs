using NUnit.Framework;
using System.Transactions;

namespace AhmedSabirPerraiTechnicalTest.components
{
    [TestFixture]
    public class BookUnitTests
    {
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

        [Test]
        public async Task ShouldGetBookList()
        {
            using (var scope = new TransactionScope(asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled))
            {
                var api = new BooksApi();
                var books = new Book();

                var getBookList = await api.BookListGetAsync();

                Assert.That(api.HasError == false, "Error Returned By Api");
                Assert.That(getBookList, Is.Not.Null, "Book List Is Null");
                Assert.That(getBookList.Any(), "No Books Found in List");
                scope.Dispose();
            }
        }

        [Test]
        public async Task ShouldGetBookById()
        {
            using(var scope = new TransactionScope(asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled))
            {
                var api = new BooksApi();
                var books = new Book();

                var getAllBooks = await api.BookListGetAsync();
                var bookId = getAllBooks.FirstOrDefault()?.id;

                var getBookById = await api.BookListGetById(bookId);

                Assert.That(api.HasError == false, "Error Returned By Api");
                Assert.That(getBookById, Is.Not.Null, "Book List returned Null");
                Assert.That(getBookById.Any(), "No Book Found");
                scope.Dispose();
            }
        }

        [Test]
        public async Task AddBookToList()
        {
            using (var scope = new TransactionScope(asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled))
            {
                var api = new BooksApi();
                var bookToAdd = new Book();
                bookToAdd.id = "1UnitTestRunId";
                bookToAdd.title = "Unit Test title";
                bookToAdd.author = "Unit test Author";
                bookToAdd.publishedDate = currentDate;
                bookToAdd.ISBN = "978-0-00-199000-0";

                await api.AddToBookList(bookToAdd);

                Assert.That(api.HasError == false, "Error Returned By API");

                var getBookById = await api.BookListGetById(bookToAdd.id);

                Assert.That(api.HasError == false, "Error Returned By API");
                Assert.That(getBookById, Is.Not.Null, "Book List returned Null");
                Assert.That(getBookById.Any(), "No Book Found");
                Assert.That(getBookById.FirstOrDefault(b => b.id == bookToAdd.id), Is.Not.Null, "Book Not Found");
                Assert.That(getBookById.FirstOrDefault(b => b.title == bookToAdd.title).title.Equals(bookToAdd.title), "Book Not Found");
            }
        }

        [Test]
        public async Task ShouldUpdateBookDetails()
        {

            using( var scope = new TransactionScope(asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled))
            {
                var api = new BooksApi();
                var getAllBooks = await api.BookListGetAsync();
                var bookId = getAllBooks.FirstOrDefault()?.id;

                var bookToUpdate = new Book();
                bookToUpdate.id = bookId;
                bookToUpdate.title = "Edited Title";
                bookToUpdate.author = "Edited Author";
                bookToUpdate.publishedDate = currentDate;
                bookToUpdate.ISBN = "978-0-00-199000-0";


                await api.UpdateBookDetailsInList(bookToUpdate);

                Assert.That(api.HasError == false, "Error Returned By API");

                var getBookById = await api.BookListGetById(bookToUpdate.id);

                Assert.That(api.HasError == false, "Error Returned By API");

                Assert.That(getBookById, Is.Not.Null, "Book List returned Null");
                Assert.That(getBookById.Any(), "No Book Found");
                Assert.That(getBookById.FirstOrDefault(b => b.id == bookToUpdate.id), Is.Not.Null, "Book Not Found");
                Assert.That(getBookById.FirstOrDefault(b => b.title == bookToUpdate.title).title.Equals(bookToUpdate.title), "Book Not Found");
                scope.Dispose();
            }
        }

        [Test]

        public async Task ShouldDeleteBook()
        {
            using (var scope = new TransactionScope(asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled))
            {
                var api = new BooksApi();
                var book = new Book();

                var getAllBooks = await api.BookListGetAsync();
                var bookId = getAllBooks.FirstOrDefault()?.id;

                await api.DeleteBookFromList(bookId);

                Assert.That(api.HasError == false, "Error Returned By API");
                scope.Dispose();

            }
        }
    }
}
