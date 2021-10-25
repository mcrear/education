using Education.Core.Models;
using Education.Core.Security;
using System;
using System.Collections.Generic;

namespace Education.Core.Security
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user, IEnumerable<Guid> permissions);
        void RevokeRefreshToken(User user);
    }
}
