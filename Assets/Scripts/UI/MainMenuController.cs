using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private GameObject creditPanel;

    // Start is called before the first frame update
    void Start()
    {
        howToPlayPanel.SetActive(false);
        creditPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameBtn()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void HowToPlayBtn()
    {
        howToPlayPanel.SetActive(true);
    }

    public void CreditBtn()
    {
        creditPanel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
