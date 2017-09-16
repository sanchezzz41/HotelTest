using System;
using HotelTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelTest.Database
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt):base(opt)
        {
        }

        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Роли
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<TypeRoom> TypeRooms { get; set; }

        public DbSet<Visitor> Visitors { get; set; }
    }
}
