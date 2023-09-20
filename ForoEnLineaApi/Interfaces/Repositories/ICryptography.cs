namespace ForoEnLineaApi.Interfaces.Repositories
{
    public interface ICryptography
    {
        string Encrypt(string plainText, string key, string iv);
        string Encrypt(string plainText);
        string Decrypt(string cipherText, string key, string iv);
        string Decrypt(string cipherText);
        bool TryDecrypt(string cipherText,out string result);
    }
}
