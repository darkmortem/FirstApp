﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.Interfaces
{
    public interface IRegistrationService
    {
        void UserRegistration(string name, string password);
    }
}
