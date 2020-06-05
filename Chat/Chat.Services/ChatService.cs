using Chat.DataAccess;
using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Services
{
    public class ChatService
    {
        public async void SendMessage(Message message)
        {
            using (var context = new ChatContext())
            {
                await context.AddAsync(message);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Message>> GetMessages()
        {
            using (var context = new ChatContext())
            {
               return await context.Messages.ToListAsync();
            }
        }
    }
}
