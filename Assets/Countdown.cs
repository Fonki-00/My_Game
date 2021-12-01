using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour
{
    public Text timerText;
    public int time = 60;
    public bool timerSubtrack = false;
    void Start()
    {
    }

    void Update()
    {
        if (timerSubtrack == false && time > 0)
        {
            StartCoroutine(TimeTicker());
        }
    }
    IEnumerator TimeTicker()
    {
        timerSubtrack = true;
        yield return new WaitForSeconds(1);
        time--;
        timerText.text = time.ToString();
        timerSubtrack = false;
    }
}
