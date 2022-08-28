using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioSource;

    public AudioClip[] spawnClips;
    public AudioClip[] cannotSpawnClips;

    public float resetDelay = 2.0f;
    public float spawnDelay = 0.35f;

    [SerializeField] private PartsFactory partSpawner;

    private float m_resetTimer;
    private bool m_inReset;

    private float m_spawnTimer;
    private bool m_inSpawn;

    private readonly int m_pushAnimHash = Animator.StringToHash("Clicked");

    private void Update()
    {
        if(m_inReset)
        {
            m_resetTimer -= Time.deltaTime;

            if(m_resetTimer <= 0f)
            {
                anim.ResetTrigger(m_pushAnimHash);
                m_inReset = false;
            }
        }

        if(m_inSpawn)
        {
            m_spawnTimer -= Time.deltaTime;

            if(m_spawnTimer <= 0f)
            {
                partSpawner.Spawn();
                m_inSpawn = false;
            }
        }
    }

    public void Push()
    {
        if (m_inSpawn || m_inReset) return;

        anim.SetTrigger(m_pushAnimHash);

        if (audioSource)
        {
            audioSource.clip = partSpawner.CanSpawn ? spawnClips[Random.Range(0, spawnClips.Length)] : cannotSpawnClips[Random.Range(0, cannotSpawnClips.Length)];
            audioSource.Play();
        }

        m_inSpawn = true;
        m_spawnTimer = spawnDelay;

        m_resetTimer = resetDelay;
        m_inReset = true;
    }
}
