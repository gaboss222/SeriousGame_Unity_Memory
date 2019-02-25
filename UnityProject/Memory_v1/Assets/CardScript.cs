using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script attached to a card.
/// Manages actions 
/// </summary>
public class CardScript : MonoBehaviour
{
    #region attribute
    AudioSource audioSource;
    GameScript gameScript;

    private int id, imgID;
    private Vector3 destination, source;
    #endregion

    /// <summary>
    /// Initialization
    /// </summary>
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameScript = FindObjectOfType<GameScript>();
        audioSource.volume = StaticParameterClass.VolumeLevel;

        //Used for the translate method
        source = transform.position;
        destination = new Vector3(0, 1f, 2f);
    }

    /// <summary>
    /// On mouse down
    /// Play sound and start rotate card
    /// </summary>
    void OnMouseDown()
    {
        audioSource.Play(0);
        StartCoroutine(CoroutineClass.OnMouseDown(this, gameScript));
    }

    /// <summary>
    /// Method used to rotate card from a Coroutine
    /// And translate card closer than the player
    /// </summary>
    public void RotateCard()
    {
        StartCoroutine(CoroutineClass.Translation(transform, transform.position, transform.position - destination, 0.2f, CoroutineClass.MoveType.Time));
        StartCoroutine(CoroutineClass.Rotation(transform, new Vector3(0, 180, 0), 0.5f));
    }

    /// <summary>
    /// Method used to rotate back card (when 2 returned cards aren't identical)
    /// And translate it to the board
    /// </summary>
    public void RotateBackCard()
    {
        //GetComponent<Collider>().enabled = false;
        StartCoroutine(CoroutineClass.Rotation(transform, new Vector3(0, 180, 0), StaticParameterClass.SpeedLevel + 0.1f));
        StartCoroutine(CoroutineClass.Translation(transform, transform.position, source, 1.0f, CoroutineClass.MoveType.Time));
        //GetComponent<Collider>().enabled = true;
    }

    /// <summary>   
    /// Method used to translate card (face shown) to the board when they're the same
    /// </summary>
    public void TranslateBackCard()
    {
        StartCoroutine(CoroutineClass.Translation(transform, transform.position, source, 1.0f, CoroutineClass.MoveType.Time));
    }

    #region gettersetter
    /// <summary>
    /// Each card has ID
    /// Set ID
    /// </summary>
    /// <param name="id">id</param>
    public void SetID(int id)
    {
        this.id = id;
    }

    /// <summary>
    /// Each card has an image that has an ID
    /// Set image ID
    /// </summary>
    /// <param name="imgID">id</param>
    public void SetImgID(int imgID)
    {
        this.imgID = imgID;
    }

    /// <summary>
    /// Return card ID
    /// </summary>
    /// <returns>card ID</returns>
    public int GetID()
    {
        return id;
    }

    /// <summary>
    /// Return image ID
    /// </summary>
    /// <returns>img ID</returns>
    public int GetImgID()
    {
        return imgID;
    }
    #endregion
}
