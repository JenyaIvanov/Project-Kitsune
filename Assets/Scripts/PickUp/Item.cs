using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO inventoryItem { get; private set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float animation_duration = 0.3f;

    [SerializeField]
    private SpriteRenderer outlineChild;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = inventoryItem.ItemImage;
        outlineChild.sprite = inventoryItem.ItemImage;
    }

    internal void DestroyItem()
    {
        GetComponent<Collider>().enabled = false;
        StartCoroutine(AnimateItemPickup());
    }

    IEnumerator AnimateItemPickup()
    {
        audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;

        while(currentTime < animation_duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / animation_duration);
            yield return null;
        }

        Destroy(gameObject);
    }


}
