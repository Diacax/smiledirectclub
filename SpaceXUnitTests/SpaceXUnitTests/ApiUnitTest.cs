using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpaceXApi.Controllers;
using SpaceXApi.Models;


namespace SpaceXUnitTests
{
    public class ApiUnitTest
    {
 
        [Fact]
        public async Task WebApiTest()
        {
            // Arrange
            var mockRepo = new Mock<IConfiguration>();
            var newController = new ValuesController(mockRepo.Object);

            // This test will have issues with changing data but for the current demostration purposes it works. 

            //Act 
            IEnumerable<LaunchpadModel> returnData = await newController.GetAllLaunchPadByStatus("under construction");
 
            //Assert
            Assert.Equal("stls", returnData.First().Id);
        }
    }
}
