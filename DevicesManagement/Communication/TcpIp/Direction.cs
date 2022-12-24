namespace Communication.TcpIp;

public record Direction(string Host, int Port) : IDirection
{
    public override string ToString()
    {
        return $"${this.Host}:${this.Port}";
    }
}
