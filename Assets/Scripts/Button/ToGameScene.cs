using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToGameScene : MonoBehaviour
{
    public void OnClickToGameSceneButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}