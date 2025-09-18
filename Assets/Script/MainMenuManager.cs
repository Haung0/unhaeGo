using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // 게임 시작 버튼
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");  // 이동할 씬 이름
    }





    // 게임 방법 버튼
    public void HowToPlay()
    {
        SceneManager.LoadScene("HowtoPlayScene");
    }
    public void NextHowToPlay1()
    {
        SceneManager.LoadScene("HowtoPlayScene1");
    }
    public void NextHowToPlay2()
    {
        SceneManager.LoadScene("HowtoPlayScene2");
    }

    // 종료 버튼 (원한다면)
    //public void QuitGame()
    //{
    //    Application.Quit();
    //    Debug.Log("게임 종료"); // 에디터에서는 종료 안 되니까 로그로 확인
    //}

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu"); // 메인 메뉴 씬 이름
    }
}
