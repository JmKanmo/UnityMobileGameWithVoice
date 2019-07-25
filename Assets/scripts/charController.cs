using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 모바일프로그래밍 유니티 게임
 * 작성일: 2018.05.11
 * 작성자: 강준모 
 */


//캐릭터 동작을 관리하는 클래스 

public class charController : MonoBehaviour
{
    public Sprite level2;
    public Sprite level3;
    public GameObject pitchGauge;
    public Constructor constructor;
    private float power = 5.5f;
    private float pitchValue = 0f;
    private float pitchSum = 0f;
    private int pitchFrame = 0;
    private int pitchLevel = 0, pitchLevelTemp = 0;
    private int stopFrame = 0;
    private int run = 0;
    private bool isLevel2 = false;
    private bool isLevel3 = false;

    // Update is called once per frame
    void Update()
    {
        // 캐릭터가 레벨업했을 때 동작컨트롤 
        
        if (constructor.coinScore >= 5000 && !isLevel2)
        {
            constructor.lvUpSound.GetComponent<AudioSource>().Play();
            GetComponent<SpriteRenderer>().sprite = level2;
            constructor.cheeseScore += 3;
            constructor.cheeseText.text = "x " + constructor.cheeseScore.ToString();
            isLevel2 = true;
        }
        if (constructor.coinScore >= 10000 && !isLevel3)
        {
            constructor.lvUpSound.GetComponent<AudioSource>().Play();
            GetComponent<SpriteRenderer>().sprite = level3;
            constructor.cheeseScore += 3;
            constructor.cheeseText.text = "x " + constructor.cheeseScore.ToString();
            isLevel3 = true;
        }
        
        // 캐릭터의 주행거리 계산 & UI갱신  

        if (++run % 300 == 0)
        {
            constructor.distance -= 0.1;
            if (constructor.distance >= 1)
                constructor.distanceText.text = "남은거리 " + constructor.distance.ToString("#.#") + "km";
            else
            {
                if (constructor.distance >= 0)
                {
                    constructor.distanceText.text = "남은거리 " + constructor.distance.ToString("0.#") + "km";

                }
                else
                {
                    constructor.distanceText.text = "남은거리 " + "0km";
                    GetComponent<Animator>().SetTrigger("gameClear");
                    Destroy(gameObject, 0.7f);
                    constructor.clapSound.GetComponent<AudioSource>().Play();
                    constructor.gameState.GetComponent<Text>().text = " GAME CLEAR! ";
                    constructor.gameState.SetActive(true);
                    pitchGauge.SetActive(false);
                }
            }
        }

        // 캐릭터가 번개에 맞아 감전될 때 동작컨트롤 

        if (power == 0f)
        {
            if (++stopFrame % 130 == 0)
            {
                power = 5f;
                stopFrame = 0;
            }
            else
            {
                GetComponent<Animator>().SetTrigger("electricShock");
            }
        }

      // 핸드폰에 인식된 목소리크기값 조작 & 캐릭터 이동동작과 연동 
     
        pitchValue = PitchDetector.MicLoudness * 100;
        pitchSum += pitchValue;

        if (++pitchFrame % 30 == 0)
        {
            float avg = pitchSum / 30;

            if (avg < 0.01f)
            {
                pitchGauge.GetComponent<Image>().fillAmount = 0.1f;
            }
            else if (avg > 0.1 && avg < 5)
            {
                pitchGauge.GetComponent<Image>().fillAmount = 0.4f;
                GetComponent<Rigidbody2D>().AddForce(Vector2.down * power);
            }
            else if (avg > 5 && avg < 60)
            {
                pitchGauge.GetComponent<Image>().fillAmount = 0.7f;
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * power);
            }
            else if (avg >=60)
            {
                pitchGauge.GetComponent<Image>().fillAmount = 1f;
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * (power + 5f));
            }
            pitchSum = 0;
        }
    }//End of Update()


    // 장애물과 충돌했을 때 동작 컨트롤 함수 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 번개와 충돌했을 때

        if (collision.gameObject.tag.Equals("lightning"))
        {
            power = 0f;
            collision.gameObject.GetComponent<AudioSource>().Play();
        }

        // 세균과 충돌했을 때

        else if (collision.gameObject.tag.Equals("virus") || collision.gameObject.tag.Equals("virus1") || collision.gameObject.tag.Equals("virus2"))
        {
            // 생명(치즈)가 있을 때
            if (constructor.cheeseScore > 0)
            {
                if (collision.gameObject.GetComponent<Move>().enabled)
                    collision.gameObject.GetComponent<Move>().enabled = false;
                collision.gameObject.GetComponent<coinDestroy>().destroy();
                constructor.cheeseScore--;
                constructor.cheeseText.text = "x " + constructor.cheeseScore.ToString();
            }
            // 생명(치즈)가 없을 때
            else
            {
                GetComponent<AudioSource>().Play();
                GetComponent<Animator>().SetTrigger("cellDie");
                Destroy(gameObject, 0.5f);
                constructor.gameState.GetComponent<Text>().text = " PEACH DIE :( ";
                constructor.gameState.SetActive(true);
                pitchGauge.SetActive(false);
            }
        }
    }//End of onCollisionEnter2D()


    // 동전/생명(치즈)와 충돌했을 때 동작컨트롤 함수 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 물체가 움직이고있다면 정지하도록 

        if (collision.GetComponent<Move>().enabled)
        {
            collision.GetComponent<Move>().enabled = false;

            // 동전이 동색일 때 동작 
            if (collision.transform.tag.Equals("bronzeCoin"))
            {
                constructor.coinScore += 100;
                constructor.coinText.text = constructor.coinScore.ToString() + "$";
                collision.GetComponent<coinDestroy>().destroy();
            }
            // 동전이 은색일 때 동작
            else if (collision.transform.tag.Equals("silverCoin"))
            {
                constructor.coinScore += 200;
                constructor.coinText.text = constructor.coinScore.ToString() + "$";
                collision.GetComponent<coinDestroy>().destroy();
            }
            // 동전이 금색일 때 동작 
            else if (collision.transform.tag.Equals("goldCoin"))
            {
                constructor.coinScore += 300;
                constructor.coinText.text = constructor.coinScore.ToString() + "$";
                collision.GetComponent<coinDestroy>().destroy();
            }
            // 치즈와 충돌할 때 동작 
            else if (collision.transform.tag.Equals("cheese"))
            {
                constructor.cheeseScore++;
                constructor.cheeseText.text = "x " + constructor.cheeseScore.ToString();
                collision.GetComponent<coinDestroy>().destroy();
            }
        }
    }//End of onTriggerEnter2D()
}//End of charController
