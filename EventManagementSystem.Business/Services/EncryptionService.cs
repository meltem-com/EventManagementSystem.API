using Microsoft.AspNetCore.DataProtection;

namespace EventManagementSystem.Business.Services
{
    /// <summary>
    /// Handles encryption and decryption of sensitive data like passwords.
    /// </summary>
    public class EncryptionService
    {
        private readonly IDataProtector _protector;

        public EncryptionService(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("UserPasswordProtector");
        }

        public string Encrypt(string input)
        {
            return _protector.Protect(input);
        }

        public string Decrypt(string input)
        {
            return _protector.Unprotect(input);
        }
    }
}
