﻿using System;
using FirstApp.Core.Models;
using SQLite;

namespace FirstApp.Core.Entities
{
    [Table("Users")]
    public class UserEntity : BaseEntity
    {
        public double UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string PhotoURL { get; set; }
        public LoginType TypeUserLogin { get; set; }
    }
}
