using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesServices
{
    public class EncryptionService
    {
        public string sha256Encrypt(string text) =>
           string.IsNullOrEmpty(text) ? string.Empty : Convert.ToBase64String(new System.Security.Cryptography.SHA256Managed().ComputeHash(System.Text.Encoding.UTF8.GetBytes(text)));
    }
}
