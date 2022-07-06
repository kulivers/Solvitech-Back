using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace PasswordManager.Entities.Exceptions
{
    public class LoginNotSuccess : MultipleErrorsException
    {
        public LoginNotSuccess(IEnumerable<IdentityError> errors) : base(errors)
        {
            Messages = errors.Select(e => e.Description);
        }

        public LoginNotSuccess(IEnumerable<string> messages) : base(messages)
        {
            Messages = messages;
        }
    }
}