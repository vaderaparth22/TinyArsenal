using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInstance : MonoBehaviour
{
    public AudioSource AudioSource;
    public int audioTimeSample;

    private static MusicInstance instance = null;

    public static MusicInstance Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    
    void Start()
    {
        
    }

    
    void Update()
    {
        audioTimeSample = AudioSource.timeSamples;
        if(audioTimeSample == 1086464)
        {
            print("now");
        }
    }
}
