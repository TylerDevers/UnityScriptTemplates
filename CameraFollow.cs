using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
    marioBros-like camera follow. No back scroll. Tracks players game object, which is attached to script in editor.
*/

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField] Transform player;
    new Camera camera;
    
    float cameraXposition;

    private void Awake() {
        camera = Camera.main;
        cameraXposition = player.position.x;
    }


    private void LateUpdate() {
        NoBackScroll();
    }


    void NoBackScroll() {
        Vector3 cameraPos = camera.transform.position;

        cameraPos.x = Mathf.Max(cameraPos.x, player.position.x);
        camera.transform.position = cameraPos;
    }

}
