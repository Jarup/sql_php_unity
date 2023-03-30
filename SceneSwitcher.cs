using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    public void LoadNewUserScene()
    {

        SceneManager.LoadScene(0);

    }


    public void LoadWelcomeScene()
    {

        SceneManager.LoadScene(1); // all scenes have id number and have to be set manualy
                                   // in Build folder to get an id number.
                                   // GameObject with this script is then given to button gameobject and function is set to button runtime on click()

    }

    public void LoadLoginScene()
    {

        SceneManager.LoadScene(2);

    }


   

  

}
