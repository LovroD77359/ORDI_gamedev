using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Strip : MonoBehaviour
{

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) { 
            Scene nextScene = SceneManager.GetActiveScene();
            SceneManager.LoadSceneAsync(nextScene.buildIndex + 1);
        }
    }
}
