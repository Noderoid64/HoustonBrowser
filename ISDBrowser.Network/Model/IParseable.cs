using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpModule.Model
{
   internal interface IParseable
    {
        byte[] GetBytes(Encoding encoder);
        string GetString();
        void SetFromString(string value);
        void SetFromBytes(byte[] value, Encoding encoder);
    }
}
