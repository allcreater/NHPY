using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindowsManager : MonoBehaviour
{
    public GameObject dialogPanel;
    public GameObject menuPanel;

    public Text messageTextBox;
    public Image messageImage;

    public void SetMessage(string message)
    {
        messageTextBox.text = message.Replace("<br>", "\n");
        dialogPanel.SetActive(true);
    }

    public void SetImage(string imageName)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            menuPanel.SetActive(!menuPanel.activeInHierarchy);

        //Auto pause when some of children is visible
        Time.timeScale = transform.GetChildren().Any(x => x.gameObject.activeInHierarchy) ? 0.0f : 1.0f;
    }
}
