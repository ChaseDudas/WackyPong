using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioSoure : MonoBehaviour
{
    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {
        if(!AudioManager.Initialized)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            AudioManager.Initialize(audioSource);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Duplicate so destroy
            Destroy(gameObject);
        }
    }
}
