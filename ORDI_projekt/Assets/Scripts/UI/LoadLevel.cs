using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void Back() 
    {
        SceneManager.LoadSceneAsync(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
