using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.ViewModels
{
    public class PairingViewModel
    {
        public UserInputViewModel Santa { get; set; }
        public UserInputViewModel Recipient { get; set; }
    }
}
