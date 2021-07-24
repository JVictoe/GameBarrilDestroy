using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //Musicas
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource musicaBG;

    //SonsFX
    [SerializeField] private AudioClip[] clipsFX;
    [SerializeField] private AudioSource sonsFX;

    public static AudioManager instance;

    bool stop = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void PlayMusic()
    {
        if (!musicaBG.isPlaying)
        {
            musicaBG.clip = GetRandom();
            musicaBG.Play();
        }
    }

    AudioClip GetRandom()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void PlayAudio(int index)
    {
        sonsFX.clip = clipsFX[index];
        sonsFX.Play();
    }

    public void StopSound()
    {
        musicaBG.Stop();
    }
}
