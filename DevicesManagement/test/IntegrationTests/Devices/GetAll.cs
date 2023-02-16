using DevicesManagement.DataTransferObjects.Responses;

namespace IntegrationTests.Devices;

[Collection("IntegrationTests")]
public partial class GetAll
{
    [Fact]
    public async void GetDevices_NoParams_ResponsesWith200()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
    }

    [Fact]
    public async void GetDevices_NoParams_ReturnsCountOfAllDevices()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        data.totalCount
            .Should()
            .Be(4);
    }

    [Fact]
    public async void GetDevices_NoParams_ReturnsAllDevices()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}");
        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        var allDevices = FirstUserDevices.Append(SecondUserDevices[0]);

        data.Results
            .Select(d => d.Id)
            .Should()
            .BeEquivalentTo(
                allDevices.Select(d => d.Id)
            );
    }

    [Fact]
    public async void GetDevices_NoParams_ReturnsDevicesOrderedByNameAscending()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        data.Results
            .Should()
            .BeInAscendingOrder(d => d.Name);
    }

    [Fact]
    public async void GetDevices_OrderOfAddressDesc_ReturnsDevicesOrderedByAddressDescending()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}?order=address:desc");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        data.Results
            .Should()
            .BeInDescendingOrder(d => d.Address);
    }

    [Fact]
    public async void GetDevices_LimitOf1_ReturnsCountOfAllDevices()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}?limit=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        data.totalCount
            .Should()
            .Be(4);
    }

    [Fact]
    public async void GetDevices_LimitOf1_ReturnsOneDevice()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}?limit=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        data.Results
            .Should()
            .HaveCount(1);
    }

    [Fact]
    public async void GetDevices_OffsetOf1_ReturnsCountOfAllDevices()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}?offset=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        data.totalCount
            .Should()
            .Be(4);
    }

    [Fact]
    public async void GetDevices_OffsetOf1_ReturnsDevicesWithoutFirst()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}?offset=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<DeviceDto>>();
        var allDevices = FirstUserDevices
            .Append(SecondUserDevices[0])
            .ToList();
        var notIncluded = allDevices.Find(d => d.Name.StartsWith('A'));

        data.Results
            .Should()
            .HaveCount(3);
        data.Results
            .Select(r => r.Id)
            .ToList()
            .Should()
            .NotContain(notIncluded.Id);
    }

    [Fact]
    public async void GetDevices_RequestWithoutToken_ResponsesWith401()
    {
        var response = await HttpClient.GetAsync($"{Route}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void GetDevices_BadRequest_ResponsesWith400()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route}?limit=100000000");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
}
