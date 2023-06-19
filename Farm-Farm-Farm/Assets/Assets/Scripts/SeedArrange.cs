using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SeedArrange : MonoBehaviour
{
    public int seedsCount;
    [SerializeField] private int seedsStartCount;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        seedsCount = seedsStartCount;
        text.text = "Semen: " + seedsCount.ToString();
    }

    public void Increase()
    {
        seedsCount += 1;
        text.text = "Semen: " + seedsCount.ToString();
    }

    public void Decrease()
    {
        seedsCount -= 1;
        text.text = "Semen: " + seedsCount.ToString();
    }
}
