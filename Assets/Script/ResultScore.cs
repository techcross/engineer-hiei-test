using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    private GameObject obj;
    private Master master;
    public Text score = null;
    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("Master");
        master = obj.GetComponent<Master>();
        var count = master.resultCount;
        score.text = count + "人抜き";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
