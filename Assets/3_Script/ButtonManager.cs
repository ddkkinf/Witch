using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour {
	public GameObject _button;
	public TextMesh _buttonText;
	private Dictionary<GameObject, List<GameObject>> _buttonListMap = new Dictionary<GameObject, List<GameObject>>();
	
	public void addButton(string text, GameObject target, string functionName, Vector3 position, GameObject parent) {
		var Button = Instantiate(_button, Vector3.zero, Quaternion.identity) as GameObject;
		Button.transform.parent = parent.transform;
		Button.transform.localScale = new Vector3(1, 1, 1);
		Button.transform.localPosition = position;
		UIButtonMessage buttonMessage = Button.GetComponent<UIButtonMessage>();
		buttonMessage.target = target;
		buttonMessage.functionName = functionName;
		var ButtonText = Instantiate(_buttonText, Vector3.zero, Quaternion.identity) as TextMesh;
		ButtonText.transform.parent = Button.transform;
		ButtonText.transform.localScale = new Vector3(10, 10, 1);
		ButtonText.transform.localPosition = new Vector3(0, 0, -2);
		ButtonText.text = text;
		List<GameObject> _buttonList;
		if (_buttonListMap.ContainsKey(parent)) {
			_buttonList = _buttonListMap[parent];
		} else {
			_buttonList = new List<GameObject>();
			_buttonListMap.Add(parent, _buttonList);
		}
		_buttonList.Add(Button);
	}
	
	public void clearButton(GameObject parent) {
		if (_buttonListMap.ContainsKey(parent)) {
			foreach (var button in _buttonListMap[parent]) {
				Destroy(button);
			}
		}
	}
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
