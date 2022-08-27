using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioSource;

    public AudioClip[] clickClips;

    private readonly int m_pushAnimHash = Animator.StringToHash("");

    public void Push()
    {
        anim.SetTrigger(m_pushAnimHash);
        audioSource.clip = clickClips[Random.Range(0, clickClips.Length)];
        audioSource.Play();
    }
}
