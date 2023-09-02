using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToCreditSceneButton : MonoBehaviour
{
    public void OnClickToCreditSceneButton()
    {
        SceneManager.LoadScene("Credit");
    }
}