using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public float smooth = 0.3f;
    public float height;
    public float distance;

    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = player.position.z - distance;
        pos.y = player.position.y + height;

        // transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
        transform.position = pos;
        transform.position = new Vector3 (transform.position.x, transform.position.y + 5, transform.position.z);

        transform.LookAt(player.transform);

    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    private void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (target == null) {
            Debug.LogWarning("Missing target ref !", this);
            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)        {
            transform.position = target.TransformPoint(offsetPosition);
        }         else         {
            transform.position = target.position + offsetPosition;
        }

        // compute rotation
        if (lookAt)         {
            transform.LookAt(target);         }
        else         {
                        transform.rotation = target.rotation;
        }
    }
}
*/