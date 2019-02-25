using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Class manages win scene
/// </summary>
public class WinMenu : MonoBehaviour
{
    #region attributes
    public TextMeshProUGUI winText;
    private string text;
    #endregion

    /// <summary>
    /// Initialization, awake = before start
    /// </summary>
    private void Start()
    {
        text = "You won in " + StaticParameterClass.NbHit + " hits\n You have taken " + StaticParameterClass.Time + " min in total.\n Your score is " + StaticParameterClass.Score;
        winText.text = text;
    }

    /// <summary>
    /// Load menu scene
    /// </summary>
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Not implemented yet.
    /// Save score to external file
    /// </summary>
    public void SaveScore()
    {
        //Not Implemented
    }

    /// <summary>
    /// Quit game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
