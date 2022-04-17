using DecartesClassLibrary.Classes;
using Xunit;
using Bunit;
//using BlazorDiffApp1.Pages;
using Microsoft.Extensions.DependencyInjection;
using System;
//using BlazorDiffApp1.Services;

namespace TestProject1
{
    public class BUnitTest
    {
        //Finish me! This is the awesome bUnit funcionality to
        //provide the integration test just clicking HTML buttons/links programmatically
        //Obstacle found on 4/17/2022: cannot run test along with the WebAPI same time
        //Thoughts on how to fix that?

        /*[Theory]
        [InlineData("38,67,131,231,8,91", "38,67,131,231,8,91")]
        public void CompareValid(string blobStr1, string blobStr2)
        {
            
            using var ctx = new TestContext();
            ctx.Services.AddTransient<DiffWebApiClient>();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            var page = ctx.RenderComponent<DiffPage>(parameters => parameters
              .Add(p => p.DebugTextLeft, blobStr1)
              .Add(p => p.DebugTextRight, blobStr2));
            page.Find("form").Submit();
            Assert.True(page.Markup.Contains("Equals"));
            
            
        }
        */
    }
}
