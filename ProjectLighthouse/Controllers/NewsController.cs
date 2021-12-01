using LBPUnion.ProjectLighthouse.Types.News;
using Microsoft.AspNetCore.Mvc;

namespace LBPUnion.ProjectLighthouse.Controllers
{
    [ApiController]
    [Route("LITTLEBIGPLANETPS3_XML/news")]
    [Produces("text/xml")]
    public class NewsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            NewsEntry newsEntry = new()
            {
                Category = "no_category",
                Summary = "test summary",
                Image = new NewsImage
                {
                    Hash = "4947269c5f7061b27225611ee58a9a91a8031bbe",
                    Alignment = "right",
                },
                Id = 1,
                Title = "Test Title",
                Text = "Test Text",
                Timestamp = 1348755214000,
            };

            return this.Ok(newsEntry);
        }
    }
}