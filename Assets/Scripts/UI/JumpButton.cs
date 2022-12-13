using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour
{

    private PlayerMovement player;
    private bool pressed;
    private float pointerDownTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if(pressed)
        {
            pointerDownTimer += Time.deltaTime;
        }
    }

    public void pointerUp()
    {
        pressed = false;
        player.isPressing= false;
    }

    public void pointerDown()
    {
        pressed = true;
        player.isPressing = true;
        player.jump();
    }
}
