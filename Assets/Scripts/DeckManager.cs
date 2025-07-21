using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KuroProductions;

public class DeckManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>(); // List of cards in the deck
    private int currentIndex = 0;

    void Start()
    {
        Card[] cards = Resources.LoadAll<Card>("CardData"); // Load all cards from the Resources/CardData folder

        deck.AddRange(cards);

        HandManager handManager = FindObjectOfType<HandManager>();
        for (int i = 0; i < 6; i++) // Draw 6 cards at the start
        {
            if (deck.Count > 0)
            {
                DrawCard(handManager);
            }
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if (deck == null || deck.Count == 0)
        {
            Debug.Log("Deck is empty. Cannot draw a card.");
            return;
        }

        Card cardToDraw = deck[currentIndex];
        handManager.AddCardToHand(cardToDraw);
        currentIndex = (currentIndex + 1) % deck.Count; // Loop back to the start of the deck
    }
}
