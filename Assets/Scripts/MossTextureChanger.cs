using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MossTextureChanger : MonoBehaviour
{

    public Image moss1;
    public Image moss2;
    public Image moss3;
    private float targetAlpha;

	private float aTime = 0.5f;

    public IEnumerator ShowTexture()
    {
        float alpha = moss3.color.a;
        Color color = moss3.color;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            color.a = Mathf.Lerp(alpha, 1, t);
            moss3.color = color;
            yield return null;
        }

        yield return null;
    }

    public IEnumerator HideTexture()
    {
        float alpha = moss3.color.a;
        Color color = moss3.color;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            color.a = Mathf.Lerp(alpha, 0, t);
            moss3.color = color;
            yield return null;
        }

        yield return null;
    }
}
