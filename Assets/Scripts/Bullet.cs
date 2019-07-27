using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {
    // public float speed;
    public float maxDistance;
    private GameObject triggeringObject;
    void Start() {
        // rb = GetComponent<Rigidbody>();
        // rb.AddForce(transform.forward * speed);
    }
    void Update()
    {
        // transform.Translate(Vector3.forward * Time.deltaTime * speed);

        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= 5) {
            // > 5 seconds
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other) {
        if(other.tag == "tag_Wall") {
            triggeringObject = other.gameObject;
            Destroy(this.gameObject);
        } else if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().changeHealthPoint(-10);
            Destroy(this.gameObject);
        }

    }
}
