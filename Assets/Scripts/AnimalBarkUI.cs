using TMPro;
using UnityEngine;

public class AnimalBarkUI : MonoBehaviour
{
    public TextMeshProUGUI barkText;
    public float displayDuration = 2f;
    private float timer = 0f;

    void Start()
    {
        barkText.text = ""; // Hide at start
    }

    public void ShowBark(string message)
    {
        barkText.text = message;
        timer = displayDuration;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                barkText.text = "";
            }
        }
    }
}

