using Domain.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AccountRepository:IAccountRepository
    {
        private readonly AppDbContext _ctx;

        public AccountRepository(AppDbContext context)
        {
            _ctx = context;
        }
        public async Task<bool> UserEmailExists(string email) 
        { 
            return await _ctx.Users.AnyAsync(x => x.Email == email);
        }
        public async Task<bool> UserUsernameExists(string username)
        {
            return await _ctx.Users.AnyAsync(x => x.UserName == username);
        }
    }
}
