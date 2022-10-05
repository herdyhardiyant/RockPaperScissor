
using UnityEngine;

public class Bot : MonoBehaviour
{
    
    private Card []cards;
    
    private void Awake()
    {
        cards = GetComponentsInChildren<Card>();
    }

    private float timer = 0;
    
    void Update()
    {
        if (timer < 0.5)
        {
            timer += Time.deltaTime;
            return;
        }
        
        var random = Random.Range(0, 3);
        cards[random].SelectCardSilently();
        timer = 0;
    }
}
