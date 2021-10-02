using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public int score;
    //public int score1;
    // 生成障碍物点列表
    public List<GameObject> bornPosList = new List<GameObject>();
    //生成得分物点列表（雪花）
    public List<GameObject> bornPosList2 = new List<GameObject>();
    // 道路列表
    public List<GameObject> roadList = new List<GameObject>();
    // 抵达点列表
    public List<GameObject> arrivePosList = new List<GameObject>();
    // 障碍物列表
    public List<GameObject> objPrefabList = new List<GameObject>();
    //得分物列表（雪花\火炬）
    public List<GameObject> objPrefabList2 = new List<GameObject>();

    public GameObject Obj;
    //雪花预制体
    public GameObject Snow;

    // 目前的障碍物
    Dictionary<string, List<GameObject>> objDict = new Dictionary<string, List<GameObject>>();
    Dictionary<string, List<GameObject>> objDict2 = new Dictionary<string, List<GameObject>>();
    
    public int roadDistance;
    public bool start = false;
    public bool isEnd = false;
    public bool ifFound = false;
    // Use this for initialization
    void Start () {
        //foreach(GameObject road in roadList)
        //{
        //    List<GameObject> objList = new List<GameObject>();
        //    objDict.Add(road.name, objList);
        //}
        //initRoad(0);
        //initRoad(1);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.N))
        {

          
        }
    }

    public void init()
	{
        foreach (GameObject road in roadList)
        {
            List<GameObject> objList = new List<GameObject>();
            objDict.Add(road.name, objList);
        }
        initRoad(0);
        initRoad(1);
    }
    // 切出新的道路
    /*
     *已修改，可以正确无限循环延长道路 by wxy
     */
    public void changeRoad(GameObject arrivePos)
    {
        //当前
        int index = arrivePosList.IndexOf(arrivePos);
        if(index >= 0)
        {
            int lastIndex = index - 1;
            int nextIndex = index + 1;
            if (lastIndex < 0)
                lastIndex = roadList.Count - 1;
            if (nextIndex>2)
			{
                nextIndex = 0;
			}
            // 移动道路
            roadList[lastIndex].transform.position = roadList[nextIndex].transform.position + roadList[nextIndex].transform.right * roadDistance;
            initRoad(lastIndex);
        }
        else
        {
            Debug.LogError("arrivePos index is error");
            return;
        }
    }

    /*
     * 已修改
     */
    void initRoad(int index)
    {
        
        string roadName = roadList[index].transform.name;
        // 清空已有障碍物
        foreach(GameObject obji in objDict[roadName])
        {
            Destroy(obji);
        }
        //foreach (GameObject obji in objDict2[roadName])
        //{
        //    Destroy(obji);
        //}
        //objDict2[roadName].Clear();

        objDict[roadName].Clear();
        

        // 添加障碍物
        foreach (Transform pos in bornPosList[index].transform)
        {
            GameObject prefab = objPrefabList[Random.Range(0, objPrefabList.Count)];

			Vector3 eulerAngle = new Vector3(pos.eulerAngles.x, pos.eulerAngles.y, pos.eulerAngles.z);
			GameObject obj = Instantiate(prefab, pos.position, Quaternion.Euler(eulerAngle),roadList[index].transform);
            Quaternion rotU = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
            obj.transform.localRotation = obj.transform.localRotation * rotU;
            obj.tag = "Obstacle";
            objDict[roadName].Add(obj);
        }

       


        //添加得分物
        foreach (Transform pos in bornPosList2[index].transform)
        {
            GameObject obj;
           // Quaternion rotU;

            GameObject prefab = objPrefabList2[Random.Range(0, objPrefabList2.Count)];

            
             Vector3 eulerAngle = new Vector3(pos.eulerAngles.x, pos.eulerAngles.y, pos.eulerAngles.z);
            
            /*
             * Vector3 location = new Vector3(pos.position.x, pos.position.y, pos.position.z-50);
            GameObject obj = Instantiate(prefab, location, Quaternion.Euler(eulerAngle), roadList[index].transform);
            Quaternion rotU = Quaternion.AngleAxis(90, Vector3.up);
            obj.transform.localRotation = obj.transform.localRotation * rotU;
           */
            //雪花
            if (prefab == objPrefabList2[0])
            {
            
                Debug.Log("snowflakes");
                Vector3 location = new Vector3(pos.position.x, pos.position.y, pos.position.z );
                obj = Instantiate(prefab, location, Quaternion.identity, roadList[index].transform);
              //  obj = Instantiate(prefab, location, Quaternion.Euler(eulerAngle));
               // rotU = Quaternion.AngleAxis(90, new Vector3(0,0,1));
               // obj.transform.localRotation = obj.transform.localRotation * rotU;
                //   obj = Instantiate(prefab, location, Quaternion.identity);
               
             //   obj.tag = "snowflakes";
                objDict2[roadName].Add(obj);
            }
            //火炬
            else if(prefab == objPrefabList2[1])
            {
                Debug.Log("torch");
                Vector3 location = new Vector3(pos.position.x, pos.position.y, pos.position.z);
                // obj = Instantiate(prefab, pos.position, Quaternion.Euler(eulerAngle), roadList[index].transform);\
                obj = Instantiate(prefab, location, Quaternion.identity, roadList[index].transform);
                //obj.tag = "torch";
                objDict2[roadName].Add(obj);
            }
           
           
        }
       
    }
}
