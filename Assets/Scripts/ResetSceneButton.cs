using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSceneButton : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioSource;

    public AudioClip[] clickClips;

    private readonly int m_pushAnimHash = Animator.StringToHash("Clicked");

    private void Update()
    {

    }

    public void Push()
    {
        anim.SetTrigger(m_pushAnimHash);
        //anim.ResetTrigger(m_pushAnimHash);
        //audioSource.clip = clickClips[Random.Range(0, clickClips.Length)];
        //audioSource.Play();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}