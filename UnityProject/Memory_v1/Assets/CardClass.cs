using UnityEngine;

/// <summary>
/// Class used to modify the properties/components of a card from the outside
/// </summary>
public class CardClass
{
    #region attributes
    public GameObject cardPrefab;
    private Transform frontCard;
    private Transform backCard;
    private Renderer backCardRenderer, frontCardRenderer;
    #endregion

    /// <summary>
    /// Constructor. Create a card and pass prefab to cardPrefab.
    /// </summary>
    /// <param name="cardPrefab">Original GameObject prefab (card)</param>
    public CardClass(GameObject cardPrefab)
    {
        this.cardPrefab = cardPrefab;
        frontCard = cardPrefab.transform.Find("Front");
        frontCardRenderer = frontCard.GetComponent<Renderer>();

        backCard = cardPrefab.transform;
        backCardRenderer = backCard.GetComponent<Renderer>();
    }

    /// <summary>
    /// Set texture on card for front/back
    /// </summary>
    /// <param name="tex">Texture to set</param>
    /// <param name="type">Front of back</param>
    public void SetTextureCard(Texture2D tex, string type)
    {
        switch(type)
        {
            case "back":
                backCardRenderer.material.SetTexture("_MainTex", tex);
                break;
            case "front":
                frontCardRenderer.material.SetTexture("_MainTex", tex);
                break;
        }
    }
}
