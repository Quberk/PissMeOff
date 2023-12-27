using UnityEngine;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] private Animator caseFileAnim;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    // Start is called before the first frame update
    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CaseFileOpened()
    {
        caseFileAnim.Play("Opened", -1, 0);
    }

    public void CaseFileClosed()
    {
        caseFileAnim.Play("Close", -1, 0);
    }

    public void Winning()
    {
        winPanel.SetActive(true);
    }

    public void Losing()
    {
        losePanel.SetActive(true);
    }
}
