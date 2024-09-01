using System.Text;

namespace WebAPICoreTask2.DTO_folder
{
    public class PasswordHasherNew
    {

        public static void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) //byte of arrays ,, why use out? to return two values and thats the main difference between out and return
        {

            using (var h = new System.Security.Cryptography.HMACSHA512()) // call the method 
            {
                passwordSalt = h.Key;  // key come from method SHA512
                passwordHash = h.ComputeHash(Encoding.UTF8.GetBytes(password));  // password ==> hashing ,, password must be as bytes, computeHash is a function ,, array of bytes
            }

        }
    }
}


