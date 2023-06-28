using System.ComponentModel.DataAnnotations;

namespace AunctionApp.BLL.Models
{
	public class ResetPasswordVM
	{
		public string UserId { get; set; }

		public string Code { get; set; }

		[Required, DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password), Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
	}
}
