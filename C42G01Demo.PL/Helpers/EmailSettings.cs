using DAL.Models;
using System.Net;
using System.Net.Mail;

namespace C42G01Demo.PL.Helpers
{
	public class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("Smtp.gmail.com",587);
			client.EnableSsl = false;
			client.Credentials = new NetworkCredential("yousefxbxd12@gmail.com", "Yousef#24@11$2001");
			client.Send("yousefxbxd12@gmail.com",email.Reciepints,email.Subject,email.Body);
		}
	}
}
