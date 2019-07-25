using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 모바일프로그래밍 유니티 게임
 * 작성일: 2018.05.11
 * 작성자: 강준모 
 */


// 장애물(동전,세균,번개,etc)의 생성,소멸, 화면 UI의 구성을 담당하는 클래스 

public class Constructor : MonoBehaviour
{
    public Text coinText;
    public Text cheeseText;
    public Text distanceText;
    public GameObject[] coins;
    public GameObject virus, virus1, virus2;
    public GameObject lightning;
    public GameObject cheese;
    public GameObject gameState;
    public GameObject clapSound;
    public GameObject lvUpSound;
    public int coinScore, cheeseScore;
    private int coinFrame = 0, virusFrame = 0, virus1Frame = 0, virus2Frame = 0, lightningFrame = 0, cheeseFrame = 0;
    public double distance;
    
    // Use this for initialization
    void Start()
    {
        coinText.text = coinScore.ToString() + "$";
        cheeseText.text = "x " + cheeseScore.ToString();
        distance = (double)Random.Range(1,3);
        distanceText.text = "남은거리 " + distance.ToString("#.#") + "km";
    }//End of Start()

    // Update is called once per frame
    void Update()
    {
        // 생성,소멸 주기를 프레임으로 관리하며 장애물의 종류에 따라 프레임이 다르다.
 
        if ((++coinFrame) % 50 == 0)
        {
            Destroy(Instantiate(coins[(int)Random.Range(0, 3)], new Vector2(2, Random.Range(-0.4f, 0.4f)), Quaternion.identity), 13f);
        }

        if ((++virusFrame) % 400 == 0)
        {
            Destroy(Instantiate(virus, new Vector2(2, Random.Range(-0.4f, 0.4f)), Quaternion.identity), 13f);
        }
        if ((++virus1Frame) % 900 == 0)
        {
            Destroy(Instantiate(virus1, new Vector2(2, Random.Range(-0.4f, 0.4f)), Quaternion.identity), 13f);
        }
        if ((++virus2Frame) % 1300 == 0)
        {
            Destroy(Instantiate(virus2, new Vector2(2, Random.Range(-0.4f, 0.4f)), Quaternion.identity), 13f);
        }
        if ((++lightningFrame) % 700 == 0)
        {
            Destroy(Instantiate(lightning, new Vector2(2, Random.Range(-0.4f, 0.4f)), Quaternion.identity), 13f);
        }
        if ((++cheeseFrame) % 1500 == 0)
        {
            Destroy(Instantiate(cheese, new Vector2(2, Random.Range(-0.4f, 0.4f)), Quaternion.identity), 13f);
        }
    }//End of Update()
}//End of Constructor
