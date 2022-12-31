using Microsoft.AspNetCore.Mvc.Testing;

namespace IT_Project_managet_Test.ControllersTests;
//public class MembersControllerTests : Controller
//{
//    private readonly IMemberService? _memberService;
//    private readonly ILogger<MembersController> _logger;
//    private readonly AppDbContext _appDbContext;
//    [Fact]
//    public void Test_Index_Returns_MembersCount()
//    {
//        //Arrange
//        string searchString = null;
//        var controller = new MembersController( _appDbContext , _logger, _memberService );
//        var result = controller.Index( searchString ) as IActionResult;
//        //Act
//        var membersList = ( List<Member>? )result;
//        //Assert
//        Assert.Equal( 4, membersList.Count);
//    }
//    [Fact]
//    public
//}

public class MembersControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public MembersControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Theory]
    [InlineData( "/" )]
    [InlineData( "/Members/Index" )]
    [InlineData( "/Managers/Index" )]
    [InlineData( "/Home/Index" )]
    public async Task Test_All_Pages(string url)
    {
        //Arrange
        var client = _factory.CreateClient();
        //Act
        var resposne = await client.GetAsync( url );
        int code = ( int )resposne.StatusCode;
        //Assert
        Assert.Equal( 200, code );
    }

    [Theory]
    [InlineData( "Krzysztof", "Palonek" )]
    public async Task Test_For_Members(string name, string surname)
    {
        //Arrange

        //Act
        var resposne = await _httpClient.GetAsync( "/Members/Index" );
        var pageContent = await resposne.Content.ReadAsStringAsync();
        var contentString = pageContent.ToString();
        //Assert
        Assert.Contains( name, contentString );
    }
}