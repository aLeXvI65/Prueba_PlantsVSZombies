using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Card : MonoBehaviour {

    public int cost = 50;

    [SerializeField]
    private GameObject plant;

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Image disabled; // This image will be shown if the user can't buy the card
    [SerializeField]
    private Image reload; // This image will be shown if the card just got bought and need to be reloaded
    [SerializeField]
    private TextMeshProUGUI costText;

    private RectTransform rt;
    private GameController gc;
    private Player player;

    Vector3 initialPosition;

    [SerializeField]
    private int cardIndex;

    private float reloadValue = 1;
    [SerializeField]
    private float reloadTime = 5;

    private bool isReloading = false;

    void Start() {
        GameObject gcObj = GameObject.FindGameObjectWithTag("GameController");
        if (gcObj != null) {
            gc = gcObj.GetComponent<GameController>();
        }
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) {
            player = playerObj.GetComponent<Player>();
        }

        rt = GetComponent<RectTransform>();
        initialPosition = rt.anchoredPosition;

        if (costText != null) {
            costText.text = ""+cost;
        }
    }

    void Update() {
        // If card is reloading, a dark reload box will be shown over the card, the reload box fillAmount will be reduced until it disappears and user can select card again.
        if (isReloading) {
            // We calculate how much is the value wee need to reduce fillAmount in reload box by each FPS and in a way that it takes the selected reloadTime
            float reloadAmount = (1f / (float)(reloadTime)) * Time.deltaTime;
            reloadValue -= reloadAmount;
            reload.fillAmount = reloadValue;

            // If reload box finish vanishing, it's disabled and user can select the card again
            if (reloadValue <= 0) {
                reload.enabled = false;
                isReloading = false;
            }
        }

        if (gc != null) {
            // If user still don't have enough sun to buy the card, a disabled box is shown.
            if (gc.sunScore < cost) {
                disabled.enabled = true;
            }
            else {
                disabled.enabled = false;
            }
        }
    }

    public void beginDragCard(BaseEventData data) {
        if (player != null) {
            player.preparePlantToSpawn(plant);
        }
    }

    public void DragCard(BaseEventData data) {
        // This is the only code I needed to take from internet, just to know how to drag a card.
        PointerEventData pointerEventData = data as PointerEventData;

        // Map screen point to the local space of the RectTransform of the card, to determine how its position should be
        // changed to be dragged.
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerEventData.position,
            canvas.worldCamera,
            out position);

        // Center position using card width and height.
        position.x -= rt.sizeDelta.x / 2;
        position.y += rt.sizeDelta.y / 2;

        transform.position = canvas.transform.TransformPoint(position);
    }

    public void endDragCard(BaseEventData data) {
        // Return card to its original position
        rt.anchoredPosition = initialPosition;
        if (player != null) {
            // Try Buy card and if able to buy put it in the floor Cube
            player.putPlantOnPlace(this);
        }
    }

    public void disableCard() {
        // Activate a dark square to show card as disabled until card is reloaded.
        reload.enabled = true;
        isReloading = true;
        reloadValue = 1;
    }
}
