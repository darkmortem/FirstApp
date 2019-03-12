﻿using FirstApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using FirstApp.Core.Models;

namespace FirstApp.Core.Interfaces
{
    public interface ISQLiteRepository
    {
        IEnumerable<UserDatabaseModel> GetItems();
        UserDatabaseModel GetItem(int id);
        int DeleteItem(int id);
        int SaveItem(UserDatabaseModel item);
    }
}
