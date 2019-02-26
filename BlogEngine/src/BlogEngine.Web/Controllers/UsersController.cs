using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using BlogEngine.Web.ApiViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogEngine.Web.Controllers
{
    public class UsersController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }
        private IMapper Mapper { get; }
        public UsersController(IHttpClientFactory clientFactory, IMapper mapper)
        {
            ClientFactory = clientFactory;
            Mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            using (var httpClient = ClientFactory.CreateClient("BlogEngineApi"))
            {
                var blogEngineClient = new BlogEngineApiClient(httpClient.BaseAddress.ToString(), httpClient);
                ViewBag.Users = await blogEngineClient.GetUsersAsync();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserInputViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("BlogEngineApi"))
                {
                    try
                    {
                        var blogEngineClient = new BlogEngineApiClient(httpClient.BaseAddress.ToString(), httpClient);
                        await blogEngineClient.CreateUserAsync(viewModel);

                        result = RedirectToAction(nameof(Index));
                    }
                    catch (SwaggerException se)
                    {
                        ModelState.AddModelError("", se.Message);
                    }
                }
            }

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            UserViewModel fetchedUser = null;

            using (var httpClient = ClientFactory.CreateClient("BlogEngineApi"))
            {
                try
                {
                    var blogEngineClient = new BlogEngineApiClient(httpClient.BaseAddress.ToString(), httpClient);
                    fetchedUser = await blogEngineClient.GetUserByIdAsync(id);
                }
                catch (SwaggerException se)
                {
                    ModelState.AddModelError("", se.Message);
                }
            }
            return View(fetchedUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("BlogEngineApi"))
                {
                    try
                    {
                        var blogEngineClient = new BlogEngineApiClient(httpClient.BaseAddress.ToString(), httpClient);
                        await blogEngineClient.UpdateUserAsync(viewModel.Id, Mapper.Map<UserInputViewModel>(viewModel));

                        result = RedirectToAction(nameof(Index));
                    }
                    catch (SwaggerException se)
                    {
                        ModelState.AddModelError("", se.Message);
                    }
                }
            }

            return result;
        }

        public async Task<IActionResult> Delete(int id)
        {
            IActionResult result = View();
            using (var httpClient = ClientFactory.CreateClient("BlogEngineApi"))
            {
                try
                {
                    var blogEngineClient = new BlogEngineApiClient(httpClient.BaseAddress.ToString(), httpClient);
                    await blogEngineClient.DeleteUserAsync(id);

                    result = RedirectToAction(nameof(Index));
                }
                catch (SwaggerException se)
                {
                    ModelState.AddModelError("", se.Message);
                }
            }

            return result;
        }
    }
}
