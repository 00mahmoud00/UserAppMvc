using Microsoft.AspNetCore.Mvc;
using UserApp.Models;

namespace UserApp.Controllers;
public class UserController : Controller
{
	private static List<User> _users =
	[
		new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
		new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" }
	];

	public IActionResult Index() => View(_users);
	public IActionResult Create() => View();
	public IActionResult Details(int id)
	{
		var user = _users.FirstOrDefault(u => u.Id == id);
		return user is null ? NotFound("USER NOT FOUND") : View(user);
	}

	[HttpPost]
	public IActionResult Create(User user)
	{
		if (ModelState.IsValid)
		{
			user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
			_users.Add(user);
			return RedirectToAction(nameof(Index));
		}
		return View(user);
	}

	[HttpPost]
	public IActionResult Edit(User updatedUser)
	{
		if (ModelState.IsValid)
		{
			var user = _users.FirstOrDefault(u => u.Id == updatedUser.Id);
			if (user != null)
			{
				user.Name = updatedUser.Name;
				user.Email = updatedUser.Email;
				return RedirectToAction(nameof(Index));
			}
			return NotFound();
		}
		return View(updatedUser);
	}

	[HttpPost]
	public IActionResult Delete(int id)
	{
		var user = _users.FirstOrDefault(u => u.Id == id);
		if (user != null)
		{
			_users.Remove(user);
			return RedirectToAction(nameof(Index));
		}
		return NotFound("Not Appeared");
	}
}
