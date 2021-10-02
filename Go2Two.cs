using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go2Two : MonoBehaviour
{
    public void onClick()
    {
        SceneManager.LoadScene(2);//4是要切换的场景的索引
    }
}
