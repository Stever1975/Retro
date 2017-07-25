using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text scoreText;
    int score=0;
   public int life = 100;

    public Slider slider;

   

    // Use this for initialization
    void Start () {
        slider.minValue = 0f;
        slider.maxValue = life;
        slider.value = slider.maxValue;
        scoreText.text = "Score: " + score;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DecrementLife(int point)
    {
        life = life - point;
        slider.value = life;// (float)life;


    }


    public void IncrementScore(int point)
    {
        score = score + point;
        scoreText.text = "Score: " + score;


    }

}
