namespace T_DevicesManagement.T_Validations.T_Users;

public class T_CreateEmployeeRequestValidator
{
    private readonly CreateEmployeeRequestValidator _validator = new();

    // NAME

    [Fact]
    public void Validate_NameAsNull_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = null,
            Password = "dummyPassword123",
            EmployeeEid = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_OneLetterName_ShouldPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "a",
            Password = "dummyPassword123",
            EmployeeEid = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyName_ShouldPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "any name",
            Password = "dummyPassword123",
            EmployeeEid = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyNameLongerThan256_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = Enumerable.Range(0, 257).Select(e => "a").Aggregate((a, b) => a + b),
            Password = "dummyPassword123",
            EmployeeEid = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    // PASSWORD

    [Fact]
    public void Validate_PasswordAsNull_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = null,
            EmployeeEid = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_PasswordShorterThan8_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "1234567",
            EmployeeEid = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_PasswordLongerThan7WithDigitLowerCaseAndUpperCase_ShouldPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "any name",
            Password = "dummyPassword123",
            EmployeeEid = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_PasswordWithoutDigit_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "any name",
            Password = "dummyPassword",
            EmployeeEid = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_PasswordWithoutLowerCase_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "any name",
            Password = "DUMMYPASSWORD123",
            EmployeeEid = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_PasswordWithoutUpperCase_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "any name",
            Password = "dummypassword123",
            EmployeeEid = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_PasswordWithAnySpecialCharacter_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "any name",
            Password = "dummy Password123",
            EmployeeEid = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_AnyPasswordLongerThan32_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dP1" + Enumerable.Range(0, 30).Select(e => "a").Aggregate((a, b) => a + b),
            EmployeeEid = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    // EMPLOYEE ID

    [Fact]
    public void Validate_EmployeeEidAsNull_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = null,
            Password = "dummyPassword123",
            EmployeeEid = null
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_EmployeeEidStartingWithFourLowerLettersAndEightDigits_ShouldPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dummyPassword123",
            EmployeeEid = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_EmployeeEidStartingWithFourLowerLettersAndEightDigitsAndAnyOtherCharacter_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dummyPassword123",
            EmployeeEid = "abcd 12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_EmployeeEidStartingWithEightDigitsAndFourLowerLetters_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dummyPassword123",
            EmployeeEid = "12345678abcd"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_EmployeeEidStartingWithThreeDigitsAndEightLetters_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dummyPassword123",
            EmployeeEid = "abc12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_EmployeeEidStartingWithFiveDigitsAndEightLetters_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dummyPassword123",
            EmployeeEid = "abcd312345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_EmployeeEidStartingWithFourDigitsAndSevenLetters_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dummyPassword123",
            EmployeeEid = "abcd1234567"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_EmployeeEidStartingWithFourDigitsAndNineLetters_ShouldNotPass()
    {
        CreateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dummyPassword123",
            EmployeeEid = "abcd123456789"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }
}