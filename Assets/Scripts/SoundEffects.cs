using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffects : MonoBehaviour
{
    public List<AudioClip> Knock;
    public List<AudioClip> Paper;
    public List<AudioClip> PaperFast;
    public List<AudioClip> RockSoft;
    public List<AudioClip> RockWobble;
    public List<AudioClip> Shuffle;
    public List<AudioClip> ShuffleSlow;
    public List<AudioClip> Soft;
    public List<AudioClip> SoftMulti;
    public List<AudioClip> Wood;
    public List<AudioClip> WoodQuiet;

    public static SoundEffects SoundEffectsMaster;
    
    private AudioSource _audioSource;
    
    void Start()
    {
        SoundEffectsMaster = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayKnock()
    {
        _audioSource.PlayOneShot(Knock[Random.Range(0, Knock.Count)], _audioSource.volume);
    }
    
    public void PlayPaper()
    {
        _audioSource.PlayOneShot(Paper[Random.Range(0, Paper.Count)], _audioSource.volume);
    }
    
    public void PlayPaperFast()
    {
        _audioSource.PlayOneShot(PaperFast[Random.Range(0, PaperFast.Count)], _audioSource.volume);
    }
    
    public void PlayRockSoft()
    {
        _audioSource.PlayOneShot(RockSoft[Random.Range(0, RockSoft.Count)], _audioSource.volume);
    }
    
    public void PlayRockWobble()
    {
        _audioSource.PlayOneShot(RockWobble[Random.Range(0, RockWobble.Count)], _audioSource.volume);
    }
    
    public void PlayShuffle()
    {
        _audioSource.PlayOneShot(Shuffle[Random.Range(0, Shuffle.Count)], _audioSource.volume);
    }
    
    public void PlayShuffleSlow()
    {
        _audioSource.PlayOneShot(ShuffleSlow[Random.Range(0, ShuffleSlow.Count)], _audioSource.volume);
    }
    
    public void PlaySoft()
    {
        _audioSource.PlayOneShot(Soft[Random.Range(0, Soft.Count)], _audioSource.volume);
    }
    
    public void PlaySoftMulti()
    {
        _audioSource.PlayOneShot(SoftMulti[Random.Range(0, SoftMulti.Count)], _audioSource.volume);
    }
    
    public void PlayWood()
    {
        _audioSource.PlayOneShot(Wood[Random.Range(0, Wood.Count)], _audioSource.volume);
    }
    
    public void PlayWoodQuiet()
    {
        _audioSource.PlayOneShot(WoodQuiet[Random.Range(0, WoodQuiet.Count)], _audioSource.volume);
    }
}
