using System.Collections;

namespace RagnarockWebApp.Interfaces
{
    public interface IPwdHasher
    {
        string GetHash(string password);
    }
}
