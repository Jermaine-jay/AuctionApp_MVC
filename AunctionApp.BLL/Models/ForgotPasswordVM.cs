using System.ComponentModel.DataAnnotations;

namespace AunctionApp.BLL.Models
{
	public class ForgotPasswordVM
	{
		[Required, DataType(DataType.EmailAddress)]
		public string Email { get; set; }
	}
}
