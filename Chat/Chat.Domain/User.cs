﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Domain
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
