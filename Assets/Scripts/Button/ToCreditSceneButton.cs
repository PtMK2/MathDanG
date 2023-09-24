using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToCreditSceneButton : MonoBehaviour
{
    public AudioClip sound1;
    AudioSource audioSource;
    void Start () 
    {
    //Componentを取得
    audioSource = GetComponent<AudioSource>();
    }
    public void OnClickToCreditSceneButton()
    {
        audioSource.PlayOneShot(sound1);
        Initiate.Fade("Credit", Color.black, 8f);
        //SceneManager.LoadScene("Credit");
    }
}