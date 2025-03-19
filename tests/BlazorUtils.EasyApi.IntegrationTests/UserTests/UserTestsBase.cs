using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract;

namespace BlazorUtils.EasyApi.IntegrationTests.UserTests;

public abstract class UserTestsBase(TestsFixture fixture) : TestsBase(fixture)
{
    [Fact]
    public async Task GetUserName_WithoutAuthenticatedUser_ReturnsNull()
    {
        // Arrange
        SignOut();
        var request = new GetUserNameRequest();

        // Act
        var response = await CallHttp<GetUserNameRequest, GetUserNameRequest.Response>(request);

        // Assert
        Assert.Null(response.Name);
    }

    [Fact]
    public async Task GetUserName_WithAuthenticatedUser_ReturnsTheName()
    {
        // Arrange
        var username = "John Doe";
        SignIn(username);

        var request = new GetUserNameRequest();

        // Act
        var response = await CallHttp<GetUserNameRequest, GetUserNameRequest.Response>(request);

        // Assert
        Assert.Equal(username, response.Name);
    } 
}
