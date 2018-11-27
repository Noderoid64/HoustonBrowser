using System;
using Xunit;
using HoustonBrowser.HttpModule.Model.Headers;

namespace HoustonBrowser.Test.HttpModule.Model
{

    public class HeaderFieldContentTypeTest
    {
        [Theory]
        [InlineData("Content-Type: text/html; charset=utf-8", "text/html")]
        public void HeaderFieldContentType_Ctor_d(string value, string expected)
        {
            //Given
            HeaderFieldContentType contentType;
            //When
            contentType = new HeaderFieldContentType(value);
            //Then
            Assert.Equal(contentType.values[0],expected);



        }
    }
}