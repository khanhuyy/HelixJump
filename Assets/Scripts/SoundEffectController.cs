using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundEffectController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bounce;
    [SerializeField] private AudioClip passFloor;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip gameOver;

    public void PlayBounce()
    {
        audioSource.clip = bounce;
        audioSource.Play();
    }
    
    public void PlayFall()
    {
        audioSource.clip = passFloor;
        audioSource.Play();
    }
    
    public void PlayWin()
    {
        audioSource.clip = win;
        audioSource.Play();
    }
    
    public void PlayLose()
    {
        audioSource.clip = gameOver;
        audioSource.Play();
    }
}
