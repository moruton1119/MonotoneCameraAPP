using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string Scene1;
    public string Scene2;
    public string Scene3;

    public void OnLoadScene1()
    {
        SceneManager.LoadScene(Scene1);
    }
    public void OnLoadScene2()
    {
        SceneManager.LoadScene(Scene2);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
