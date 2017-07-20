using JWT;
using System;
using System.Collections.Generic;

namespace LovePdf.Core
{
    /// <summary>
    /// JWTHelper
    /// </summary>
    public static class JwtHelper
    {
        /// <summary>
        /// Encode
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="secret"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public static string Encode(Dictionary<string, object> payload, string secret, JwtHashAlgorithm algorithm)
        {
            var token = JsonWebToken.Encode(payload, secret, algorithm);

            return token;
        }

        /// <summary>
        /// Decode
        /// </summary>
        /// <param name="token"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static string Decode(string token, string secret)
        {

            var decodedJson = JsonWebToken.Decode(token, secret);
            return decodedJson;
        }

        /// <summary>
        /// Check Expired
        /// </summary>
        /// <param name="token"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static bool CheckExpired(string token, string secret)
        {
            try
            {
                JsonWebToken.Decode(token, secret);
                return false;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// Decode To Object
        /// </summary>
        /// <param name="token"></param>
        /// <param name="secret"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DecodeToOjbect<T>(string token, string secret)
        {
            return JsonWebToken.DecodeToObject<T>(token, secret);
        }
    }
}
