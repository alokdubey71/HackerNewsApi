using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HackerNewsApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

public interface IStoryService
{
    Task<IEnumerable<Story>> GetNewestStories(int pageSize, int page);
    Task<IEnumerable<Story>> SearchStories(string query);
}
public class StoryService : IStoryService
{
    private readonly HttpClient _httpClient;

    public StoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Story>> GetNewestStories(int pageSize, int page)
    {
        var response = await _httpClient.GetStringAsync($"newstories.json?print=pretty");
        var storyIds = JsonConvert.DeserializeObject<IEnumerable<int>>(response);

        var stories = new List<Story>();

        foreach (var storyId in storyIds.Skip((page - 1) * pageSize).Take(pageSize))
        {
            var story = await GetStoryById(storyId);
            stories.Add(story);
        }

        return stories;
    }

    public async Task<IEnumerable<Story>> SearchStories(string query)
    {
        // This is a simplified example. You would need to implement the actual search logic.
        throw new NotImplementedException();
    }

    private async Task<Story> GetStoryById(int storyId)
    {
        var response = await _httpClient.GetStringAsync($"item/{storyId}.json?print=pretty");
        var story = JsonConvert.DeserializeObject<Story>(response);
        return story;
    }
}