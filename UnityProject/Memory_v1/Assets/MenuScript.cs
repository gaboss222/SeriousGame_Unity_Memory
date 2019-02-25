using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;
using SFB;

/// <summary>
/// Main menu script.
/// Used to save options, level and number of card chosen.
/// </summary>
public class MenuScript : MonoBehaviour
{
    #region attribute
    public Slider VolumeSlider, SpeedSlider;
    public AudioSource AudioSource;
    #endregion

    /// <summary>
    /// Set level to the static class
    /// </summary>
    /// <param name="lvl">level</param>
    public void SetLevel(string lvl)
    {
        StaticParameterClass.LevelInformation = lvl;
    }

    /// <summary>
    /// Set number of cards (16, 20 or 24) to the static class
    /// </summary>
    /// <param name="nbCard">number of cards</param>
    public void SetNbCard(int nbCard)
    {
        StaticParameterClass.CardInformation = nbCard;
    }

    /// <summary>
    /// Change music volume. Saved to the static class
    /// </summary>
    public void OnVolumeSliderValueChanged()
    {
        AudioSource.volume = VolumeSlider.value;
        StaticParameterClass.VolumeLevel = VolumeSlider.value;
    }

    /// <summary>
    /// Change card return back speed. Saved to the static class
    /// </summary>
    public void OnSpeedSliderValueChanged()
    {
        StaticParameterClass.SpeedLevel = SpeedSlider.value;
    }

    /// <summary>
    /// Load a background image as Texture2D and pass it to the Static class
    /// OpenFilePanel from https://github.com/gkngkc/UnityStandaloneFileBrowser
    /// </summary>
    public void SetBackgroundCard()
    {
        Texture2D texture = new Texture2D(100, 100);

        //Used only in Editor mode
        //string pathToTexture = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
       
        // Open file with filter
        var extensions = new[] {
        new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
        };

        var pathToTexture = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);

        if (pathToTexture.Length != 0)
        {
            var fileContent = File.ReadAllBytes(pathToTexture[0]);
            texture.LoadImage(fileContent);
            StaticParameterClass.TextureBackgroundCard = texture;
        }
    }

    /// <summary>
    /// Change scene
    /// Assign the volume and speed if they're not been changed
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("MemoryGame");
        StaticParameterClass.VolumeLevel = VolumeSlider.value;
        StaticParameterClass.SpeedLevel = SpeedSlider.value;
    }

    /// <summary>
    /// Properly quit application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
