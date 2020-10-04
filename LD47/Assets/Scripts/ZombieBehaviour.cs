using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEditor.Animations;

public class ZombieBehaviour : MonoBehaviour
{
    // If there are multiple sprite
    public bool hasSpriteVariation = false;

    public Sprite[] sprites = new Sprite[0];
    public AnimatorController[] animatorControllers = new AnimatorController[0];

    // Big boy has a specific animation
    public bool isBigBoy = false;


    [HideInInspector]
    public Monster monsterInfo;


    public SpriteRenderer spriteRenderer;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isBigBoy || IsJumping(spriteRenderer.sprite.name))
        {
            transform.position -= new Vector3(monsterInfo.speed, 0f, 0f) * Time.deltaTime;
        }
    }

    private bool IsJumping(string spriteName)
    {
        return !(spriteName == "big-boy_0" || spriteName == "big-boy_1" || spriteName == "big-boy_2" || spriteName == "big-boy_3");
    }
}
