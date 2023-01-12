using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{

    [SerializeField] private float speed;

    [SerializeField] private float acceleration;

    [SerializeField] private float turnSpeed = 200f;

    private int steerValue;

    void Update()
    {
        this.speed += this.acceleration * Time.deltaTime;

        transform.Rotate(0f, this.steerValue * turnSpeed * Time.deltaTime, 0f);

        transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isObstacle = other.CompareTag("Obstacle");

        Debug.Log(isObstacle);

        if (isObstacle)
        {
            SceneManager.LoadScene("Scene_MainMenu");
        }
    }

    public void Steer(int value)
    {
        this.steerValue = value;
    }
}
