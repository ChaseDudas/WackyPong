using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();

    public static bool Initialized
    {
        get { return initialized; }
    }

    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;

        audioClips.Add(AudioClipName.ButtonPress, Resources.Load<AudioClip>("8BitSounds/Menu2"));
        audioClips.Add(AudioClipName.BallDeath, Resources.Load<AudioClip>("8BitSounds/Explosion1"));
        audioClips.Add(AudioClipName.HitPaddle, Resources.Load<AudioClip>("8BitSounds/Shoot2"));
        audioClips.Add(AudioClipName.ScorePoint, Resources.Load<AudioClip>("8BitSounds/Pickup2"));
        audioClips.Add(AudioClipName.PickupEffectOn, Resources.Load<AudioClip>("8BitSounds/Pickup4"));
        audioClips.Add(AudioClipName.PickupEffectOff, Resources.Load<AudioClip>("8BitSounds/Pickup1"));
        audioClips.Add(AudioClipName.GameOver, Resources.Load<AudioClip>("8BitSounds/BloopDownPitch1"));
        audioClips.Add(AudioClipName.Pause, Resources.Load<AudioClip>("8BitSounds/Brassic1"));
        audioClips.Add(AudioClipName.SpawnBall, Resources.Load<AudioClip>("8BitSounds/Pickup3"));
        audioClips.Add(AudioClipName.BallOnBall, Resources.Load<AudioClip>("8BitSounds/Menu4"));

    }

    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
