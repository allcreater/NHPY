using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoresTextUpdate : MonoBehaviour
{
    private Text text;

    public string scoresMessage => $"Осчастливлено <b>{ScoreManager.instance.scores}</b> пони!";

    void Awake()
    {
        text = GetComponent<Text>();
    }


    void Update()
    {
        text.text = scoresMessage;
    }
}
