using DevicesManagement.DataTransferObjects.Responses;

namespace IntegrationTests.Users;

[Collection("IntegrationTests")]
public partial class GetUserDevices
{
    [Fact]
    public async void GetUserDevices_NoParams_ResponsesWith200()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(DummyUsers[0])}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
    }

    [Fact]
    public async void GetUserDevices_NoParams_ReturnsCountOfRequestedUserDevices()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(DummyUsers[0])}");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        data.totalCount
            .Should()
            .Be(3);
    }

    [Fact]
    public async void GetUserDevices_NoParams_ReturnsRequestedUserDevices()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(DummyUsers[0])}");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        data.Results
            .Select(d => d.Id)
            .Should()
            .BeEquivalentTo(
                FirstUserDevices.Select(d => d.Id)
            );
    }

    [Fact]
    public async void GetUserDevices_NoParams_ReturnsDevicesOrderedByNameAscending()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(DummyUsers[0])}");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        data.Results
            .Should()
            .BeInAscendingOrder(d => d.Name);
    }

    [Fact]
    public async void GetUserDevices_OrderOfAddressDesc_ReturnsDevicesOrderedByAddressDescending()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(DummyUsers[0])}?order=address:desc");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        data.Results
            .Should()
            .BeInDescendingOrder(d => d.Address);
    }

    [Fact]
    public async void GetUserDevices_LimitOf1_ReturnsCountOfAllUserDevices()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(DummyUsers[0])}?limit=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        data.totalCount
            .Should()
            .Be(3);
    }

    [Fact]
    public async void GetUserDevices_LimitOf1_ReturnsOneDevice()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(DummyUsers[0])}?limit=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        data.Results
            .Should()
            .HaveCount(1);
    }

    [Fact]
    public async void GetUserDevices_OffsetOf1_ReturnsCountOfAllDevices()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(DummyUsers[0])}?offset=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        data.totalCount
            .Should()
            .Be(3);
    }

    [Fact]
    public async void GetUserDevices_OffsetOf1_ReturnsUserDevicesWithoutFirst()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(DummyUsers[0])}?offset=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        var notIncluded = FirstUserDevices.Find(d => d.Name.StartsWith('A'));

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
    public async void GetUserDevices_RequestWithoutToken_ResponsesWith401()
    {
        var response = await HttpClient.GetAsync($"{Route(DummyUsers[0])}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void GetUserDevices_BadRequest_ResponsesWith400()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(DummyUsers[0])}?limit=100000000");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
}
