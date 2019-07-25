using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 모바일프로그래밍 유니티 게임
 * 작성일: 2018.05.11
 * 작성자: 강준모 
 */


 // 버튼실행을 관리하는 클래스

public class ButtonController : MonoBehaviour {

    //게임실행버튼함수
   public void startBtn()
    {
        SceneManager.LoadScene("Ingame");
    }//End of startBtn()
    
    //게임반복버튼함수
    public void replayBtn()
    {
        SceneManager.LoadScene("Ingame");
    } //End of replayBtn()

    //뒤로가기버튼함수
    public void backBtn()
    {
        SceneManager.LoadScene("Start");
    }//End of backBtn()

    //게임종료버튼함수
    public void quitBtn()
    {
        Application.Quit();
    }//End of quitBtn()
}//End of ButtonController
