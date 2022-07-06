using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace PasswordManager.Entities.Exceptions
{
    public class MultipleErrorsException : Exception
    {
        public IEnumerable<string> Messages { get; set; }

        public MultipleErrorsException(IEnumerable<IdentityError> errors)
        {
            Messages = errors.Select(e => e.Description);
        }

        public MultipleErrorsException(IEnumerable<string> messages)
        {
            Messages = messages;
        }
    }
}