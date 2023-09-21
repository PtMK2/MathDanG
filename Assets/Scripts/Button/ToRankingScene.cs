using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToRankingScene : MonoBehaviour
{
    public void OnClickToGameSceneButton()
    {
        Initiate.Fade("Ranking", Color.black, 8f);
        //SceneManager.LoadScene("Ranking");
    }
}