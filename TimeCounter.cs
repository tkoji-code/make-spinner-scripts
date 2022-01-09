using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    //config param
    [SerializeField] GameObject timeText;
    [SerializeField] float initSec = 0f;
    [SerializeField] bool isCountDown = false;
    [SerializeField] bool isCountUp = false;

    float secCounter = 0f;

    public void SetCountDown(bool isOn){ isCountDown = isOn; }
    public void SetCountUp(bool isOn){ isCountUp = isOn; }

    // Start is called before the first frame update
    public GameObject GetTimeText()
    {
        return timeText;
    }

    public float SecCounter() { return secCounter; }

    void Start()
    {
        InitSecCounter();
    }

    public void InitSecCounter()
    {
        secCounter = initSec;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountUp){ CountUp(); }
        else if (isCountDown){ CountDown(); }
    }

    private void CountUp()
    {
        secCounter += Time.deltaTime;
        UpdateTimeText();
    }


    private void CountDown()
    {
        secCounter -= Time.deltaTime;
        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        timeText.GetComponent<Text>().text = "回転時間 " + secCounter.ToString("f2") + "秒";
    }
}
