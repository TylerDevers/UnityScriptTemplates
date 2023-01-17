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
    

    private void Awake() {
        camera = Camera.main;
    }


    private void LateUpdate() {
        FollowNoBackScroll();
    }


    void FollowNoBackScroll() {
        Vector3 cameraPos = camera.transform.position;

        cameraPos.x = Mathf.Max(cameraPos.x, player.position.x);
        camera.transform.position = cameraPos;
    }

}
