namespace T_DevicesManagement.T_Validations.T_Commands;

public class T_EditCommandRequestValidator
{
    private readonly EditCommandRequestValidator _validator = new();

    [Fact]
    public void Validate_AllAttributesAsNull_True()
    {
        EditCommandRequest request = new() 
        {
            Name = null,
            Description = null,
            Body = null
        };
        
        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    // NAME

    [Fact]
    public void Validate_OneLetterName_True()
    {
        EditCommandRequest request = new()
        {
            Name = "a",
            Description = null,
            Body = null
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyName_True()
    {
        EditCommandRequest request = new()
        {
            Name = "any name",
            Description = null,
            Body = null
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyNameLongerThan64_False()
    {
        EditCommandRequest request = new()
        {
            Name = Enumerable.Range(0, 65).Select(e => "a").Aggregate((a, b) => a + b),
            Description = null,
            Body = null
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    // DESCRIPTION

    [Fact]
    public void Validate_OneLetterDescription_True()
    {
        EditCommandRequest request = new()
        {
            Name = null,
            Description = "a",
            Body = null
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyDescription_True()
    {
        EditCommandRequest request = new()
        {
            Name = null,
            Description = "any description",
            Body = null
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyDescriptionLongerThan4097_False()
    {
        EditCommandRequest request = new()
        {
            Name = null,
            Description = Enumerable.Range(0, 4097).Select(e => "a").Aggregate((a, b) => a + b),
            Body = null
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }


    // BODY

    [Fact]
    public void Validate_OneLetterBody_True()
    {
        EditCommandRequest request = new()
        {
            Name = null,
            Description = null,
            Body = "a"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyBody_True()
    {
        EditCommandRequest request = new()
        {
            Name = null,
            Description = null,
            Body = "any body"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyBodyLongerThan2049_False()
    {
        EditCommandRequest request = new()
        {
            Name = null,
            Description = null,
            Body = Enumerable.Range(0, 2049).Select(e => "a").Aggregate((a, b) => a + b)
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }
}