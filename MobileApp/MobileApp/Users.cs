using System;
using System.Collections.Generic;
using System.Text;
using Plugin.CloudFirestore.Attributes;

namespace MobileApp
{
    class Users
    {
        [MapTo("login")]
        public string Login { get; set; }
        [MapTo("email")]
        public string Email { get; set; }
        [MapTo("pass")]
        public string Pass { get; set; }
        [MapTo("friends")]
        public int FriendsCount { get; set; }

    }
}
