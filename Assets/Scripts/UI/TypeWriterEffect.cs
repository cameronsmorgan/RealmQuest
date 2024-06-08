using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    public Text textComponent; // Assign this in the Inspector
    public string fullText; // The full text to display
    public float delay = 0.1f; // Delay between each character

    private string currentText = "";

    private void Start()
    {
        // Ensure the text component starts empty
        textComponent.text = "";
        StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
