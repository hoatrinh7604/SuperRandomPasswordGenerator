using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController _instance;
    public static SoundController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    //
    [SerializeField] GameObject prefab;

    public AudioClip bg;
    private AudioSource bgSource;
    public AudioClip gameOver;
    private AudioSource gameOverSource;   
    public AudioClip firing;
    private AudioSource firingSource;    
    public AudioClip bang;
    private AudioSource bangSource;

    public AudioClip clicked;
    private AudioSource clickedSource;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            PlayAudio(this.clicked, 1, false);
        }
    }

    public void PlayAudio(AudioClip audio, float volume, bool isLoopback)
    {
        if (audio == this.bg)
        {
            Play(audio, ref bgSource, volume, isLoopback);
            return;
        }
        if (audio == this.gameOver)
        {
            Play(audio, ref gameOverSource, volume, isLoopback);
            return;
        }
        if (audio == this.firing)
        {
            Play(audio, ref firingSource, volume, isLoopback);
            return;
        }
        if (audio == this.bang)
        {
            Play(audio, ref bangSource, volume, isLoopback);
            return;
        }
        if (audio == this.clicked)
        {
            Play(audio, ref clickedSource, volume, isLoopback);
            return;
        }
    }

    private void Play(AudioClip audio, ref AudioSource audioSource, float volume, bool isLoopback = false)
    {
        audioSource = Instantiate(Instance.prefab).GetComponent<AudioSource>();

        audioSource.volume = volume;
        audioSource.loop = isLoopback;
        audioSource.clip = audio;
        audioSource.Play();

        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void StopAudio(AudioClip audio)
    {
        if (audio == this.bg)
        {
            bgSource?.Stop();
            return;
        }
        if (audio == this.gameOver)
        {
            gameOverSource?.Stop();
            return;
        }
        if (audio == this.firing)
        {
            firingSource?.Stop();
            return;
        }
        if (audio == this.bang)
        {
            bangSource?.Stop();
            return;
        }
        if (audio == this.clicked)
        {
            clickedSource?.Stop();
            return;
        }
    }
}
