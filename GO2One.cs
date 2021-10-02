using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GO2One : MonoBehaviour
{
    public void onClick()
    {
        SceneManager.LoadScene(1);//4是要切换的场景的索引
    }
}
