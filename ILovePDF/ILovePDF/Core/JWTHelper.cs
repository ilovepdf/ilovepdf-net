using System;
using System.Collections.Generic;
using JWT;

namespace ILovePDF.Core
{
    public class JWTHelper
    {
        public static string Encode(Dictionary<string, object> payload, string secret, JwtHashAlgorithm algorithm)
        {
            string token = JsonWebToken.Encode(payload, secret, algorithm);

            return token;
        }

        public static string Decode(string token, string secret)
        {
            
            string decodedJson = JsonWebToken.Decode(token, secret);
            return decodedJson;
        }

        public static bool CheckExpired(string token, string secret)
        {
            try
            {
                JsonWebToken.Decode(token, secret);
                return false;
            }
            catch (Exception)
            {

                return true;
            }
        }

        public static T DecodeToOjbect<T>(string token, string secret)
        {
            return JsonWebToken.DecodeToObject<T>(token, secret);
        }
    }
}
