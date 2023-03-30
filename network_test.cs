using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class network_test : MonoBehaviour
{

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(testing());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator testing()
    {
        UnityWebRequest webrequest = UnityWebRequest.Get("http://localhost/cruds/newplayer.php");
        yield return webrequest.SendWebRequest();
        text.text = webrequest.downloadHandler.text;


    }

}
