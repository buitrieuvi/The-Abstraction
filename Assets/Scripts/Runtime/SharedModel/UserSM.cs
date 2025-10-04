namespace Abstraction.SharedModel
{
    using System;

    public class UserSM
    {
        public string Id;
        public string UserName;
        public string PasswordHash;

        public string RefreshToken;
        public DateTime RefreshTokenExpiryTime;

        public UserSM(string id, string userName, string passwordHash)
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;
        }
    }

}