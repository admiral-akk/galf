using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip intro;
    [SerializeField] private AudioClip loop;
    public static MusicManager instance;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(intro);
            audioSource.PlayDelayed(intro.length - 0.15f);

        } else if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}
