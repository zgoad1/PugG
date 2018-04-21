using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static int number = 0;
    public static AudioManager instance;

    public Sound[] sounds;
	private static Sound CurrentSong;
    private int index;

	// Use this for initialization
	void Awake () {
		
        index = number;
        number++;
        if(number <= 1) {
            instance = this;
            Debug.Log("Creating audio manager #" + index);
        } else {
            Destroy(gameObject);
            Debug.Log("Destroying extra audio manager #" + index);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
	}

    public void Play(string name) {
        Sound sound = Array.Find(sounds, s => s.name == name);
        Debug.Log("Playing sound " + sound.name + " at volume " + sound.volume + ", source: " + sound.source.ToString());
        sound.source.Play();
		if(sound.song) {
			if(CurrentSong != null) FadeOut(CurrentSong.name);
			CurrentSong = sound;
		}
    }

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

	IEnumerator FadeInTrack(string name) {
		Sound sound = Array.Find(sounds, s => s.name == name);
		for(float i = 0; i < 60f; i++) {
			sound.source.volume = (i + 1) / 60f * sound.volume;
			yield return null;
		}
	}

	IEnumerator FadeOutTrack(string name) {
		Sound sound = Array.Find(sounds, s => s.name == name);
		for(float i = 0; i < 30f; i++) {
			sound.source.volume = (30f - i) / 30f * sound.volume;
			yield return null;
		}
		Stop(name);
	}

	public void FadeIn(string name) {
		StartCoroutine("FadeInTrack", name);
	}

	public void FadeOut(string name) {
		StartCoroutine("FadeOutTrack", name);
	}
}
