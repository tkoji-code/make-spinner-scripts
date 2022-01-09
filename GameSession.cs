using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    //config param
    [SerializeField] float waitSecAfterGameOver = 3f;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject ResultTimeText;

    [SerializeField] float zeroJudge = 0f;

    //cashed reference
    GameObject timeText;
    Spinner spinner;
    TimeCounter timeCounter;
    bool isGameStart = false;

    [SerializeField] float angVel;  //for debug

    public void SetIsGameStart(bool isOn){ isGameStart = isOn; }
    public bool GetIsGameStart() { return isGameStart; }

    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;
        spinner = GameObject.Find("Koma").GetComponent<Spinner>();
        timeCounter = FindObjectOfType<TimeCounter>();
        timeText = FindObjectOfType<TimeCounter>().GetTimeText();

        if (GameObject.FindGameObjectsWithTag("Spinner Body").Length <= 0)
        {
            spinner.ReadyPosToPlay();
            spinner.SetSpinnerPhysics();  ///ここを変える必要あり
        }

        StartSpin();
    }

    private void StartSpin()
    {
        spinner.ReadyPosToPlay();
        spinner.AddTorqueToSpinner();

        timeText.SetActive(true);
        timeCounter.InitSecCounter();
        timeCounter.SetCountUp(true);
     
        GameOverUI.SetActive(false);
    }

    private void Update()
    {
        angVel = spinner.AngularVelocityY();        //for debug
        if (isGameStart & (spinner.AngularVelocityY() < zeroJudge) )
        {
            GameOver(); 
        }
    }

    public void GameOver()
    {
        isGameStart = false;
        timeCounter.SetCountUp(false);
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        //時間を点滅表示したい
        yield return new WaitForSeconds(waitSecAfterGameOver);
        timeText.SetActive(false);
        GameOverUI.SetActive(true);
        float timeSec = timeCounter.SecCounter();
        ResultTimeText.GetComponent<Text>().text = timeSec.ToString("f2") + "秒";
    }

    private static void PushRankingBoard(float timeSec)
    {
        int millsec = Mathf.RoundToInt(timeSec * 100) * 10;
        var timeScore = new System.TimeSpan(0, 0, 0, 0, millsec);
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(timeScore);
    }

    public void Ranking()
    {
        float timeSec = timeCounter.SecCounter();
        PushRankingBoard(timeSec);
    }

    //public void Replay()
    //{
    //    StartSpin();
    //}
}
