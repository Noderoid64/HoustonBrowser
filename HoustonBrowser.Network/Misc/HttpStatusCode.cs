using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoustonBrowser.HttpModule
{
    internal static class HttpStatusCode
    {
        // 1xx Information
        public const ushort Continue = 100;
        public const ushort SwitchingProtocols = 101;
        public const ushort Processing = 102;

        // 2xx Success
        public const ushort OK = 200;
        public const ushort Created = 201;
        public const ushort Accepted = 202;
        public const ushort NonAuthoritativeInformation = 203;
        public const ushort NoContent = 204;
        public const ushort ResetContent = 206;
        public const ushort MultiStatus = 207;
        public const ushort AlreadyReported = 208;
        public const ushort IMUsed = 226;

        // 3xx Redirection
        public const ushort MultiplyChoices = 300;
        public const ushort MovedPermanently = 301;

        // 4xx Client Error
        public const ushort BadRequest = 400;

        // 5xx Server Error
        public const ushort InternalServerError = 500;

    }
}
