using System.Collections.Generic;

namespace DomainModels.Models.ViewModels
{
	public class UsersViewModel
	{
		public UsersViewModel()
		{
			Users = new List<User>();
			user = new User();
		}

		public List<User> Users { get; set; }
		public User user { get; set; }
	}
}