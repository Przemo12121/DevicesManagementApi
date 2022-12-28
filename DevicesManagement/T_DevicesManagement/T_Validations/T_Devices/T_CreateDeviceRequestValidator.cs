namespace T_DevicesManagement.T_Validations.T_Devices;

public class T_CreateDeviceRequestValidator
{
    private readonly CreateDeviceRequestValidator _validator = new();

    // NAME

    [Fact]
    public void Validate_NullName_ShouldNotPasss()
    {
        CreateDeviceRequest request = new()
        {
            Name = null,
            Address = "127.0.0.1:5000"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_OneLetterName_ShouldPasss()
    {
        CreateDeviceRequest request = new()
        {
            Name = "a",
            Address = "127.0.0.1:5000"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyName_ShouldPasss()
    {
        CreateDeviceRequest request = new()
        {
            Name = "any name",
            Address = "127.0.0.1:5000"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyNameLongerThan256_ShouldNotPasss()
    {
        CreateDeviceRequest request = new()
        {
            Name = Enumerable.Range(0, 257).Select(e => "a").Aggregate((a, b) => a + b),
            Address = "127.0.0.1:5000"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    // ADDRESS

    [Fact]
    public void Validate_NullAddress_ShouldNotPasss()
    {
        CreateDeviceRequest request = new()
        {
            Name = "dummy name",
            Address = null
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_PossibleIPv4WithPort_ShouldPasss()
    {
        CreateDeviceRequest request = new()
        {
            Name = "dummy name",
            Address = "127.0.0.1:5000"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_ImpossibleIPv4WithPort_ShouldPasss()
    {
        CreateDeviceRequest request = new()
        {
            Name = "any name",
            Address = "350.0.0.1:5000"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_PossibleIPv4WithoutPort_ShouldNotPasss()
    {
        CreateDeviceRequest request = new()
        {
            Name = "dummy name",
            Address = "127.0.0.1"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_NonIPv4FormatWithPort_ShouldNotPasss()
    {
        CreateDeviceRequest request = new()
        {
            Name = "dummy name",
            Address = "127.0.1:5000"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_PossibleIPv4WithPortAndBadCharacters_ShouldNotPasss()
    {
        CreateDeviceRequest request = new()
        {
            Name = "dummy name",
            Address = "127.0.0.1:5000-invalid"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }
}