using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 모바일프로그래밍 유니티 게임
 * 작성일: 2018.05.11
 * 작성자: 강준모 
 */


// 장애물의 이동속도를 관리하는 클래스

public class Move : MonoBehaviour {

    float speed = 0.008f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.left * speed);
	}
}
