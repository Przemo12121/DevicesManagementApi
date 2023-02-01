namespace DevicesManagement.MediatR.Commands;

public interface IRequestContainerCommand<T>
{
    T Request { get; init; }
}
