using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    private string score;
    public void ScoreUpdate()
    {
        GameObject player = GameObject.Find("Player");
        Kill playerSocre = player.GetComponent<Kill>();
        score = playerSocre.score.ToString();
        scoreText.text = score;
    }



    void Update()
    {
        ScoreUpdate();
    }
}
