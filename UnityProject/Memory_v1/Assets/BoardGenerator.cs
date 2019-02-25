using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class implements the board. 
/// It will generate the cards (instanciation, texture) and place them on the board.
/// </summary>
public class BoardGenerator : MonoBehaviour
{
    #region attribute
    public GameObject cardPrefab;
    public GameObject background;

    private GameScript gameScript;

    private Texture2D frontTexture, backTexture;

    private List<CardClass> _listCard;
    private List<GameObject> _listGOCard;

    private int nbCard;
    private Vector3 posXInit;
    #endregion

    /// <summary>
    /// Initialization
    /// </summary>
    private void Start()
    {
        //Direct link to GameScript.cs
        gameScript = FindObjectOfType<GameScript>();

        _listGOCard = new List<GameObject>();
        _listCard = new List<CardClass>();

        nbCard = StaticParameterClass.CardInformation;

        posXInit = new Vector3(-5.25f, 5f, 0);

        InstantiateCards();
        PositionColCard();
        InstantiateTextures();
    }

    /// <summary>
    /// Instanciate all the card and place them face down, on their initial position, on the board
    /// </summary>
    public void InstantiateCards()
    {
        for (int i = 1; i <= nbCard; i++)
        {
            cardPrefab = GameObject.Instantiate(cardPrefab);
            cardPrefab.name = "CardPrefab" + i.ToString();
            cardPrefab.transform.SetParent(background.transform);
            cardPrefab.transform.localPosition = posXInit;
            cardPrefab.transform.localScale = new Vector3(1.7f, 1.7f, 0.01f);
            cardPrefab.transform.localRotation = new Quaternion(0, 0, 0, 0);
            cardPrefab.GetComponent<CardScript>().SetID(i);

            CardClass c = new CardClass(cardPrefab);

            _listCard.Add(c);
            _listGOCard.Add(cardPrefab);
        }
        gameScript.SetGOCardList(_listGOCard);
    }

    /// <summary>
    /// Place texture (mixed randomly) on each card.
    /// Back texture = card back. Can be chosen from the menu
    /// Front texture = card front
    /// </summary>
    public void InstantiateTextures()
    {
        //List of int. Each int corresponds to a picture.
        List<int> _listImg = new List<int>(nbCard);

        for (int i = 1; i <= nbCard / 2; i++)
        {
            //Each picture will be added 2 times because of 2 cards have them on their front.
            _listImg.Add(i);
            _listImg.Add(i);
        }

        //Shuffle the list to obtain a random effect
        _listImg = ShuffleList(_listImg);

        //Put front texture (pictures) on each card
        string frontTexturePath = StaticParameterClass.LevelInformation;
        for (int i = 0; i < nbCard; i++)
        {
            frontTexture = Resources.Load("CardTexture/" + frontTexturePath + _listImg[i].ToString()) as Texture2D;

            _listGOCard[i].GetComponent<CardScript>().SetImgID(_listImg[i]);

            _listCard[i].SetTextureCard(frontTexture, "front");
        }

        //And put back texture (if not chosen from the menu, put Background_card.png)
        if (StaticParameterClass.TextureBackgroundCard)
            backTexture = StaticParameterClass.TextureBackgroundCard;
        else
            backTexture = Resources.Load("CardTexture/BackgroundCard/Background_card") as Texture2D;

        foreach (CardClass c in _listCard)
        {
            c.SetTextureCard(backTexture, "back");
        }
    }

    /// <summary>
    /// Shuffle function
    /// Mix randomly the list.
    /// </summary>
    /// <param name="list">_listImg contains all picture used to the frontcards</param>
    /// <returns></returns>
    public static List<int> ShuffleList(List<int> list)
    {
        int count = list.Count;
        int end = count - 1;
        for (int i = 0; i < end; ++i)
        {
            int rnd = Random.Range(i, count);
            int tmp = list[i];
            list[i] = list[rnd];
            list[rnd] = tmp;
        }
        return list;
    }

    /// <summary>
    /// Statically place the cards on 4 different columns
    /// </summary>
    private void PositionColCard()
    {
        Vector3 updPosX = new Vector3(3.5f, 0, 0);
        for (int i = 1; i <= nbCard; i += 4)
        {
            _listGOCard[i].transform.localPosition += updPosX;
        }
        for (int i = 2; i <= nbCard; i += 4)
        {
            _listGOCard[i].transform.localPosition += 2 * updPosX;
        }
        for (int i = 3; i <= nbCard; i += 4)
        {
            _listGOCard[i].transform.localPosition += 3 * updPosX;
        }

        float spaceY = 0;
        int nbRow = 0;
        switch (nbCard)
        {
            case 16:
                nbRow = 4;
                spaceY = -3.0f;
                break;

            case 20:
                nbRow = 5;
                spaceY = -2.5f;
                break;

            case 24:
                nbRow = 6;
                spaceY = -2.0f;
                break;
        }
        Vector3 updPosY = new Vector3(0, spaceY, 0);
        PositionRowCard(nbRow, updPosY);
    }

    /// <summary>
    /// Positions Y cards via a static space
    /// </summary>
    /// <param name="nbRow">Number of row (4 row for 16 card, 5 for 20 and 6 for 24)</param>
    /// <param name="updPosY">Space used between each card at the Y coordinate</param>
    private void PositionRowCard(int nbRow, Vector3 updPosY)
    {
        switch (nbRow)
        {
            case 4:
                for (int i = 4; i <= 7; i++)
                {
                    _listGOCard[i].transform.localPosition += updPosY;
                }
                for (int i = 8; i <= 11; i++)
                {
                    _listGOCard[i].transform.localPosition += 2 * updPosY;
                }
                for (int i = 12; i <= 15; i++)
                {
                    _listGOCard[i].transform.localPosition += 3 * updPosY;
                }
                break;
            case 5:
                for (int i = 4; i <= 7; i++)
                {
                    _listGOCard[i].transform.localPosition += updPosY;
                }
                for (int i = 8; i <= 11; i++)
                {
                    _listGOCard[i].transform.localPosition += 2 * updPosY;
                }
                for (int i = 12; i <= 15; i++)
                {
                    _listGOCard[i].transform.localPosition += 3 * updPosY;
                }
                for (int i = 16; i <= 19; i++)
                {
                    _listGOCard[i].transform.localPosition += 4 * updPosY;
                }
                break;
            case 6:
                for (int i = 4; i <= 7; i++)
                {
                    _listGOCard[i].transform.localPosition += updPosY;
                }
                for (int i = 8; i <= 11; i++)
                {
                    _listGOCard[i].transform.localPosition += 2 * updPosY;
                }
                for (int i = 12; i <= 15; i++)
                {
                    _listGOCard[i].transform.localPosition += 3 * updPosY;
                }
                for (int i = 16; i <= 19; i++)
                {
                    _listGOCard[i].transform.localPosition += 4 * updPosY;
                }
                for (int i = 20; i <= 23; i++)
                {
                    _listGOCard[i].transform.localPosition += 5 * updPosY;
                }
                break;
        }
    }
}
