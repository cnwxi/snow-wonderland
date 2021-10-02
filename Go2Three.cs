using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go2Three : MonoBehaviour
{
    public void onClick()
    {
        SceneManager.LoadScene(3);//4是要切换的场景的索引
    }
}
