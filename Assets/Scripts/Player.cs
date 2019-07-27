using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public float movementSpeed;
    // public GameObject camera;
    public GameObject bulletSpawnPoint;
    public GameObject bullet;
    public float bulletSpeed;
    public int healthPoint;
    public GameObject HPBar;

    private bool isGameOver = false;
    // public GameObject playerGo;
    public Material grayMaterial;

    void Update()
    {
        if (isGameOver) return;

        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if (playerPlane.Raycast(ray, out hitDist)) {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0)) { // Left
            Cmd_Shoot();
        }
    }
    public void changeHealthPoint(int delta){
        if (isGameOver) return;

        healthPoint += delta;
        HPBar.transform.localScale = new Vector3(0.2f, 0.1f, 2.0f/100*healthPoint );
        if (healthPoint <= 0) {
            isGameOver = true;
        }
    }
    void FixedUpdate() {
        if (isGameOver) return;

        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        transform.position = transform.position + movement * movementSpeed * Time.fixedDeltaTime;

    }

    [Command]
    void Cmd_Shoot() {
        // Transform clone;
        // clone = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
        // clone.rotation = bulletSpawnPoint.transform.rotation;
        // clone.GetComponent<Rigidbody>().AddForce(clone.transform.forward * bulletSpeed);

        Rpc_Shoot();
    }

    [ClientRpc]
    void Rpc_Shoot() {
        // if (isLocalPlayer) return;

        GameObject clone = Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);

        // print("Rigidbody:" + clone.GetComponent<Rigidbody>());
        // print("bulletSpawnPoint.transform.position: " + bulletSpawnPoint.transform.position);
        // print("bulletSpawnPoint.transform.rotation: " + bulletSpawnPoint.transform.rotation);

        clone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        NetworkServer.Spawn(clone);
    }
}
