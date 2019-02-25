using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// MoveObject class
/// Used to rotate and translate card every time someone click on
/// Original code : http://wiki.unity3d.com/index.php?title=MoveObject
/// Transformed into a more generic class
/// </summary>
public static class CoroutineClass
{
    
    public enum MoveType { Time }

    #region IEnumerator

    /// <summary>
    /// Rotate card to an given degree
    /// </summary>
    /// <param name="thisTransform">Object, card (passed as Transform)</param>
    /// <param name="degrees">Degrees</param>
    /// <param name="time">Time used to rotate</param>
    /// <returns></returns>
    internal static IEnumerator Rotation(Transform thisTransform, Vector3 degrees, float time)
    {
        Quaternion endRotation = thisTransform.rotation * Quaternion.Euler(degrees);
        float rate = 1.0f / time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, endRotation, t);
            yield return null;
        }
    }

    /// <summary>
    /// Translate card from startPos to endPos
    /// </summary>
    /// <param name="thisTransform">card passed as Transform</param>
    /// <param name="startPos">initial position</param>
    /// <param name="endPos">final position</param>
    /// <param name="value">time used</param>
    /// <param name="moveType">Type (time)</param>
    /// <returns></returns>
    internal static IEnumerator Translation(Transform thisTransform, Vector3 startPos, Vector3 endPos, float value, MoveType moveType)
    {
        float rate = (moveType == MoveType.Time) ? 1.0f / value : 1.0f / Vector3.Distance(startPos, endPos) * value;
        float t = 0.0f;
        while (t < 1.0)
        {
            t += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }

    /// <summary>
    /// Wait 0.5sec before start RotateBack process
    /// </summary>
    /// <param name="c1">Card 1</param>
    /// <param name="c2">Card 2</param>
    /// <returns></returns>
    internal static IEnumerator WaiterRotateBack(CardScript c1, CardScript c2)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        c1.RotateBackCard();
        c2.RotateBackCard();
    }

    /// <summary>
    /// Translate back card (when they're identical) to the board
    /// </summary>
    /// <param name="c1">Card 1</param>
    /// <param name="c2">Card 2</param>
    /// <returns></returns>
    internal static IEnumerator WaiterTranslate(CardScript c1, CardScript c2)
    {
        yield return new WaitForSecondsRealtime(0.65f);
        try
        {
            c1.TranslateBackCard();
            c2.TranslateBackCard();
        }
        catch (MissingReferenceException)
        {
            Debug.Log("Object destroyed.");
        }
        
    }

    /// <summary>
    /// On Mouse Down check. Used to Rotate card before add it to the list !
    /// </summary>
    /// <param name="cs">Card</param>
    /// <param name="gs">Direct link to GameScript</param>
    /// <returns></returns>
    internal static IEnumerator OnMouseDown(CardScript cs, GameScript gs)
    {
        cs.RotateCard();
        yield return new WaitForSeconds(StaticParameterClass.SpeedLevel + 0.1f);
        gs.AddIDToList(cs);
    }

    /// <summary>
    /// When win, wait 1 second before change Scene (via gs.WinGame())
    /// </summary>
    /// <param name="gs">Direct link to GameScript</param>
    /// <returns></returns>
    internal static IEnumerator WinGame(GameScript gs)
    {
        yield return new WaitForSeconds(1f);
        gs.WinGame();
    }
    #endregion
}