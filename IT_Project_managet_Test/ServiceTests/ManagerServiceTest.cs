using FakeItEasy;
using IT_Project_manager.Controllers;
using IT_Project_manager.Data;
using IT_Project_manager.Models;
using IT_Project_manager.Services;
using IT_Project_manager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Project_managet_Test.ServiceTests;

public class ManagerServiceTest 
{

    [Fact]
    public void CreateManager_ReturnsManager_OnSuccess()
    {
        // Arrange
        var manager = new Manager { Name = "Marian", Surname = "Testowy", Telephone = "1234567890" };
        var service = new ManagersServiceEF( null );

        // Act
        var result = service.CreateManager( manager );

        // Assert
        Assert.IsType<Manager>( result );
        Assert.Equal( "Marian", result.Name );
        Assert.Equal( "Testowy", result.Surname );
        Assert.Equal( "1234567890", result.Telephone );
    }

   


}
