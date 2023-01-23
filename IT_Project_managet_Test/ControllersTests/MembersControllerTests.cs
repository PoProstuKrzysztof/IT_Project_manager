
using FakeItEasy;
using IT_Project_manager.Controllers;
using IT_Project_manager.Data;
using IT_Project_manager.Models;
using IT_Project_manager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using NSubstitute;

namespace IT_Project_managet_Test.ControllersTests;




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
    public async Task MembersController_GetMemberByName_ReturnsKrzysztofPalonek(string name, string surname)
    {
        //Arrange

        //Act
        var resposne = await _httpClient.GetAsync( "/Members/Index" );
        var pageContent = await resposne.Content.ReadAsStringAsync();
        var contentString = pageContent.ToString();
        //Assert
        Assert.Contains( name, contentString );
    }


    [Fact]
    public void CreateMember_ReturnsMember_OnSuccess()
    {
        // Arrange
        var memberModel = new MembersViewModel { Name = "John", Surname = "Doe", Email = "johndoe@email.com", DateOfBirth = new DateTime( 2000, 01, 01 ) };
        var service = new MembersServiceEF( null );

        // Act
        var result = service.CreateMember( memberModel );

        // Assert
        Assert.IsType<Member>( result );
        Assert.Equal( "John", result.Name );
        Assert.Equal( "Doe", result.Surname );
        Assert.Equal( "johndoe@email.com", result.Email );
        Assert.Equal( new DateTime( 2000, 01, 01 ), result.DateOfBirth );
    }


}