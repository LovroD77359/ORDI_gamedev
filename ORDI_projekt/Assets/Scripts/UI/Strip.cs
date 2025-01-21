using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Strip : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StripCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) { 
            Scene nextScene = SceneManager.GetActiveScene();
            SceneManager.LoadSceneAsync(nextScene.buildIndex + 1);
        }
    }

    IEnumerator StripCoroutine() 
    {
        yield return new WaitForSeconds(5);
    }
}
