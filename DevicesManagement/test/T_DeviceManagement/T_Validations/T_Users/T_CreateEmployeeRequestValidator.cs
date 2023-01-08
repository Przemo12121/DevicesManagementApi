using DevicesManagement.DataTransferObjects.Requests;

namespace T_DevicesManagement.T_Validations.T_Users;

public class T_CreateEmployeeRequestValidator
{
    private readonly CreateEmployeeRequestValidator _validator = new();

    // NAME

    [Fact]
    public void Validate_NameAsNull_False()
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
    public void Validate_OneLetterName_True()
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
    public void Validate_AnyName_True()
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
    public void Validate_AnyNameLongerThan256_False()
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
    public void Validate_PasswordAsNull_False()
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
    public void Validate_PasswordShorterThan8_False()
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
    public void Validate_PasswordLongerThan7WithDigitLowerCaseAndUpperCase_True()
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
    public void Validate_PasswordWithoutDigit_False()
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
    public void Validate_PasswordWithoutLowerCase_False()
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
    public void Validate_PasswordWithoutUpperCase_False()
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
    public void Validate_PasswordWithAnySpecialCharacter_False()
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
    public void Validate_AnyPasswordLongerThan32_False()
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
    public void Validate_EmployeeEidAsNull_False()
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
    public void Validate_EmployeeEidStartingWithFourLowerLettersAndEightDigits_True()
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
    public void Validate_EmployeeEidStartingWithFourLowerLettersAndEightDigitsAndAnyOtherCharacter_False()
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
    public void Validate_EmployeeEidStartingWithEightDigitsAndFourLowerLetters_False()
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
    public void Validate_EmployeeEidStartingWithThreeDigitsAndEightLetters_False()
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
    public void Validate_EmployeeEidStartingWithFiveDigitsAndEightLetters_False()
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
    public void Validate_EmployeeEidStartingWithFourDigitsAndSevenLetters_False()
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
    public void Validate_EmployeeEidStartingWithFourDigitsAndNineLetters_False()
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