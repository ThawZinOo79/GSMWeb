using GSMWeb.Core.Entities;
using GSMWeb.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSMWeb.Infrastructure.Services
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _newsRepository;

        public NewsArticleService(INewsArticleRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<NewsArticle> CreateArticleAsync(NewsArticle article)
        {
            await _newsRepository.AddAsync(article);
            await _newsRepository.SaveChangesAsync();
            return article;
        }

        public async Task<bool> DeleteArticleAsync(int id)
        {
            var article = await _newsRepository.GetByIdAsync(id);
            if (article == null) return false;

            _newsRepository.Delete(article);
            await _newsRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<NewsArticle>> GetAllPublishedArticlesAsync()
        {
            return await _newsRepository.GetAllPublishedAsync();
        }

        public async Task<NewsArticle?> GetArticleByIdAsync(int id)
        {
            return await _newsRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateArticleAsync(NewsArticle article)
        {
            var existingArticle = await _newsRepository.GetByIdAsync(article.Id);
            if (existingArticle == null) return false;

            existingArticle.Title = article.Title;
            existingArticle.Summary = article.Summary;
            existingArticle.ContentBody = article.ContentBody;
            existingArticle.ImageUrl = article.ImageUrl;
            existingArticle.PublishedDate = article.PublishedDate;
            existingArticle.IsPublished = article.IsPublished;

            _newsRepository.Update(existingArticle);
            await _newsRepository.SaveChangesAsync();
            return true;
        }
    }
}