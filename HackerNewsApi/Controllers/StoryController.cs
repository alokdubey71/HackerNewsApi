using HackerNewsApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _storyService;

        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        [HttpGet]
        public IActionResult GetNewestStories(int pageSize = 10, int page = 1)
        {
            var stories = _storyService.GetNewestStories(pageSize, page);
            return Ok(stories);
        }

        [HttpGet("search")]
        public IActionResult SearchStories(string query)
        {
            var result = _storyService.SearchStories(query);
            return Ok(result);
        }
    }

}
