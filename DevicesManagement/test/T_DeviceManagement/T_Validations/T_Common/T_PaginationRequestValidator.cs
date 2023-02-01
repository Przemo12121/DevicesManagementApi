using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.Validations.Common;

namespace T_DevicesManagement.T_Validations.T_Common;

public class T_PaginationRequestValidator
{
    [Fact]
    public void Validate_OffsetLowerThan0_False()
    {
        PaginationRequest request = new()
        {
            Offset = -1,
            Limit = 5,
            Order = "name:asc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_LimitLowerThan1_False()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 0,
            Order = "name:asc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_LimitGreaterThanGiven_False()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 11,
            Order = "name:asc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_LimitEqualToGiven_True()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 10,
            Order = "name:asc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_LimitLessThanGiven_True()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 9,
            Order = "name:asc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_LimitNull_True()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = null,
            Order = "name:asc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_OffsetNull_True() 
    { 
        PaginationRequest request = new()
        {
            Offset = null,
            Limit = 5,
            Order = "name:asc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyOffset_True()
    {
        PaginationRequest request = new()
        {
            Offset = 100000000,
            Limit = 5,
            Order = "name:asc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_OrderWithGivenKeySemicolonAndAsc_True()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 5,
            Order = "name:asc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_OrderWithGivenKeySemicolonAndDesc_True()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 5,
            Order = "name:desc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_ProperOrderStringMixedCasing_True()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 5,
            Order = "nAMe:DEsc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_OrderWithoutOption_False()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 5,
            Order = "name:"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_OrderWithoutSemicolon_False()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 5,
            Order = "namedesc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_OrderWithSpaceInseeadOfSemicolon_False()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 5,
            Order = "name desc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_OrderWithReversedOptionAndKey_False()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 5,
            Order = "desc:name"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_OrderWithoutKey_False()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 5,
            Order = ":name"
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_OrderNull_True()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 5,
            Order = null
        };

        var result = new PaginationRequestValidator(10, new[] { "name" }).Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_OrderOneOfListed_True()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 5,
            Order = "name:asc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name", "other" }).Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_OrderTwoOfListed_False()
    {
        PaginationRequest request = new()
        {
            Offset = 5,
            Limit = 5,
            Order = "name:asc,other:desc"
        };

        var result = new PaginationRequestValidator(10, new[] { "name", "other" }).Validate(request);

        result.IsValid.Should().BeTrue();
    }
}
