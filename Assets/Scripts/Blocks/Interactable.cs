using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour
{

    public abstract void Interact();

    public IEnumerator HitTimout()
    {
        GameManager.instance.timedout = true;
        yield return new WaitForSeconds(1f);
        GameManager.instance.timedout = false;
    }
}
