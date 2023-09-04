using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToRankingScene : MonoBehaviour
{
    public void OnClickToGameSceneButton()
    {
        SceneManager.LoadScene("Ranking");
    }
}