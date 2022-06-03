using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(.0f, 99999.0f)] public float health = 100.0f;
    [Range(.0f, 10000.0f)] public float damage = 4.0f;

    [Range(0.5f, 3.0f)][SerializeField] float m_speed = 0.5f;
    
    [SerializeField] float acceleration = 4.0f;
    
    private CharacterController c_controller;
    private float p_input;
    public bool jump = false;
    public bool attack = false;
    public bool dead = false;

    private void Start()
    {
        c_controller = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        p_input = Input.GetAxis("Horizontal") * m_speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            attack = true;
        }

        if(health <= 0) {
            dead = true;
        }
    }

    private void FixedUpdate()
    {
        c_controller.Move(p_input, acceleration, jump, attack, dead);

        if (attack)
        {
            attack = false;
        }

        if (jump)
        {
            jump = false;   
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Enemy"))
            health -= 15f;
    }
}
