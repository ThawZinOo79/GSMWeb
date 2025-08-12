using GSMWeb.Core.Entities;
using GSMWeb.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GSMWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsArticleService _newsService;

        public NewsController(INewsArticleService newsService)
        {
            _newsService = newsService;
        }

        // GET: api/news
        // Public endpoint for everyone to see published news
        [HttpGet]
        public async Task<IActionResult> GetPublishedNews()
        {
            var articles = await _newsService.GetAllPublishedArticlesAsync();
            return Ok(articles);
        }

        // GET: api/news/{id}
        // Public endpoint to get a single article
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var article = await _newsService.GetArticleByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        // POST: api/news
        // Protected endpoint for creating news articles
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNewsArticle([FromBody] NewsArticle newsArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdArticle = await _newsService.CreateArticleAsync(newsArticle);
            return CreatedAtAction(nameof(GetNewsById), new { id = createdArticle.Id }, createdArticle);
        }

        // PUT: api/news/{id}
        // Protected endpoint for updating news articles
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateNewsArticle(int id, [FromBody] NewsArticle newsArticle)
        {
            if (id != newsArticle.Id)
            {
                return BadRequest("Article ID mismatch.");
            }

            var success = await _newsService.UpdateArticleAsync(newsArticle);
            if (!success)
            {
                return NotFound();
            }
            return NoContent(); // Success
        }

        // DELETE: api/news/{id}
        // Protected endpoint for deleting news articles
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteNewsArticle(int id)
        {
            var success = await _newsService.DeleteArticleAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent(); // Success
        }
    }
}