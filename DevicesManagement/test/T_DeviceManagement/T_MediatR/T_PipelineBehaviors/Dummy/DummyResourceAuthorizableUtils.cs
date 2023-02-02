using Database.Models.Base;
using DevicesManagement.MediatR.Commands;
using System.Security.Principal;

namespace T_DeviceManagement.T_MediatR.T_PipelineBehaviors.Dummy;

public class DummyResource : DatabaseModel
{
}

public record DummyAuthorizableRequestCommand : IRequest<string>, IResourceAuthorizableCommand<DummyResource>
{
    public Guid ResourceId { get; init; }
    public DummyResource Resource { get; set; }
}

public static class DummyHttpContextAccessorFactory
{
    public static IHttpContextAccessor Create(string employeeId, string role)
    {
        var identityMock = new Mock<IIdentity>();
        identityMock.SetupGet(identity => identity.Name)
            .Returns(employeeId);
        
        var userMock = new Mock<ClaimsPrincipal>();
        userMock.SetupGet(user => user.Identity)
            .Returns(identityMock.Object);
        userMock.SetupGet(user => user.Claims)
            .Returns(new[]
            {
                new Claim(ClaimTypes.Role, role)
            });

        var httpContext = new DefaultHttpContext();
        httpContext.User = userMock.Object;

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.SetupGet(accessor => accessor.HttpContext)
            .Returns(httpContext);

        return httpContextAccessorMock.Object;
    }
}