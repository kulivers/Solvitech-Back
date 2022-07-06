using System;

namespace SecretsProvider
{
    public static class JwtSecretsProvider
    {
        public static string GetJwtSecretKey()
        {
            return "MySuperStrongSecretKey"; //todo change it
        }
    }
    
}