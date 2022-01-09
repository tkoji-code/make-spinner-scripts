using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!gameSession.GetIsGameStart())
        {
            gameSession.SetIsGameStart(true);
        }
    }
}
