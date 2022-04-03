using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GamesCatalog.Services
{
    public interface IFirebaseAuthentication
    {
        Task<bool> RegisterAsync(string email, string password);
        Task<bool> LoginAsync(string email, string password);
        bool SignOut();
        bool IsSignIn();
    }
}
