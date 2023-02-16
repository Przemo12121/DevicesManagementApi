using DevicesManagement.DataTransferObjects.Responses;

namespace IntegrationTests.Users;

[Collection("IntegrationTests")]
public partial class GetEmployees
{
    [Fact]
    public async void GetEmpployees_NoParams_ResponsesWith200()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
    }

    [Fact]
    public async void GetEmpployees_NoParams_ReturnsCountOfAllEmployees()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<UserDto>>();
        data.totalCount
            .Should()
            .Be(3);
    }

    [Fact]
    public async void GetEmpployees_NoParams_ReturnsAllEmployees()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<UserDto>>();
        data.Results
            .Select(r => r.Id)
            .Should()
            .BeEquivalentTo(
                DummyUsers.Select(u => u.Id)
            );
    }

    [Fact]
    public async void GetEmpployees_NoParams_ReturnsEmployeesOrderedByNameAscending()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<UserDto>>();
        data.Results
            .Should()
            .BeInAscendingOrder(r => r.Name);
    }

    [Fact]
    public async void GetEmpployees_OrderOfEidDesc_ReturnsEmployeesOrderedByEmployeeIdDescending()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}?order=eid:desc");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<UserDto>>();
        data.Results
            .Should()
            .BeInDescendingOrder(r => r.EmployeeId);
    }

    [Fact]
    public async void GetEmpployees_LimitOf1_ReturnsCountOfAllEmployees()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}?limit=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<UserDto>>();
        data.totalCount
            .Should()
            .Be(3);
    }

    [Fact]
    public async void GetEmpployees_LimitOf1_ReturnsOneEmployee()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}?limit=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<UserDto>>();
        data.Results
            .Should()
            .HaveCount(1);
    }

    [Fact]
    public async void GetEmpployees_OffsetOf1_ReturnsCountOfAllEmployees()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}?offset=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<UserDto>>();
        data.totalCount
            .Should()
            .Be(3);
    }

    [Fact]
    public async void GetEmpployees_OffsetOf1_ReturnsEmployeesWithoutFirst()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}?offset=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<UserDto>>();
        var notIncluded = DummyUsers.Find(u => u.Name.StartsWith('A'));

        data.Results
            .Should()
            .HaveCount(2);
        data.Results
            .Select(r => r.Id)
            .ToList()
            .Should()
            .NotContain(notIncluded.Id);
    }

    [Fact]
    public async void GetEmpployees_RequestWithoutToken_ResponsesWith401()
    {
        var response = await HttpClient.GetAsync($"{Route}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void GetEmpployees_BadRequest_ResponsesWith400()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}?limit=100000000");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
}
