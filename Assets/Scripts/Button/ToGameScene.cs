using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToGameScene : MonoBehaviour
{
    public AudioClip sound1;
    AudioSource audioSource;
    void Start () 
    {
    //Componentを取得
    audioSource = GetComponent<AudioSource>();
    }
    public void OnClickToGameSceneButton()
    {
        audioSource.PlayOneShot(sound1);
        Initiate.Fade("GameScene", Color.black, 6f);
        //SceneManager.LoadScene("GameScene");
    }
}