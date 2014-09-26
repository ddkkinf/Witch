using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {
	public AudioClip _lobbyBgm;
	public AudioClip _playBgm;
	public AudioClip _damEffSound;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		Application.LoadLevel ("1_start");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SoundOff() {
		PlayerPrefs.SetInt("SoundCheck", 0);
		audio.enabled = false;
	}

	public void SoundOn() {
		PlayerPrefs.SetInt("SoundCheck", 1);
		audio.enabled = true;
	}

	bool isSoundOn() {
		return PlayerPrefs.GetInt("SoundCheck") > 0;
	}

	public void PlayGameBgm() {
		if (isSoundOn()) {
			audio.clip = _playBgm;
			audio.Play ();
		}
	}

	public void PlayLobbyBgm() {
		//if (isSoundOn()) {
			audio.clip = _lobbyBgm;
			audio.Play ();
		//}
	}

	public void Stop() {
		//if (isSoundOn()) {
			audio.Stop ();
		//}
	}
}
