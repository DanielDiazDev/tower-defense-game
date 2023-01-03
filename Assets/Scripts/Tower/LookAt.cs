using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform[] enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var enemies in enemy)
        {
            Vector2 direction = enemies.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        }
    }
}
