using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        } // C++에서 배웠던 함수 생성자 (이니셜라이저). Count 클래스 생성 시 최솟값과 최댓값을 전달한다.
    }
    public int columns = 8;
    public int rows = 8; //columns와 row가 각각 8이라는 것은 맵의 넓이가 8 x 8이라는 의미이다.
    public Count wallCount = new Count(5, 9);  //생성되는 장애물 (벽) 이 최소 5개, 최대 9개 생긴다는 의미.
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;

    private Transform boardHolder; //프로그램 재생 시 생성되는 오브젝트들 (음식, 벽 등)이 hierarchy창을 지저분하게 하기 때문에, 
                                   // boardHolder의 하위 멤버로 두는 것을 통해 깔끔하게 보이게 한다. 

    private List<Vector3> gridPositions = new List<Vector3>(); // 랜덤으로 생성되는 모든 오브젝트들의 위치의 부정성 검사
    void InitialiseList()
    {
        gridPositions.Clear();
        for (int x=1; x<columns-1; x++)
        {
            for (int y=1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f)); //부정성 검사 리스트에 요소들이 위치할 수 있는 좌표를 담는다.
            }
        }
    }
    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns + 1; x++) // outerWall 오브젝트를 이용해 이동 불가능한 외벽을 만든다.
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)]; 
                if (x==-1 || x==columns || y==-1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)]; 
                }
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 RandomPosition() //어떤 위치를 랜덤하게 반환하는 함수.
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex); //무언가를 생성할 때 중복되지 않도록 랜덤으로 설정한 위치에 있는 것을 먼저 삭제한다.
        return randomPosition;
    }
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1); // 얼마나 많은 오브젝트를 생성할 지 
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition(); // 생성할 오브젝트 수만큼 random 위치를 뽑아온다.
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)]; // tileArray에 속한 오브젝트들을 랜덤 생성한다.
            Instantiate(tileChoice, randomPosition, Quaternion.identity); 
        }
    }
    public void SetupScene(int level) //유일한 public void이며, 실제로 board를 세팅할 때 사용되는 함수
    {
        BoardSetup();
        InitialiseList();
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);
        int enemyCount = (int)Mathf.Log(level, 2f); //2층 (level) 에는 적이 하나, 4층(level) 에는 적이 둘, 8층 (level) 에는 적이 셋... 을 의미한다. 난이도도 올라감.
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount); // 적의 수는 랜덤이 아니라 항상 고정이라는 의미
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0F), Quaternion.identity); // exit은 항상 같은 위치에 존재한다. 
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
