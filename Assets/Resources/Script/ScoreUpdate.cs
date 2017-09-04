using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour {

    Text scoreLabel;

    // Use this for initialization
    void Awake()
    {
        scoreLabel = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        scoreLabel.text = "점수 : "+ScoreManager.score.ToString();
	}
}
