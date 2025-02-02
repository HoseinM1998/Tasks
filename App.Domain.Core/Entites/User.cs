using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Core.Entites
{
    public class User : IdentityUser<int>
    {
        public List<Task> Tasks { get; set; }

    }
}
