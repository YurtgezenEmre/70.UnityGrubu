using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plus : MonoBehaviour
{
    public Text buYazi;

    public GameObject canvas;
    public int x,y;

    public float zaman;
    public int hiz;

    void Start()
    {
        zaman=0;

        canvas = GameObject.Find("Canvas");
        transform.SetParent(canvas.transform);

        x = Random.Range(-150, 151);
        y = Random.Range(-150, 151);

        transform.localPosition = new Vector3 (x, y, 0);
    }
    
    void Update()
    {
        zaman+= Time.deltaTime;

        if(zaman >= 1f){
            Destroy(this.gameObject);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y +Time.deltaTime * hiz, 0);

        buYazi.text ="+ "+ Game.vurusGucu;
    }
}
