using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
   public void CloseGame()
    {
        Debug.Log("Has Quit");
        Application.Quit();
    }
}
  
