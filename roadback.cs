using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadback : MonoBehaviour
{
    public GameManager gameManager;
    public float backSpeed;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
		if (gameManager.start&&!gameManager.isEnd&&gameManager.ifFound)//游戏开始未结束,识别图扫描到
		{
            this.transform.position -= this.transform.right * backSpeed * Time.deltaTime;
        }
    }
}
