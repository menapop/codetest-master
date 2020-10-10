using System.Diagnostics;
using System.Linq;
using Frontend.Utilities;
using DataComponent.Repositories.Interfaces;
using DomainModels.Models;
using DomainModels.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Frontend.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUserRepository _userRepository;

		public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		}

		public IActionResult Index()
		{
			var users = _userRepository.GetAllSync();

			if (users.Count > 0)
			{
				return View("Users", new UsersViewModel
				{
					Users = users.ToList()
				});
			}

			var usersFromJson = new GetUsersFromJson("users.json").Execute();
			_userRepository.AddManySync(usersFromJson);
			users = usersFromJson.ToList();
 
			return View("Users", new UsersViewModel { Users = users.ToList() , user =new User() });
		}

		public IActionResult CreateUser()
		{
			return View();
		}

		[HttpDelete]
		public JsonResult Delete(string id)
		{
			_userRepository.DeleteSync(x => x.Id == id);
			return new JsonResult(new
			{
				success = true,
				responseText = "User successfully deleted!"
			});
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel
			{
				RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
			});
		}

		//Users

		[HttpPost]

		public ActionResult create(User user)
		{
			if (ModelState.IsValid)
			{

				User newuser = new User
				{
					Name = user.Name,
					Email = user.Email,
					Address = user.Address,
					PhoneNumber = user.PhoneNumber,
					CreatedAt = DateTime.Now,
				};
				_userRepository.Add(user);
				return RedirectToAction("Index");
			}

			return View();
		}


	}
}