using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go2Zero : MonoBehaviour
{
    public void onClick()
    {
        SceneManager.LoadScene(0);//0是要切换的场景的索引
    }
}
