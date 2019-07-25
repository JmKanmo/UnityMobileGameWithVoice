using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 모바일프로그래밍 유니티 게임
 * 작성일: 2018.05.11
 * 작성자: 강준모 
 */


// 장애물/동전이 캐릭터와 충돌시 소멸하도록 동작컨트롤함수  

public class coinDestroy : MonoBehaviour
{
    // 소멸동작수행 함수

    public void destroy()
    {
        GetComponent<AudioSource>().Play();
        if (GetComponent<Transform>().tag.Equals("bronzeCoin"))
        {
            GetComponent<Animator>().Play("coinDestroyMotion3");
            Destroy(gameObject, 1f);
        }
        else if (GetComponent<Transform>().tag.Equals("silverCoin"))
        {
            GetComponent<Animator>().Play("coinDestroyMotion2");
            Destroy(gameObject, 1f);
        }
        else if (GetComponent<Transform>().tag.Equals("goldCoin"))
        {
            GetComponent<Animator>().Play("coinDestroyMotion");
            Destroy(gameObject, 1f);
        }
        else if (GetComponent<Transform>().tag.Equals("cheese"))
        {
            GetComponent<Animator>().Play("cheeseDestroyMotion");
            Destroy(gameObject, 1f);
        }
        else if (GetComponent<Transform>().tag.Equals("virus") || GetComponent<Transform>().tag.Equals("virus1") || GetComponent<Transform>().tag.Equals("virus2"))
        {
            GetComponent<Animator>().Play("virusDieMotion");
            Destroy(gameObject, 0.3f);
        }     
    }//End of destroy()
}//End of coinDestroy
