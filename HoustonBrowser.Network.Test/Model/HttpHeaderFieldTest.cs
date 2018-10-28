using Xunit;
using HoustonBrowser.HttpModule.Model;

namespace HoustonBrowser.HttpModule.Model.Test
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
    }
}