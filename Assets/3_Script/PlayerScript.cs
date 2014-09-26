using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float _speed = 5.0f;
	private float _halfHeight;
	public Sound sound;

	// Use this for initialization
	void Start () {
		sound = GameObject.Find("Sound").GetComponent<Sound>();
		_halfHeight = Screen.height * 0.5f;
		animation["0_idle"].layer = 0;
		animation["1_damage"].layer = 1;
		animation["1_damage"].speed = 5.0f;
	}

	void DamageSound()
	{
		if (sound.audio.enabled) {
			sound.audio.PlayOneShot(sound._damEffSound);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			float _deltaPosY = Input.GetTouch (0).position.y - _halfHeight;
			float _Ypos = _deltaPosY - transform.localPosition.y;
			transform.Translate(0, _speed * Time.deltaTime * _Ypos * 0.01f, 0);
		}
		transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, -300.0f, 250.0f), transform.localPosition.z);
	}
	
	public int _hp = 100;
	public int _hpDamage = 1;
	public GameObject _DamageEff;
	public UIFilledSprite _GuageBarWidget;
	void OnTriggerEnter(Collider other) {
		_hp -= _hpDamage;
		_GuageBarWidget.fillAmount = _hp * 0.01f;
		if (_hp <= 0) {
			GameObject.Find("GM").SendMessage("GameOver", SendMessageOptions.DontRequireReceiver);
		} else {
			animation.Play ("1_damage");
			var _Eff1 = Instantiate(_DamageEff, transform.localPosition, Quaternion.identity) as GameObject;
			_Eff1.transform.parent = transform;
			_Eff1.transform.localPosition = Vector3.zero;
			_Eff1.transform.localScale = new Vector3(1, 1, 1);
		}
	}
}
