using System.Security.Cryptography;
using System.Text;
namespace Enroot.Infrastructure.Services;

public static class SymmetricCrypt
{
    public static string Encrypt(string data, string key)
    {
        byte[] initializationVector = Encoding.ASCII.GetBytes("abcede0123456789");
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = initializationVector;
        var symmetricEncryption = aes.CreateEncryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, symmetricEncryption, CryptoStreamMode.Write);
        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            streamWriter.Write(data);
        }
        return Convert.ToBase64String(memoryStream.ToArray());
    }
    public static string Decrypt(string cipherText, string key)
    {
        byte[] initializationVector = Encoding.ASCII.GetBytes("abcede0123456789");
        byte[] buffer = Convert.FromBase64String(cipherText);
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = initializationVector;
        var decryption = aes.CreateDecryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream(buffer);
        using var cryptoStream = new CryptoStream(memoryStream, decryption, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);
        return streamReader.ReadToEnd();
    }
}
