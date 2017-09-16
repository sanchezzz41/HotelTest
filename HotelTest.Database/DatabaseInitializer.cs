using HotelTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace HotelTest.Database
{
    /// <summary>
    /// Статический класс содержащий метод иницилизации для DatabaseContext
    /// </summary>
    public static class DatabaseInitializer
    {
        public static async Task Initializer(this DatabaseContext context, IServiceProvider provider)
        {
            //Иницилизация ролей
            {
                foreach (RolesOptions role in Enum.GetValues(typeof(RolesOptions)))
                {
                    var roleForAdd = await context.Roles.SingleOrDefaultAsync(x => x.Id == role);
                    if (roleForAdd == null)
                    {
                        roleForAdd = new Role(role, Enum.GetName(typeof(RolesOptions), role));
                        await context.Roles.AddAsync(roleForAdd);
                    }
                }
            }

            //Иницилизация типов комнат
            {
                foreach (RoomOptions typeRoom in Enum.GetValues(typeof(RoomOptions)))
                {
                    var typeRoomForAdd = await context.TypeRooms.SingleOrDefaultAsync(x => x.Id == typeRoom);
                    if (typeRoomForAdd == null)
                    {
                        typeRoomForAdd = new TypeRoom(typeRoom, Enum.GetName(typeof(RoomOptions), typeRoom));
                        await context.TypeRooms.AddAsync(typeRoomForAdd);
                    }
                }
            }
            //Иницилизация админа
            {
                var admin = await context.Users.SingleOrDefaultAsync(x =>
                    x.Fio == "admin" && x.RoleId == RolesOptions.Admin);

                if (admin == null)
                {
                    var hashProvider = provider.GetService<IPasswordHasher<User>>();
                    var passwordSalt = "uigu93gtuh";
                    var password = "admin";
                    var resultHash = hashProvider.HashPassword(null, password + passwordSalt);
                    admin = new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "admin",
                        Fio = "admin",
                        PasswordSalt = passwordSalt,
                        PasswordHash = resultHash,
                        PhoneNumber = "adminNumber",
                        RoleId = RolesOptions.Admin,
                    };
                    await context.Users.AddAsync(admin);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
