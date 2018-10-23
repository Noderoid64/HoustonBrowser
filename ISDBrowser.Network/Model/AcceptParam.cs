using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.Http.Model
{
    public class AcceptParam : IParseable
    {
        private float? qValue;
        public float Qvalue
        {
            get{
                return (float)qValue;
            }
            set
            {
                if(value > 1)
                {
                    throw new Exception("qValue > 1");
                }
                else if (value < 0)
                {
                    throw new Exception("qValue < 0");
                }
                else
                {
                    qValue = (float) Math.Round(value,3);
                    
                }
            }
        }

        public byte[] GetBytes(Encoding encoder)
        {
            return encoder.GetBytes(GetString());
        }

        public string GetString()
        {
            if (qValue == null)
                return null;
            return ";q=" + Math.Round((float) qValue,3); //RFC 2616 3.9 Quality Values
        }

        public void SetFromBytes(byte[] value, Encoding encoder)
        {
            throw new NotImplementedException();
        }

        public void SetFromString(string value)
        {
            throw new NotImplementedException();
        }
    }
}
