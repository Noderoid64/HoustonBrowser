using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.Http.Model
{
    public class HttpHeaderField  : IParseable
    {
        public string Name { get; private set; }
        public List<AcceptValue> Values { get; private set; } = new List<AcceptValue>();

        public HttpHeaderField(string name, IEnumerable<AcceptValue> values)
        {
            Name = name;
            Values.AddRange(values);
        }
        public HttpHeaderField(string name, AcceptValue value)
        {
            Name = name;
            Values.Add(value);
        }

        virtual public AcceptValue GetValue(int index)
        {
            if(index >= Values.Count)
            {
                throw new Exception("HttpHeaderField.Values.Count must be less than index");
            }
            else
            {
                return Values[index];
            }
        }
        virtual public void AddValue(AcceptValue value)
        {
            Values.Add(value);
        }
        virtual public void AddValues(IEnumerable<AcceptValue> values)
        {
            Values.AddRange(values);
        }
        virtual public void RemoveValue(int index)
        {
                Values.Remove(GetValue(index));
        }

        public byte[] GetBytes(Encoding encoder)
        {
            byte[] FieldBytes = encoder.GetBytes(Name = ": ").Concat(Values[0].GetBytes(encoder)).ToArray();
            for (int i = 1; i < Values.Count; i++)
            {
                FieldBytes = FieldBytes.Concat( encoder.GetBytes(", ").Concat(Values[i].GetBytes(encoder)).ToArray()).ToArray();
            }
            return FieldBytes;
        }
        public string GetString()
        {
            string FieldString = Name + ": " + Values[0].GetString();
            for (int i = 1; i < Values.Count; i++)
            {
                FieldString += ", " + Values[0].GetString();
            }
            return FieldString;
        }
        public void SetFromString(string value)
        {
            throw new NotImplementedException();
        }
        public void SetFromBytes(byte[] value, Encoding encoder)
        {
            throw new NotImplementedException();
        }
    }
}
