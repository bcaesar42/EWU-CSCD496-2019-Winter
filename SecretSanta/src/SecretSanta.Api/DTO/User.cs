using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User(SecretSanta.Domain.Models.User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        public static SecretSanta.Domain.Models.User ToEntity(DTO.User user)
        {
            Domain.Models.User userModel = new Domain.Models.User()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return userModel;
        }
    }
}
