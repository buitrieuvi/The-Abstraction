namespace Abstraction.SharedModel
{
    public class UserTokenSM
    {
        public string AccessToken;
        public string RefreshToken;

        public UserTokenSM(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    } 
}