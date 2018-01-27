using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBigCanPass : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            if (!player.isFlat)
            {
                player.Die();
            }
        }
    }
}
