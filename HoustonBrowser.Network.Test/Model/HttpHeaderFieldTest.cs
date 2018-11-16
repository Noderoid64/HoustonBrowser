using Xunit;
using HoustonBrowser.HttpModule.Model;
using HoustonBrowser.HttpModule;

namespace HoustonBrowser.HttpModule.Test
{
    public class HttpHeaderFieldTest
    {
        [Theory]
        [InlineData("Name","Value","Name: Value")]
        [InlineData("Field_-_f", "first, second, third", "Field_-_f: first, second, third")]
        
        internal void GetString_getFieldString_string(string name, string value, string expected)
        {
            HttpHeaderField field = new HttpHeaderField();
            field.name = name;
            field.value = value;

            string real = field.GetString();

            Assert.Equal(real, expected);
        }

        [Theory]
        [InlineData("Accept: text/plain; q=0.5, text/html")]
        internal void CreateHttp_fillName_Equal(string value)
        {
            string expected = "Accept";

            HttpHeaderField headerField = new HttpHeaderField(value);

            Assert.Equal(headerField.name, expected);
        }

        [Theory]
        [InlineData("Accept: text/plain; q=0.5, text/html")]
        internal void CreateHttp_fillValue_Equal(string value)
        {
            string expected = "text/plain; q=0.5, text/html";

            HttpHeaderField headerField = new HttpHeaderField(value);

            Assert.Equal(headerField.value, expected);
        }
    }
}