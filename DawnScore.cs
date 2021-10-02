using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DawnScore : MonoBehaviour
{
    public GameObject player;
    public float timerLinmit = 0;
    public float a = 0;
    
    public float speed = 2;//分数移动速度
    public float color_a = 255;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        GetComponent<Text>().text ="+"+player.GetComponent<playerController>().CurrentScore.ToString();
        a = 0;
        StartCoroutine(ScoreUp());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartIE()
    {
       
       
    }
    IEnumerator ScoreUp()
    {
      
        while (timerLinmit < 1.5)
        {
            yield return new WaitForSeconds(0.05f);
            a += speed;
            //文本渐隐
            GetComponent<Text>().color = new Color(255 / 255f, 0, 0, color_a / 255f);
            color_a -= 10;       
            transform.localPosition = new Vector3(-28, a, 0);
            timerLinmit += 0.05f;
        }
        StopCoroutine(ScoreUp());
        //移动结束后销毁
        Destroy(gameObject);
    }
}
