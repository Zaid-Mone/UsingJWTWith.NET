using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsingJWT.Models;

namespace UsingJWT.Services
{
    public interface ITokenGenerator
    {
        public string CreateToken(User user);
    }
}
