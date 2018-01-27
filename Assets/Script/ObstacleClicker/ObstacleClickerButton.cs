using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleClickerButton : MonoBehaviour {
    public ObstacleClicker parent;
    private void Start()
    {
        parent = gameObject.transform.parent.gameObject.GetComponent<ObstacleClicker>();
    }
    private void OnMouseDown()
    {
        Debug.Log("click");
        parent.ButtonClicked();
    }

    private void OnMouseEnter()
    {
        Debug.Log("dsds");
    }
}
