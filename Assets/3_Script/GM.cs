using UnityEngine;
using System.Collections;

public class GM : MonoBehaviour {
	public GameObject _gm;
	public GameObject _enemySet;
	public GameObject _nearBgObj;
	public Transform _PlayerObjPool;

	private ButtonManager _buttonManager;
	private Sound sound;

	// Use this for initialization
	void Start () {
		_buttonManager = GameObject.Find ("ButtonManager").GetComponent<ButtonManager>();
		sound = GameObject.Find ("Sound").GetComponent<Sound>();
		sound.PlayGameBgm();
		Time.timeScale = 1.0f;
		PlayerPrefs.SetFloat("timeScale", Time.timeScale);
		_ResultText.text = "";
	}

	public bool _SpawnCheck = true;
	public TextMesh _ScoreText;
	public float _TimerForLevel = 0.0f;
	public float _TimerForLevelLim = 10.0f;
	public PlayerScript _PlayerSt;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {

		}
		_TimerForLevel += Time.deltaTime;
		if (_TimerForLevel > _TimerForLevelLim) {
			if (Time.timeScale < 5.0f) {
				_PlayerSt._hpDamage++;
				Time.timeScale *= 1.2f;
				_TimerForLevelLim *= 1.2f;
			}
			_TimerForLevel = 0;
		}

		_ScoreText.text = (Time.timeSinceLevelLoad * 2000.0f).ToString("N0");
		if (_nearBgObj.transform.localPosition.x < -2460.0f && _SpawnCheck) {
			var Set1 = Instantiate (_enemySet, Vector3.zero, Quaternion.identity) as GameObject;
			Set1.transform.parent = _PlayerObjPool;
			Set1.transform.localScale = new Vector3 (1, 1, 1);
			Set1.transform.localPosition = new Vector3(640, 0, 0);
			_SpawnCheck = false;
		}
		if (_nearBgObj.transform.localPosition.x > -1300.0f && !_SpawnCheck) {
			_SpawnCheck = true;
		}
	}

	public GameObject _PauseUI;
	public GameObject _ResultUI;
	public TextMesh _ResultText;


	void GameOver() {
		sound.Stop();
		_buttonManager.addButton("Retry", _gm, "GameRetry", new Vector3(0, -150, 0), _ResultUI);
		_buttonManager.addButton("Main", _gm, "GameMain", new Vector3(0, 0, 0), _ResultUI);
		_ResultUI.SetActive(true);
		_ResultText.text = "Your Score is \n" + _ScoreText.text;
		Time.timeScale = 0.0f;
	}

	void GamePause() {
		sound.Stop ();
		_buttonManager.addButton("Retry", _gm, "GameRetry", new Vector3(0, -150, 0), _ResultUI);
		_buttonManager.addButton("Continue", _gm, "GameContinue", new Vector3(0, 150, 0), _ResultUI);
		_buttonManager.addButton("Main", _gm, "GameMain", new Vector3(0, 0, 0), _ResultUI);
		PlayerPrefs.SetFloat("timeScale", Time.timeScale);
		Time.timeScale = 0.0f;
		_ResultUI.SetActive(true);
	}
	
	void GameRetry() {
		Time.timeScale = 1.0f;
		_ResultUI.SetActive(false);
		_buttonManager.clearButton(_ResultUI);
		Application.LoadLevel("2_play");
	}

	void GameContinue() {
		sound.PlayGameBgm();
		Time.timeScale = PlayerPrefs.GetFloat("timeScale");
		_ResultUI.SetActive(false);
		_buttonManager.clearButton(_ResultUI);
	}

	void GameMain() {
		_ResultUI.SetActive(false);
		_buttonManager.clearButton(_ResultUI);
		Application.LoadLevel("1_start");
	}
}
