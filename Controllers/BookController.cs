using AhmedSabirPerraiTechnicalTest.components;
using Microsoft.AspNetCore.Mvc;


namespace AhmedSabirPerraiTechnicalTest.Controllers
{
    [ApiController]
    [Route("api/v1/books")]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var api = new BooksApi();
            var bookList = await api.BookListGetAsync();

            return api.HasError
                ? BadRequest(api.LastErrorMessage)
                : Ok(bookList);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByIdAsync(string id)
        {
            var api = new BooksApi();
            var bookListById = await api.BookListGetById(id);

            return api.HasError
                ? BadRequest(api.LastErrorMessage)
                : Ok(bookListById);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Book bookDetails)
        {
            var api = new BooksApi();
            await api.AddToBookList(bookDetails);

            return api.HasError
                ? BadRequest(api.LastErrorMessage)
                : Ok();

        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Book bookDetails)
        {
            var api = new BooksApi();
            await api.UpdateBookDetailsInList(bookDetails);

            return api.HasError
                ? BadRequest(api.LastErrorMessage)
                : Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var api = new BooksApi();
            await api.DeleteBookFromList(id);

            return api.HasError
                ? BadRequest(api.LastErrorMessage)
                : Ok();
        }
    }
}
