using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleClickerButton : MonoBehaviour {
    private ObstacleClicker parent;
    private void Start()
    {
        parent = gameObject.transform.parent.gameObject.GetComponent<ObstacleClicker>();
    }
    private void OnMouseDown()
    {
        parent.ButtonClicked();
    }
}
