using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.User
{
    public interface IUseAppService
    {
        Task<IdentityResult> Register(Entites.User user,CancellationToken cancellationToken );
        Task<IdentityResult> Login(string username, string password);
    }
}
