namespace MarketingBox.Sdk.Common.Enums;

public enum RegistrationMode
{
    /// <summary>
    /// Uses traffic engine
    /// </summary>
    Auto,
    
    /// <summary>
    /// Does not use traffic engine
    /// </summary>
    Manual,
    
    /// <summary>
    /// Does not use traffic engine, but need to provide customerInfo
    /// </summary>
    S2S
}