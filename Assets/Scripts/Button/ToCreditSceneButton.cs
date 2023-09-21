using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToCreditSceneButton : MonoBehaviour
{
    public void OnClickToCreditSceneButton()
    {
        Initiate.Fade("Credit", Color.black, 8f);
        //SceneManager.LoadScene("Credit");
    }
}