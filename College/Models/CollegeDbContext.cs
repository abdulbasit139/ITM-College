﻿using Microsoft.EntityFrameworkCore;

namespace College.Models
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions options) : base(options) 
        { 
        
        }


    }
}
