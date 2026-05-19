using System.Collections;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private MeshRenderer Renderer;
    private Material mat;

    private float fadeTime;
    public MaterialPropertyBlock propertyBlock;
    private void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        mat = Renderer.material;
        mat.SetFloat("_DissapearAmount", 0f);
        /*propertyBlock = new MaterialPropertyBlock();
        propertyBlock.SetFloat("_DisappearAmount", 0f);
        Renderer.SetPropertyBlock(propertyBlock);*/
    }

    public void StartFadeOut(float fadeTime)
    {
        this.fadeTime = fadeTime;
        StopAllCoroutines();
        StartCoroutine("Dissapear");
    }

    public void StartFadeIn(float fadeTime)
    {

        this.fadeTime = fadeTime;
        StopAllCoroutines();
        StartCoroutine("Reappear");
    }
    private IEnumerator Dissapear()
    {
        float initialFadeAmount = mat.GetFloat("_DisappearAmount");
        float t = 0f;
        while (t < 1f) 
        {
            t += Time.deltaTime / fadeTime;
            mat.SetFloat("_DisappearAmount", Mathf.Lerp(initialFadeAmount, 1.3f, t));
            yield return null;
        }
    }


    private IEnumerator Reappear()
    {
        float initialFadeAmount = mat.GetFloat("_DisappearAmount");
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeTime;
            mat.SetFloat("_DisappearAmount", Mathf.Lerp(initialFadeAmount, 0, t));
            yield return null;
        }
    }
}
