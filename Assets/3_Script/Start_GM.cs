using UnityEngine;
using System.Collections;

public class Start_GM : MonoBehaviour {
	public GameObject _startGm;
	private ButtonManager _buttonManager;
	private Sound sound;
	public GameObject _popupUI;

	void Awake() {
		sound = GameObject.Find ("Sound").GetComponent<Sound>();
		if (PlayerPrefs.GetInt("SoundCheck") == 0) {
			SoundOff();
		} else {
			SoundOn ();
		}
		sound.PlayLobbyBgm();
	}

	bool MainEnable() {
		return !_popupUI.activeSelf;
	}

	void StartGame() {
		if (MainEnable()) {
			Application.LoadLevel ("2_play");
		}
	}

	public GameObject _SoundOff;
	public GameObject _SoundOn;

	void SoundOff() {
		if (MainEnable()) {
			PlayerPrefs.SetInt("SoundCheck", 0);
			_SoundOff.SetActive(false);
			_SoundOn.SetActive(true);
			sound.SoundOff();
		}
	}

	void SoundOn() {
		if (MainEnable()) {
			PlayerPrefs.SetInt("SoundCheck", 1);
			_SoundOff.SetActive(true);
			_SoundOn.SetActive(false);
			sound.SoundOn();
		}
	}

	void Start() {
		_buttonManager = GameObject.Find ("ButtonManager").GetComponent<ButtonManager>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			_buttonManager.addButton("OK", _startGm, "Exit", new Vector3(-200, 0, 0), _popupUI);
			_buttonManager.addButton("Cancel", _startGm, "Cancel", new Vector3(200, 0, 0), _popupUI);
			_popupUI.SetActive(true);
		}
	}

	void Exit() {
		Application.Quit();
	}

	void Cancel() {
		_buttonManager.clearButton(_popupUI);
		_popupUI.SetActive(false);
	}
}
