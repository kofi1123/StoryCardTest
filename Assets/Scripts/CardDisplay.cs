using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using KuroProductions;

public class CardDisplay : MonoBehaviour
{

    public Card cardData;
    public Image cardImage;
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public Image[] typeImages;

    private Color[] cardColors = {
        new Color(0.4f, 0f, 0f),    // Fire
        new Color(0.8f, 0.5f, 0.2f), // Earth
        new Color(0f, 0f, 0.5f),   // Wate
        new Color(0f, 0.6f, 0.6f),   // Air
        new Color(0.5f, 0.5f, 0f), // Light
        new Color(0.2f, 0f, 0f)  // Dark
    };

    private Color[] typeColors = {
        Color.red,    // Fire
        new Color(0.8f, 0.2f, 0.2f), // Earth
        Color.blue,   // Water
        Color.cyan,   // Air
        Color.yellow, // Light
        new Color(0.2f, 0f, 0.4f)  // Dark
    };

    void Start()
    {
        UpdateCardDisplay();
    }
    
    public void UpdateCardDisplay()
    {
        if (cardData == null) return;

        //Update the main card image color based on the first card type
        if (cardData.cardType.Count > 0)
        {
            cardImage.color = cardColors[(int)cardData.cardType[0]];
        }
        else
        {
            cardImage.color = Color.white; // Default color if no type is set
        }

        nameText.text = cardData.cardName;
        healthText.text = cardData.health.ToString();
        damageText.text = $"{cardData.damageMin} - {cardData.damageMax}";

        for (int i = 0; i < typeImages.Length; i++)
        {
            if (i < cardData.cardType.Count)
            {
                typeImages[i].gameObject.SetActive(true);
                typeImages[i].color = typeColors[(int)cardData.cardType[i]];
            }
            else
            {
                typeImages[i].gameObject.SetActive(false);
            }
        }
    }
}
