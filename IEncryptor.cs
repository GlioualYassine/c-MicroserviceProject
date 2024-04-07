using System;

public interface IEncryptor
{
    string GetSalt();
    string GetHash(string value, string salt);
}
