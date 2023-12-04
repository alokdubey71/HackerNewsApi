using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetNewestStories(int pageSize = 10, int page = 1)
    {
        var stories = await _storyService.GetNewestStories(pageSize, page);
        return Ok(stories);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchStories(string query)
    {
        var result = await _storyService.SearchStories(query);
        return Ok(result);
    }
}
