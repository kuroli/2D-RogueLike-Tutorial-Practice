using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;  // instance는 외부에서 접근 가능한 변수이지만 한 번 설정되면 실행되는 동안 계속 유지되고 공유됨.
                                                // GameManager에 담기는 정보는 캐릭터 레벨, 층 수 등 두 개 이상 필요가 없는 정보들이기 때문.
    public BoardManager boardScript;
    private int level = 5; //초기 설정이 5인 이유는 적이 2마리 나타나는 레벨에서 테스트하기 위해서임.

	// Use this for initialization
	void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != null) // 실수로 GameManager가 두 개 이상 생기지 않도록 방지한다.
            Destroy(this); //gameObject는 클래스명이 아니라 인스턴트명 같은데....

        DontDestroyOnLoad(this); // 새 scene이 로딩되었을 때 GameManager가 파괴되지 않도록 한다.
        
        boardScript = GetComponent<BoardManager>(); // BoardManager의 모든 구성요소를 가져온다? InitGame 이전에 이것이 있는 이유?
        InitGame();
	}
	void InitGame()
    {
        boardScript.SetupScene(level);
    }
	// Update is called once per frame
	void Update () {
		
	}
}


