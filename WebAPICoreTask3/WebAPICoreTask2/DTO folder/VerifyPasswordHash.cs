using System.Text;
namespace WebAPICoreTask2.DTO_folder
{
    public class PasswordHasher
    {
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt) // must be static, bool because we want to return true or false,, note: we can write this method inside  the class made for registration 
            // storedHash means the hashed password stored in the database, it is best practice to name it (storedHas & storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt)) // decryption 
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}
