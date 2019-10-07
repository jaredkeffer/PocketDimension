using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    void Start()
    {
        Player = GameObject.Find("Creator").transform;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 14, Player.transform.position.z - 10);
    }
}
