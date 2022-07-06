using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace PasswordManager.Entities.Exceptions
{
    public class RegistrationNotSuccess : Exception
    {
        public RegistrationNotSuccess() : base()
        {
        }

        public RegistrationNotSuccess(string message) : base(message)
        {
        }
    }
}