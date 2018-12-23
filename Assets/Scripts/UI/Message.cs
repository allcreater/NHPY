using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public GameObject dialogPanel;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Auto pause
        Time.timeScale = dialogPanel.activeInHierarchy ? 0.0f : 1.0f;
    }
}
