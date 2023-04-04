using Tour.Application.Common.Interfaces.Authentication;

namespace Tour.Application.Services.Authentication
{
    public class AuthenticationApp : IAuthentication
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationApp(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult(
                 Guid.NewGuid(),
                 "firstName",
                 "lastName",
                 email,
                 "token");
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            // check if user already exists

            // create user

            // create token
            Guid userId = Guid.NewGuid();
            var token = _jwtTokenGenerator.GenerateToken(userId, email, password);
            return new AuthenticationResult(
                userId,
                firstName,
                lastName,
                email,
                token);
        }
    }
}
