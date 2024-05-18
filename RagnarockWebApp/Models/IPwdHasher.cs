namespace RagnarockWebApp.Models
{
    public interface IPwdHasher
    {
        string UserPwdInput { get; }

        string GetHash(string password);
    }
}
