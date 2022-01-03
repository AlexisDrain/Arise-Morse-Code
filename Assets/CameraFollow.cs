using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{

    public Vector2 cameraBounds = new Vector2(0, 0);
    public Vector2 offset = new Vector2(0, 0);

    private Vector2 cameraCoords = new Vector2(0, 0);
    private Text levelnameText;

    void Start()
    {
        levelnameText = GameObject.Find("LevelName").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.playerTransform.position.x > cameraBounds.x + transform.position.x + offset.x) {
            transform.position = new Vector3(transform.position.x + cameraBounds.x * 2, 0f, transform.position.y);

            cameraCoords = new Vector2(cameraCoords.x + 1, cameraCoords.y);
            levelnameText.text = "x" + cameraCoords.x + "y" + cameraCoords.y;
        } else if (GameManager.playerTransform.position.x < -cameraBounds.x + transform.position.x - offset.x) {
            transform.position = new Vector3(transform.position.x - cameraBounds.x * 2, 0f, transform.position.z);

            cameraCoords = new Vector2(cameraCoords.x - 1, cameraCoords.y);
            levelnameText.text = "x" + cameraCoords.x + "y" + cameraCoords.y;
        // } else if (GameManager.playerTransform.position.z > cameraBounds.y + transform.position.y + offset.y) {
            //transform.position = new Vector3(transform.position.x, transform.position.y + cameraBounds.y * 2 + offset.y);

          //  cameraCoords = new Vector2(cameraCoords.x, cameraCoords.y + 1);
           // levelnameText.text = "x" + cameraCoords.x + "y" + cameraCoords.y;
        } else if (GameManager.playerTransform.position.z < -cameraBounds.y + transform.position.z + offset.y) {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z - cameraBounds.y * 2);

            cameraCoords = new Vector2(cameraCoords.x, cameraCoords.y - 1);
            levelnameText.text = "x" + cameraCoords.x + "y" + cameraCoords.y;
            print(levelnameText.text);
        }
    }
}
