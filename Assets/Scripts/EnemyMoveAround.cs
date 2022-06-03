using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMoveAround : MonoBehaviour
{
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;
    [SerializeField] PlayerController playerController;
    [SerializeField] Slider heathSlider;
    [SerializeField] float m_Speed = 2.0f;
    [SerializeField] float health = 50.0f;

    void Update()
    {
        if (gameObject.transform.position.x >= point2.position.x || gameObject.transform.position.x <= point1.position.x)
        {
            m_Speed = -m_Speed;
        }
        gameObject.transform.Translate(m_Speed * Vector3.right * Time.deltaTime,Space.World);

        heathSlider.value = health;

        if (playerController.attack)
        {

        }
    }

}
