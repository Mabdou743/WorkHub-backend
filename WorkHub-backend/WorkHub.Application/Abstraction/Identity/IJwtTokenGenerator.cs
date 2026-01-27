namespace WorkHub.Application
{
    public interface IJwtTokenGenerator
    {
        TokenResult Generate(Guid userId, IList<string> roles);
    }
}
