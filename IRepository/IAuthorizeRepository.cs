namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IAuthorizeRepository
    {
        Logon Authenticate(string email, string password, ref List<string> errors);

        bool CheckGradStudent(string sid, ref List<string> errors);

        bool CheckForeignStudent(string sid, ref List<string> errors);
    }
}
