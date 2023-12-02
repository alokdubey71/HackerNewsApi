using HackerNewsApi.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace HackerNewsApi.Services
{
    public interface IStoryService
    {
        IEnumerable<Story> GetNewestStories(int pageSize, int page);
        IEnumerable<Story> SearchStories(string query);
    }

    public class StoryService : IStoryService
    {
        private readonly HttpClient _httpClient;

        public StoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<Story> GetNewestStories(int pageSize, int page)
        {
            // Fetch data from Hacker News API
            // Implement caching logic if needed
            // For demonstration purposes, returning an empty list
            return new List<Story>();
        }

        public IEnumerable<Story> SearchStories(string query)
        {
            // Fetch data from Hacker News API based on search query
            // For demonstration purposes, returning an empty list
            return new List<Story>();
        }

    }

}
