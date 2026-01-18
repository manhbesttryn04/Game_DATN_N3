using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSound : MonoBehaviour {
    public AudioClip m_soundLogo;
    private AudioSource m_audio;

    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        m_audio.clip = m_soundLogo;
    }

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            m_audio.Play();
        }
    }
}
