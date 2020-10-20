using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface ITokenRepository
    {
        Task<string> CreateToken(AppUser appUser);
    }
}
