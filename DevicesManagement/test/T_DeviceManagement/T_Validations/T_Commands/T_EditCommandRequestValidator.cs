using DevicesManagement.DataTransferObjects.Requests;

namespace T_DevicesManagement.T_Validations.T_Commands;

public class T_EditCommandRequestValidator
{
    private readonly EditCommandRequestValidator _validator = new();

    [Fact]
    public void Validate_AllAttributesAsNull_False()
    {
        UpdateCommandRequest request = new() 
        {
            Name = null,
            Description = null,
            Body = null
        };
        
        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    #region .Name
    [Fact]
    public void Validate_OneLetterName_True()
    {
        UpdateCommandRequest request = new()
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
        UpdateCommandRequest request = new()
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
        UpdateCommandRequest request = new()
        {
            Name = Enumerable.Range(0, 65).Select(e => "a").Aggregate((a, b) => a + b),
            Description = null,
            Body = null
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }
    #endregion

    #region .Description
    [Fact]
    public void Validate_OneLetterDescription_True()
    {
        UpdateCommandRequest request = new()
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
        UpdateCommandRequest request = new()
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
        UpdateCommandRequest request = new()
        {
            Name = null,
            Description = Enumerable.Range(0, 4097).Select(e => "a").Aggregate((a, b) => a + b),
            Body = null
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }
    #endregion

    #region .Body
    [Fact]
    public void Validate_OneLetterBody_True()
    {
        UpdateCommandRequest request = new()
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
        UpdateCommandRequest request = new()
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
        UpdateCommandRequest request = new()
        {
            Name = null,
            Description = null,
            Body = Enumerable.Range(0, 2049).Select(e => "a").Aggregate((a, b) => a + b)
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }
    #endregion
}