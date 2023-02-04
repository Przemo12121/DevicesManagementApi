using DevicesManagement.DataTransferObjects.Requests;

namespace T_DevicesManagement.T_Validations.T_Users;

public class T_EditEmployeeRequestValidator
{
    private readonly UpdateEmployeeRequestValidator _validator = new();

    [Fact]
    public void Validate_AllAttributesAsNull_False()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = null,
            Password = null,
            EmployeeId = null
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    // NAME

    [Fact]
    public void Validate_OneLetterName_True()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "a",
            Password = "dummyPassword123",
            EmployeeId = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyName_True()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "any name",
            Password = "dummyPassword123",
            EmployeeId = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_AnyNameLongerThan256_False()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = Enumerable.Range(0, 257).Select(e => "a").Aggregate((a, b) => a + b),
            Password = "dummyPassword123",
            EmployeeId = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    // PASSWORD

    [Fact]
    public void Validate_PasswordShorterThan8_False()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "1234567",
            EmployeeId = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_PasswordLongerThan7WithDigitLowerCaseAndUpperCase_True()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "any name",
            Password = "dummyPassword123",
            EmployeeId = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_PasswordWithoutDigit_False()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "any name",
            Password = "dummyPassword",
            EmployeeId = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_PasswordWithoutLowerCase_False()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "any name",
            Password = "DUMMYPASSWORD123",
            EmployeeId = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_PasswordWithoutUpperCase_False()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "any name",
            Password = "dummypassword123",
            EmployeeId = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_PasswordWithAnySpecialCharacter_False()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "any name",
            Password = "dummy Password123",
            EmployeeId = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_AnyPasswordLongerThan32_False()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dP1" + Enumerable.Range(0, 30).Select(e => "a").Aggregate((a, b) => a + b),
            EmployeeId = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    // EMPLOYEE ID

    [Fact]
    public void Validate_EmployeeEidStartingWithFourLowerLettersAndEightDigits_True()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dummyPassword123",
            EmployeeId = "abcd12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_EmployeeEidStartingWithFourLowerLettersAndEightDigitsAndAnyOtherCharacter_False()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dummyPassword123",
            EmployeeId = "abcd 12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_EmployeeEidStartingWithEightDigitsAndFourLowerLetters_False()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dummyPassword123",
            EmployeeId = "12345678abcd"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_EmployeeEidStartingWithThreeDigitsAndEightLetters_False()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dummyPassword123",
            EmployeeId = "abc12345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_EmployeeEidStartingWithFiveDigitsAndEightLetters_False()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dummyPassword123",
            EmployeeId = "abcd312345678"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_EmployeeEidStartingWithFourDigitsAndSevenLetters_False()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dummyPassword123",
            EmployeeId = "abcd1234567"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_EmployeeEidStartingWithFourDigitsAndNineLetters_False()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "dummy name",
            Password = "dummyPassword123",
            EmployeeId = "abcd123456789"
        };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }
}