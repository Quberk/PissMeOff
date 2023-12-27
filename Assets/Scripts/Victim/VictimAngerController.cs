using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictimAngerController : MonoBehaviour
{
    [SerializeField] private float startAngerLevel;
    [SerializeField] private Slider angerLevelSlider;
    [SerializeField] private float minusAngerPeriodically;
    private float angerLevel;

    [SerializeField] private ParticleSystem veryAngryFx;
    private bool gameOver;

    [SerializeField] private GameplayUIController gameplayUIController;

    // Start is called before the first frame update
    void Start()
    {
        angerLevel = startAngerLevel;
    }

    // Update is called once per frame
    void Update()
    {
        angerLevel -= (minusAngerPeriodically * Time.deltaTime);
        angerLevelSlider.value = angerLevel;

        if (angerLevel >= 100 && gameOver == false)
        {
            gameOver = true;
            veryAngryFx.Play();
            StartCoroutine(Win(0.5f));

            return;
        }

        if (angerLevel <= 0 && gameOver == false)
        {
            gameOver = true;
            StartCoroutine(Lose(0.5f));

            return;
        }
    }

    public void SetTheAngerLevel(float anger)
    {
        angerLevel += anger;

    }

    IEnumerator Win(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        gameplayUIController.Winning();
    }

    IEnumerator Lose(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        gameplayUIController.Losing();
    }
}
