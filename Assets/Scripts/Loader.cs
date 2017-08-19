using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject gameManager;
	// Use this for initialization
	void Awake() {
        if (GameManager.instance == null)
            Instantiate(gameManager); //Hierarchy에 gameManager 오브젝트가 없어도 생성해서 진행. DontDestroyOnLoad의 하위에 생성된다.
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
