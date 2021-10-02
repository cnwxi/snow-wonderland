using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    
   // public Text totalscore1;
    public Text totalscore;
    public GameManager gameManager;
    public float moveSpeed;
    public float jumpSpeed;
    Animator animator;

 

    //临时得分
    public int CurrentScore;


    //
    GameObject m_obj;
    GameObject m_obj1;
    private Vector2 startFingerPos;
    private Vector2 nowFingerPos;
    private float h1 = 0;
    private float timer = 0.0f;
    private float offsetTime = 0.01f;
    //

    float moveHSpeed;   // 水平移动速度
    // Start is called before the first frame update
    void Start()
    {

        //
        m_obj = GameObject.Find("button");  //“重新开始”按钮
        m_obj1 = GameObject.Find("button1");  //"结束"按钮
        //


        moveHSpeed = moveSpeed;
        GetComponent<Rigidbody>().freezeRotation = true;
        animator = GetComponent<Animator>();
        if (animator == null)
            print("null");
    }

    void FixedUpdate()
    {
        if (gameManager.ifFound)//实现重力
        {
            Rigidbody rigidbody = this.GetComponent<Rigidbody>();
            rigidbody.AddForce(this.transform.up * -9.81f);
        }
    }
    /*
     * start\isEnd
     * 0        0   初始状态 没有开始
     * 1        0   开始游戏
     * 0        1   结束游戏
     * 1        1   暂停游戏
     */
    // Update is called once per frame
    void Update()
    {
        if (gameManager.start && !gameManager.isEnd && gameManager.ifFound)//游戏开始并且识别图被扫描到
        {
            //this.transform.GetComponent<Collider>().enabled = true;
            // 获得键盘输入,左右方向键
            float h = Input.GetAxis("Horizontal");
            h1 = 0;
            //获得触摸屏输入
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)//手指按下
                {
                    timer = 0;
                    startFingerPos = Input.GetTouch(0).position;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved)//划动
                {
                    timer += Time.deltaTime;
                    if (timer > offsetTime)
                    {
                        nowFingerPos = Input.GetTouch(0).position;
                        if (nowFingerPos.x < startFingerPos.x)
                        {
                            h1 = -1;
                        }
                        else if (nowFingerPos.x > startFingerPos.x)
                        {
                            h1 = 1;
                        }
                        startFingerPos = nowFingerPos;
                        timer = 0;
                    }
                }
            }
            Debug.Log(h);
            Debug.Log(h1);
            Vector3 hSpeed = new Vector3(this.transform.right.x, this.transform.right.y, this.transform.right.z) * moveHSpeed * (h + h1);
            //移动
            this.transform.position += (hSpeed * Time.deltaTime);
        }
        else
        {
            if (gameManager.start && !gameManager.isEnd && !gameManager.ifFound)
            {
                gameManager.isEnd = true;//进入暂停状态
            }
            /*
             * (如果有ui界面需要修改)
             */
            //按下空格\点击屏幕开始游戏
            if (Input.GetKey(KeyCode.Space) || Input.touchCount > 0 && gameManager.ifFound)
            {
                Debug.Log("用户有输入");
                if (!gameManager.start && gameManager.isEnd)//0 1 游戏结束状态,重载场景
                {
                    SceneManager.LoadScene("gameScene");
                    // m_obj.SetActive(true);

                }
                else if (gameManager.start && gameManager.isEnd)//1 1暂停状态,取消暂停
                {
                    gameManager.isEnd = false;
                    //  m_obj.SetActive(false);
                }
                else if (!gameManager.start && !gameManager.isEnd)//初始状态,进入开始游戏状态,初始化
                {
                    gameManager.start = true;
                    m_obj.SetActive(false);  //隐藏 button
                    m_obj1.SetActive(false);  //隐藏button1
                    gameManager.init();



                }
            }
        }
    }
    //得分
    void OnGUI()//简单的UI提示,需要替换
    {
        if (gameManager.ifFound)
        {
            if (!gameManager.start && gameManager.isEnd)
            {
                GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter;
                style.fontSize = 20;
                style.normal.textColor = Color.red;
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "你输了,点击“重新开始”按钮，重新开始游戏,点击“结束”按钮，结束游戏", style);
                m_obj.SetActive(true);  //显示button
                m_obj1.SetActive(true);  //显示button1

            }
            if (!gameManager.start && !gameManager.isEnd)
            {
                GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter;
                style.fontSize = 40;
                style.normal.textColor = Color.red;
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "点击屏幕或按下空格键开始", style);
                m_obj.SetActive(true);
                m_obj1.SetActive(true);
            }
            if (gameManager.start && gameManager.isEnd)
            {
                GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter;
                style.fontSize = 40;
                style.normal.textColor = Color.red;
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "点击屏幕或按下空格键继续", style);
                m_obj.SetActive(false);
                m_obj1.SetActive(false);
            }
        }
        else
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 40;
            style.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "请将摄像头对准识别图", style);
        }
    }

    #region 计分模块
    //UI画布
    public GameObject UI_Score;
    //Text预制体
    public GameObject Text_GetScore;
    public void GetScore(int score)
    {
        gameManager.score += score;
        CurrentScore = score;
        //播放得分动画
        GameObject prefablnstance = Instantiate(Text_GetScore);
        prefablnstance.transform.parent = UI_Score.transform;
        prefablnstance.transform.localPosition = new Vector2(-28,0);
        updatescore();
    }

    void updatescore()
    {
        //totalscore1.text = gameManager.score.ToString();
        
        totalscore.text = gameManager.score.ToString();
    }

    #endregion
    void OnTriggerEnter(Collider other)
    {
        // 如果是抵达点
        if (other.name.Equals("ArrivePos"))
        {
            Debug.Log("ArrivePos");
            gameManager.changeRoad(other.gameObject);
        }
        // 如果是透明墙
        else if (other.tag.Equals("AlphaWall"))
        {
            Debug.Log("AlphaWall");
            // 没啥事情
        }
        // 如果是障碍物,结束游戏
        // 如果不生效,记得修改预制体标签
        else if (other.tag.Equals("Obstacle"))
        {
            Debug.Log("Obstacle");
            gameManager.start = false;
            gameManager.isEnd = true;
        }
        else if (other.tag.Equals("snowflakes"))
        {
            GetScore(1);
            Debug.Log("触碰雪花");
           // gameManager.score= gameManager.score+1;
            
           // updatescore();
        }
        else if (other.tag.Equals("torch"))
        {

            //   gameManager.score = gameManager.score+ 5;
            GetScore(5);
            Debug.Log("触碰火炬");
           // updatescore();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("snowflakes"))
        {
            
            Destroy(other.gameObject);
            
        }
        else if (other.tag.Equals("torch"))
        {
            Destroy(other.transform.gameObject);
        }
    }
}
