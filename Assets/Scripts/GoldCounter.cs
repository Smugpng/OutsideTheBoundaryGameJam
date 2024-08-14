using TMPro;
using UnityEngine;

public class GoldCounter : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float goldCount = PlayerPrefs.GetFloat("gold");
        textMeshProUGUI.SetText("Gold Needed " + goldCount + "/5000");
    }
}