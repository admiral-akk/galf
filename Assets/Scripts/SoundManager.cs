using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource source;

    [SerializeField] private AudioClip wallHit;
    [SerializeField] private AudioClip waterHit;
    [SerializeField] private AudioClip ballHit;
    [SerializeField] private AudioClip inHole;
    [SerializeField] private AudioClip ice;
    [SerializeField] private AudioClip grass;


    private AudioClip loopingClip = null;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void WallHit(float percentage)
    {
        source.PlayOneShot(wallHit, Mathf.Pow(Mathf.Clamp(percentage, 0, 0.8f), 2));
    }

    public void WaterHit()
    {
        source.PlayOneShot(waterHit, 0.3f);
    }

    public void BallHit()
    {
        source.PlayOneShot(ballHit, 0.3f);
    }

    public void InHole()
    {
        source.PlayOneShot(inHole, 0.3f);
    }

}
