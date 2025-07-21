using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KuroProductions;

public class HandManager : MonoBehaviour
{
    public DeckManager deckManager; // Reference to the DeckManager
    public GameObject cardPrefab; // Assign the card prefab in the inspector
    public Transform handTransform; // Root of the hand position
    public float fanSpread = -7.5f; // Spread angle for the fan effect
    public float horizontalSpacing = 100f; // Spacing between cards
    public float verticalSpacing = 50f; // Vertical spacing between cards
    public List<GameObject> cardsInHand = new List<GameObject>(); // List to hold the card objects in hand
    public int maxHandCount = 8; // Maximum number of cards in hand

    void Start()
    {

    }

    public void AddCardToHand(Card cardData)
    {
        if (cardsInHand.Count >= maxHandCount) // Limit the hand to 10 cards
        {
            Debug.LogWarning("Cannot add more than 8 cards to hand.");
            return;
        }
        // Create a new card object from the prefab
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);

        // Set the card data for the new card
        newCard.GetComponent<CardDisplay>().cardData = cardData;

        // Set the position of the new card in the fan layout
        UpdateHandVisuals();
    }

    void Update()
    {
        //UpdateHandVisuals();
    }

    public void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 0) return;

        if (cardCount == 1)
        {
            // If there's only one card, center it
            cardsInHand[0].transform.localPosition = Vector3.zero;
            cardsInHand[0].transform.localRotation = Quaternion.identity;
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = fanSpread * (i - (cardCount - 1) / 2f);
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = horizontalSpacing * (i - (cardCount - 1) / 2f);

            float normazliedPosition = 2f * i / (cardCount - 1) - 1f; // Normalize position between -1 and 1
            float verticalOffset = verticalSpacing * (1 - normazliedPosition * normazliedPosition); // Parabolic vertical offset

            // Set the position of the card in the fan layout
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }
}
