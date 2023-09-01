using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCredit : MonoBehaviour
{
   public void OnClickToCreditSceneButton()
   {
        SceneManager.LoadScene("CreditScene");
   }
}
