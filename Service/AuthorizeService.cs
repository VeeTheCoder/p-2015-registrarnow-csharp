namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class AuthorizeService
    {
        private readonly IAuthorizeRepository repository;

        public AuthorizeService(IAuthorizeRepository repository)
        {
            this.repository = repository;
        }

        public Logon Authenticate(string email, string password, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                errors.Add("Invalid email or password.");
                throw new ArgumentException();
            }

            Logon result = this.repository.Authenticate(email, password, ref errors);

            if (string.Equals(result.Role, "student"))
            {
                if (this.repository.CheckGradStudent(result.Id, ref errors))
                {
                    result.Role = "gradStudent";
                }
            }

            if (string.Equals(result.Role, "student"))
            {
                if (this.repository.CheckForeignStudent(result.Id, ref errors))
                {
                    result.Role = "foreignStudent";
                }
            }

            return result;
        }
    }
}
