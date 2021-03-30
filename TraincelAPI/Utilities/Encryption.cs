using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Utilities
{
    public static class Encryption
    {
        public static string Encrypt(string password)
        {
            var passwordHasher = new PasswordHasher<UserTable>();
            var user = new UserTable();
            var hashedPassword = passwordHasher.HashPassword(user, password);
            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string enteredPassword)
        {
            var passwordHasher = new PasswordHasher<UserTable>();
            var user = new UserTable();
            bool verified = false;
            var result = passwordHasher.VerifyHashedPassword(user, password, enteredPassword);
            if (result == PasswordVerificationResult.Success) verified = true;
            else if (result == PasswordVerificationResult.SuccessRehashNeeded) verified = true;
            else if (result == PasswordVerificationResult.Failed) verified = false;
            return verified;
        }
    }
}
