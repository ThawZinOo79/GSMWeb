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

        [HttpGet]
        public async Task<IActionResult> GetPublishedNews()
        {
            var articles = await _newsService.GetAllPublishedArticlesAsync();
            return Ok(articles);
        }

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
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteNewsArticle(int id)
        {
            var success = await _newsService.DeleteArticleAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}