using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IAccountRepository
    {
        Task<bool> UserEmailExists(string email);
        Task<bool> UserUsernameExists(string username);
    }
}
