﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace HelloT1908E.Helper
{
    public class PasswordHelper
    {
        public string MD5Hash(string input)
        {
            var hash = new StringBuilder();
            var md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));
            foreach (var t in bytes)
            {
                hash.Append(t.ToString("x2"));
            }

            return hash.ToString();
        }

        public string GenerateSalt()
        {
            int size = 7;
            var builder = new StringBuilder();
            var random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString().ToLower();
        }

        public bool ComparePassword(string password, string accountSalt, string accountPasswordHash)
        {
            return MD5Hash(password + accountSalt) == accountPasswordHash;
        }
    }
}