using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Light light;
    Color lerpedColor;
    float colorTimer;
    bool direction;

    Animator animator;
    bool animation;

    void Start()
    {
        animation = false;
        direction = false;
        colorTimer = 0f;
        animator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
    }

    void Update()
    {
        ColorTransition();
    }

    void OnLevelWasLoaded(int level)
    {
        //Sets timescale when loading menu
        if (level == 0)
            Time.timeScale = 1.0f;
    }

    void ColorTransition()
    {
        colorTimer += Time.deltaTime / 4;

        if (direction)
            lerpedColor = Color.Lerp(Color.blue, Color.red, colorTimer);
        else
            lerpedColor = Color.Lerp(Color.red, Color.blue, colorTimer);

        if (colorTimer > 1)
        {
            colorTimer = 0;
            direction = !direction;
        }

        light.color = lerpedColor;
    }

    public void Loadlevel()
    {
        Application.LoadLevel(1);
    }

    public void PlayAnimation()
    {
        animation = !animation;
        if (animation)
            animator.Play("MoveCamera");
        else if (!animation)
            animator.Play("MoveCameraBack");
    }
}
