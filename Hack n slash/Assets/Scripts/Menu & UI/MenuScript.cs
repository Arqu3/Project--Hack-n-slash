using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Light menuLight;
    Color lerpedColor;
    float colorTimer;
    bool direction;

    Animator animator;
    bool isAnimation = false;

    void Start()
    {
        direction = false;
        colorTimer = 0f;
        animator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        ColorTransition();
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

        menuLight.color = lerpedColor;
    }

    public void Loadlevel()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayAnimation()
    {
        isAnimation = !isAnimation;
        if (isAnimation)
            animator.Play("MoveCamera");
        else if (!isAnimation)
            animator.Play("MoveCameraBack");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
