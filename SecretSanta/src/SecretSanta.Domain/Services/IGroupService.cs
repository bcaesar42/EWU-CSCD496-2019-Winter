using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public interface IGroupService
    {
        Models.Group AddGroup(Models.Group @group);
        Models.Group UpdateGroup(Models.Group @group);
        List<Models.Group> FetchAll();
        List<Models.User> GetUsers(int groupId);
    }
}
