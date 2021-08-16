using SpreetailWorkSample.Configuration;

namespace SpreetailWorkSample.Helpers
{
    public static class ConfigurationHelper
    {
        #region AppSettings                
        public static string InputDelimiter => ConfigurationManager.GetValue<string>("InputDelimiter", "AppSettings");     
        #endregion        
    }
}
