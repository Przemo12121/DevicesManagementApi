namespace T_DevicesManagement.T_Validations.T_Devices;

public class T_CreateCommandRequestValidator
{
    private readonly CreateCommandRequestValidator _validator = new();

    // NAME

    [Fact]
    public void Validate_NullName_ShouldNotPasss()
    {
        CreateCommandRequest request = new()
        {
            Name = null,
            Description = "dummy description",
            Body = "dummy body"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_OneLetterName_ShouldPasss()
    {
        CreateCommandRequest request = new()
        {
            Name = "a",
            Description = "dummy description",
            Body = "dummy body"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyName_ShouldPasss()
    {
        CreateCommandRequest request = new()
        {
            Name = "any name",
            Description = "dummy description",
            Body = "dummy body"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyNameLongerThan64_ShouldNotPasss()
    {
        CreateCommandRequest request = new()
        {
            Name = Enumerable.Range(0, 65).Select(e => "a").Aggregate((a, b) => a + b),
            Description = "dummy description",
            Body = "dummy body"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    // DESCRIPTION

    [Fact]
    public void Validate_NullDescription_ShouldPasss()
    {
        CreateCommandRequest request = new()
        {
            Name = "dummy name",
            Description = null,
            Body = "dummy body"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_OneLetterDescription_ShouldPasss()
    {
        CreateCommandRequest request = new()
        {
            Name = "dummy name",
            Description = "a",
            Body = "dummy body"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyDescription_ShouldPasss()
    {
        CreateCommandRequest request = new()
        {
            Name = "dummy name",
            Description = "any description",
            Body = "dummy body"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyDescriptionLongerThan4097_ShouldNotPasss()
    {
        CreateCommandRequest request = new()
        {
            Name = "dummy name",
            Description = Enumerable.Range(0, 4097).Select(e => "a").Aggregate((a, b) => a + b),
            Body = "dummy body"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }


    // BODY

    [Fact]
    public void Validate_NullBody_ShouldNotPasss()
    {
        CreateCommandRequest request = new()
        {
            Name = "dummy name",
            Description = "dummy description",
            Body = null
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_OneLetterBody_ShouldPasss()
    {
        CreateCommandRequest request = new()
        {
            Name = "dummy name",
            Description = "dummy description",
            Body = "a"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyBody_ShouldPasss()
    {
        CreateCommandRequest request = new()
        {
            Name = "dummy name",
            Description = "dummy description",
            Body = "any body"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyBodyLongerThan2049_ShouldNotPasss()
    {
        CreateCommandRequest request = new()
        {
            Name = "dummy name",
            Description = "dummy description",
            Body = Enumerable.Range(0, 2049).Select(e => "a").Aggregate((a, b) => a + b)
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }
}