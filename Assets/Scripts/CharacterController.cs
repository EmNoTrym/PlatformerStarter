using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 4f;                          
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  
	[SerializeField] private bool m_AirControl = false;                         
	[SerializeField] private LayerMask m_WhatIsGround;                          
	[SerializeField] private Transform m_GroundCheck;                           
	[SerializeField] private Transform m_CeilingCheck;                          
	
	const float k_GroundedRadius = .2f;
	private bool m_Grounded;            
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;
	private Vector3 velocity = Vector3.zero;
	private SpineControl s_control;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		s_control = gameObject.GetComponent<SpineControl>();
	}

    private void FixedUpdate()
	{
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;
		}
	}


	public void Move(float move, float acceleration, bool jump, bool attack, bool dead)
	{
        if (!dead)
        {
			if (move != 0 && !s_control.currentState.Equals("Jump") && !s_control.currentState.Equals("Attack"))
			{
				if (Mathf.Abs(move) < 1.5f)
				{
					s_control.SetCharacterState("Walk");
				}

				else if (Mathf.Abs(move) >= 1.5f)
				{
					s_control.SetCharacterState("Run");
				}
			}

			if (m_Grounded || m_AirControl)
			{
				Vector3 targetVelocity = new Vector2(move * acceleration, m_Rigidbody2D.velocity.y);
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

				if (move > 0 && !m_FacingRight)
				{
					Flip();
				}
				else if (move < 0 && m_FacingRight)
				{
					Flip();
				}
			}


			if (m_Grounded)
			{
				if (jump)
				{
					//save the previous state to return to that state when end jumping
					if (!s_control.currentState.Equals("Jump"))
					{
						s_control.previousState = s_control.currentState;
					}
					s_control.SetCharacterState("Jump");

					m_Grounded = false;
					if (s_control.currentState.Equals("Jump"))
					{
						m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
					}
				}

				if (attack)
				{
					if (!s_control.currentState.Equals("Attack"))
					{
						s_control.previousState = s_control.currentState;
					}
					s_control.SetCharacterState("Attack");
				}

			}

			if (move == 0)
			{
				if (!s_control.currentState.Equals("Jump") && !s_control.currentState.Equals("Attack"))
				{
					s_control.SetCharacterState("Idle");
				}
			}
		}
		
		else if (dead)
        {
			s_control.SetCharacterState("Dead");
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


}