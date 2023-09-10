using System;
using UnityEngine;


public enum SoundType
{
    ButtonClick
}

[Serializable]
public class Sounds
{
    public SoundType Type;
    public AudioClip Clip;
    [Range(0f, 1f)] public float Volume;
}
public class SoundService : GenericMonoSingleton<SoundService>
{
    private AudioSource source;
    public Sounds[] Sounds;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayClip(SoundType type)
    {
        Sounds sound = Array.Find(Sounds, i => i.Type == type);
        source.volume = sound.Volume;
        source.clip = sound.Clip;
        source.Play();
    }
}
