using System.Collections;
using UnityEngine;

public class ImmedietlyStartFadeout : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    [SerializeField] private float TimeBeforeFade = 0.5f;
    FadeOut[] fadeOuts;
    private void Start()
    {
        fadeOuts = GetComponentsInChildren<FadeOut>();
        StartCoroutine("DestroyObjects");
    }
    IEnumerator DestroyObjects()
    {
        yield return new WaitForSeconds(TimeBeforeFade);
        for (int i = 0; i < fadeOuts.Length; i++)
        {
            fadeOuts[i].StartFadeOut(fadeTime);
        }
        yield return new WaitForSeconds(fadeTime + 0.5f);
        Destroy(gameObject);
    }
}
