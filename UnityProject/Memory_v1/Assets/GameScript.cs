using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// This class implements game logic.
/// This is where checks will take place between 2 images, the time / score or the end of the game.
/// </summary>
public class GameScript : MonoBehaviour
{
    #region attributes
    public TextMeshProUGUI timeText, scoreText;
    private AudioSource audioSourceFlipWin, audioSourceFlipLose;

    private List<GameObject> _listGOCard;
    private List<CardScript> _listCheckImgID;

    private int nbCardLeft, nbHit, nbFirstTryHit;

    private bool pauseTimer;
    private float time, score;
    #endregion

    /// <summary>
    /// Initialization
    /// </summary>
    void Start()
    {
        var aSource = GetComponents<AudioSource>();
        audioSourceFlipWin = aSource[0];
        audioSourceFlipLose = aSource[1];

        _listCheckImgID = new List<CardScript>(2);

        nbHit = nbFirstTryHit = 0;
        pauseTimer = false;
        time = score = 0;
    }

    /// <summary>
    /// Update method. 
    /// It will run once per frame. 
    /// Each frame, we will check SCORE, TIME and if player has WON
    /// </summary>
    private void Update()
    {
        //Timer and score
        {
            if (!pauseTimer)
                time += Time.deltaTime;

            var min = Mathf.Floor(time / 60);
            var sec = time % 60;
            var micro = (time * 100) % 100;

            timeText.text = string.Format("{0:00} : {1:00} : {2:00}", min, sec, micro);
            scoreText.text = "Score : " + (nbFirstTryHit * 100).ToString();
        }

        //Win condition
        if (nbCardLeft == 0)
        {
            //foreach (GameObject g in _listGOCard)
            //    Destroy(g);

            StartCoroutine(CoroutineClass.WinGame(this));
            pauseTimer = true;
            score = (nbFirstTryHit / time) * 1000;
        }
    }

    /// <summary>
    /// This method is used to manage the cards that have been returned
    /// _listCheckImgID contains 2 cards. The card 1 and card 2.
    /// tmp1 and tmp2 to avoid blocking when you click on several card too quickly
    /// </summary>
    /// <param name="cs">Direct link to cardScript. Used to check IDs</param>
    public void AddIDToList(CardScript cs)
    {
        CardScript tmp1, tmp2;

        switch (_listCheckImgID.Count)
        {
            case 0:
                _listCheckImgID.Add(cs);
                break;
            case 1:
                _listCheckImgID.Add(cs);

                tmp1 = _listCheckImgID[0];
                tmp2 = _listCheckImgID[1];

                //If cards are not the same, rotate them face down. If same, translate them face shown
                if (!CheckSameImg())
                {
                    StartCoroutine(CoroutineClass.WaiterRotateBack(tmp1, tmp2));
                }
                else
                {
                    StartCoroutine(CoroutineClass.WaiterTranslate(tmp1, tmp2));
                    nbCardLeft--;
                }
                _listCheckImgID.Clear();
                break;
            case 2:
                _listCheckImgID.Clear();
                _listCheckImgID.Add(cs);
                break;
        }
    }

    /// <summary>
    /// Check if imgID in _list[0] equals to imgID in list[1].
    /// Is same --> 2 cards clicked are the same. Return true. Else, return false
    /// And increments nbHit.
    /// </summary>
    /// <returns>True if cards are same, else false</returns>
    private bool CheckSameImg()
    {
        nbHit++;
        if (_listCheckImgID[0].GetImgID() == _listCheckImgID[1].GetImgID())
        {
            nbFirstTryHit++;
            audioSourceFlipWin.Play();
            return true;
        }
        else
        {
            audioSourceFlipLose.Play();
            return false;
        }

    }

    /// <summary>
    /// Win function.
    /// Save datas (score, time, nbHit) to static class.
    /// Change scene.
    /// </summary>
    public void WinGame()
    {
        StaticParameterClass.Score = score;
        StaticParameterClass.Time = timeText.text;
        StaticParameterClass.NbHit = nbHit;
        SceneManager.LoadScene("WinScene");
    }

    /// <summary>
    /// Set list of cards as GameObject.
    /// Used to destroy them.
    /// </summary>
    /// <param name="list">list of GameObject cards</param>
    public void SetGOCardList(List<GameObject> list)
    {
        _listGOCard = list;
        nbCardLeft = _listGOCard.Count / 2;
    }
}
