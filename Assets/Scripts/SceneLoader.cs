using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int sceneid = 5;
    
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneid); 
        
    }

}
