using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;
	private Sound[] pausedSounds;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void StopPlaying (string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
		Debug.LogWarning("Sound: " + name + " not found!");
		return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Stop ();
 	}

	public void StopFadeOut (string sound, float FadeTime)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
		Debug.LogWarning("Sound: " + name + " not found!");
		return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
	
		StartCoroutine (FadeOut(s,FadeTime));


		}
	

	IEnumerator FadeOut (Sound fs ,float FadeTime) {
			float startVolume = fs.source.volume;
	
			while (fs.source.volume > 0.001) {
				fs.source.volume -= startVolume * Time.deltaTime / FadeTime;
	
			yield return null;
        }
		fs.source.Stop ();
	}

	public void PlayFadeIn (string sound, float FadeTime)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
		Debug.LogWarning("Sound: " + name + " not found!");
		return;
		}
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
	
		StartCoroutine (FadeIn(s, FadeTime));


	}

	IEnumerator FadeIn (Sound fs ,float FadeTime) {
		float target = fs.source.volume;
		fs.source.volume = 0f;
		fs.source.Play ();
			while (fs.source.volume < target) {
				fs.source.volume += Time.deltaTime * FadeTime;
	
			yield return null;
        }
		
	}	

	public void Pause(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Pause();
	}

	public void waitPlaying(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}


		if (!s.source.isPlaying){
			s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
			s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
			s.source.Play();
		}
	}

	public void pauseAll()
	{
		foreach (Sound s in sounds)
		{	if(s.name!="OnClickMenu" && s.name!="OnQuitGame" && s.name!="OnBackMenu" && s.source.isPlaying){
				s.source.volume = (s.source.volume/100)*30;
			}
		}
	}

	public void stopPauseAll()
	{
		foreach(Sound s in sounds){
			s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
			s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
		}
	}
}
