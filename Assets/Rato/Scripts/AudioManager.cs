using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private bool EffectsMusic;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(EffectsMusic){
            audioSource.volume = GameController.Instance.audioVolume;
        }else{
            audioSource.volume = GameController.Instance.musicVolume;
        }
    }
}
