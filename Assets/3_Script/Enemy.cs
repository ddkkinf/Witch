using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float _speed;
	public GameObject[] _EnemySetObj;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4; i++) {
			_EnemySetObj[i].transform.localPosition += new Vector3(0, Random.Range(-2, 3) * 180.0f, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(_speed * Time.deltaTime, 0, 0);
	}
}
