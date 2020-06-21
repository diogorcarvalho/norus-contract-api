using System;
using System.Security.Cryptography;
using System.Text;

namespace NorusContract.Services
{
  public interface ICryptographyService
  {
    string GenerateHash(string input);
    bool VerifyHash(string input, string hash);
  }

  public class CryptographyService : ICryptographyService
  {
    public string GenerateHash(string input)
    {
      string hash;
      using (MD5 md5Hash = MD5.Create())
      {
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
          sBuilder.Append(data[i].ToString("x2"));
        }
        hash = sBuilder.ToString();
      }
      return hash;
    }

    public bool VerifyHash(string input, string hash)
    {
      string hashOfInput = GenerateHash(input);
      StringComparer comparer = StringComparer.OrdinalIgnoreCase;
      if (0 == comparer.Compare(hashOfInput, hash))
      {
        return true;
      }
      else
      {
        return false;
      }
    }
  }
}