using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToGameScene : MonoBehaviour
{
    public void OnClickToGameSceneButton()
    {
        Initiate.Fade("GameScene", Color.black, 6f);
        //SceneManager.LoadScene("GameScene");
    }
}