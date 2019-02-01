using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    public class TestableGroupService : IGroupService
    {
        public Group AddGroup_Return { get; set; }
        public Group AddGroup_Group { get; set; }

        public Group AddGroup(Group group)
        {
            AddGroup_Group = group;
            return AddGroup_Return;
        }

        public List<Group> FetchAll_Return { get; set; }

        public List<Group> FetchAll()
        {
            return FetchAll_Return;
        }

        public List<User> GetUsers_Return { get; set; }
        public int GetUsers_GroupId { get; set; }

        public List<User> GetUsers(int groupId)
        {
            GetUsers_GroupId = groupId;
            return GetUsers_Return;
        }

        public Group UpdateGroup_Return { get; set; }
        public Group UpdateGroup_Group { get; set; }

        public Group UpdateGroup(Group group)
        {
            UpdateGroup_Group = group;
            return UpdateGroup_Return;
        }
    }
}
