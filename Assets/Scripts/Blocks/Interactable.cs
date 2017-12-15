using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour
{
    public bool timedout = false;

    public abstract void Interact();

    public IEnumerator HitTimout()
    {
        timedout = true;
        yield return new WaitForSeconds(1f);
        timedout = false;
    }
}
