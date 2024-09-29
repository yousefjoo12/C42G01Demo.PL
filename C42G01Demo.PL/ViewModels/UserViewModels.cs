using System;
using System.Collections.Generic;

namespace C42G01Demo.PL.ViewModels
{
    public class UserViewModels
    {

        public string Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string PhoneNumbers { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public UserViewModels()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}
