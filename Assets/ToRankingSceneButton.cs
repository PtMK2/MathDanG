using UnityEngine;
using UnityEngine.SceneManagement;
public class ToGameSceneButton : MonoBehaviour
{
    public void OnClickToGameSceneButton()
    {
        SceneManager.LoadScene("Ranking");
    }
}