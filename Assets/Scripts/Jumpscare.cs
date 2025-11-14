using System.Collections;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] private AudioSource audioScream;
    [SerializeField] private AudioSource audioDeath;
    [SerializeField] private GameObject jumpscareCam;

    public IEnumerator PlayJumpscareSequence(CanvasGroup fadeScreen, float fadeDuration)
    {
        //Turn on jumpscare camera/enemy
        jumpscareCam.SetActive(true);

        //Start audio
        audioScream.Play();

        //delay death sound
        yield return new WaitForSeconds(2f);
        audioDeath.Play();

        yield return new WaitForSeconds(2f);

        //Fade to black
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            fadeScreen.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }

        fadeScreen.alpha = 1f;

        //Disable jumpscare camera after fade
        jumpscareCam.SetActive(false);
    }
}
