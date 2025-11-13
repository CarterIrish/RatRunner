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
        audioDeath.Play();

        //Wait for the longer audio source to finish
        float longestClip = 0f;
        if (audioScream.clip != null)
            longestClip = Mathf.Max(longestClip, audioScream.clip.length);
        if (audioDeath.clip != null)
            longestClip = Mathf.Max(longestClip, audioDeath.clip.length);

        yield return new WaitForSeconds(longestClip);

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
