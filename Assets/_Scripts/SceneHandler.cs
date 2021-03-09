using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public Image fadeOutPanel;
    AudioSource audioSourceComponent;
    public AudioClip buttonAudioClip;

    private void Awake()
    {
        audioSourceComponent = GetComponent<AudioSource>();
    }
    public void ChangeScene(int scene)
    {
        audioSourceComponent.PlayOneShot(buttonAudioClip);
        StartCoroutine(ChangingScene(0));
    }
    IEnumerator ChangingScene(int scene)
    {
        Color c = fadeOutPanel.color;
        c.a = 1;
        Tween toFade = fadeOutPanel.DOColor(c,2);
        yield return toFade.WaitForCompletion();
        SceneManager.LoadScene(scene);

    }
}
