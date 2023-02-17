using DevicesManagement.DataTransferObjects.Responses;

namespace IntegrationTests.Devices;

[Collection("IntegrationTests")]
public partial class GetCommands
{
    [Fact]
    public async void GetCommands_NoParams_ResponsesWith200()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(FirstDevice)}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
    }

    [Fact]
    public async void GetCommands_NoParams_ReturnsCountOfAllDeviceCommands()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(FirstDevice)}");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<CommandDto>>();
        data.totalCount
            .Should()
            .Be(3);
    }

    [Fact]
    public async void GetCommands_NoParams_ReturnsAllDeviceCommands()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(FirstDevice)}");
        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<CommandDto>>();

        data.Results
            .Select(d => d.Id)
            .Should()
            .BeEquivalentTo(
                FirstDeviceCommands.Select(c => c.Id)
            );
    }

    [Fact]
    public async void GetCommands_NoParams_ReturnsCommandsOrderedByNameAscending()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(FirstDevice)}");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<CommandDto>>();
        data.Results
            .Should()
            .BeInAscendingOrder(c => c.Name);
    }

    [Fact]
    public async void GetCommands_OrderOfBodyDesc_ReturnsCommandsOrderedByCommandsDescending()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(FirstDevice)}?order=body:desc");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<CommandDto>>();
        data.Results
            .Should()
            .BeInDescendingOrder(c => c.Body);
    }

    [Fact]
    public async void GetCommands_LimitOf1_ReturnsCountOfDeviceCommands()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(FirstDevice)}?limit=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<CommandDto>>();
        data.totalCount
            .Should()
            .Be(3);
    }

    [Fact]
    public async void GetCommands_LimitOf1_ReturnsOneCommand()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(FirstDevice)}?limit=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<CommandDto>>();
        data.Results
            .Should()
            .HaveCount(1);
    }

    [Fact]
    public async void GetCommands_OffsetOf1_ReturnsCountOfDeviceCommands()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(FirstDevice)}?offset=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<CommandDto>>();
        data.totalCount
            .Should()
            .Be(3);
    }

    [Fact]
    public async void GetCommands_OffsetOf1_ReturnsCommandsWithoutFirst()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(FirstDevice)}?offset=1");

        var data = await response.Content.ReadFromJsonAsync<PaginationResponseDto<CommandDto>>();
        var notIncluded = FirstDeviceCommands.Find(d => d.Name.StartsWith('A'));

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
    public async void GetCommands_RequestWithoutToken_ResponsesWith401()
    {
        var response = await HttpClient.GetAsync($"{Route(FirstDevice)}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void GetCommands_BadRequest_ResponsesWith400()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.GetAsync($"{Route(FirstDevice)}?limit=100000000");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
}
