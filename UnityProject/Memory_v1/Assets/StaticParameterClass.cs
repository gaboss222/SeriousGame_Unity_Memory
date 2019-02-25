using UnityEngine;

/// <summary>
/// Static class used to save data and options from menu and game
/// </summary>
public static class StaticParameterClass
{
    #region properties
    public static string LevelInformation { get; set; }
    public static int CardInformation { get; set; }
    public static Texture2D TextureBackgroundCard { get; set; }
    public static float VolumeLevel { get; set; }
    public static float SpeedLevel { get; set; }
    public static float Score { get; set; }
    public static string Time { get; set; }
    public static int NbHit { get; set; }
    #endregion
}

