using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public interface IUserService
    {
        Models.User AddUser(Models.User user);
        Models.User UpdateUser(Models.User user);
        List<Models.User> FetchAll();
    }
}
