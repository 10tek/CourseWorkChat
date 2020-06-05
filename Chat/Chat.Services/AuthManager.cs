using Chat.DataAccess;
using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Services
{
    public class AuthManager
    {
        public async Task<User> SignIn(string login, string password)
        {
            using (var context = new ChatContext())
            {
                return await context.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
            }
        }

        public async Task<User> SignUp(string login, string password, string name, string surname)
        {
            var user = new User
            {
                Login = login,
                Password = password,
                FirstName = name,
                LastName = surname
            };
            using (var context = new ChatContext())
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
            return user;
        }
    }
}
