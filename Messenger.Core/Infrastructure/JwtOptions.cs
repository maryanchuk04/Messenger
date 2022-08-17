namespace Messenger.Core.Infrastructure;

public class JwtOptions
{
    public string SecretKey { get; set; }

    public double LifeTime { get; set; }
}