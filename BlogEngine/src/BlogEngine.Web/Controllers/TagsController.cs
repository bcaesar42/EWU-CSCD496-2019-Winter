using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlogEngine.Web.ApiViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogEngine.Web.Controllers
{
    public class TagsController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }
        public TagsController(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }
        public async Task<IActionResult> Index()
        {
            using (var httpClient = ClientFactory.CreateClient("BlogEngineApi"))
            {
                var secretSantaClient = new BlogEngineApiClient(httpClient.BaseAddress.ToString(), httpClient);
                ViewBag.Tags = await secretSantaClient.GetTagsAsync();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(TagInputViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("BlogEngineApi"))
                {
                    try
                    {
                        var secretSantaClient = new BlogEngineApiClient(httpClient.BaseAddress.ToString(), httpClient);
                        await secretSantaClient.CreateTagAsync(viewModel);

                        result = RedirectToAction(nameof(Index));
                    }
                    catch (SwaggerException se)
                    {
                        ViewBag.ErrorMessage = se.Message;
                    }
                }
            }

            return result;
        }
    }
}
