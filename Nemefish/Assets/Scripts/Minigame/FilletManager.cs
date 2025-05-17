using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilletManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Sprite> filletFishSprites;
    public List<Sprite> gutFishSprites;
    public List<Sprite> mutantFishSprites;

    public List<Vector3> knifePositions;

    public int index;
    public List<Sprite> currentList;

    public Image activeImage;
    public Image knife;
    
    void Start()
    {
        currentList = gutFishSprites;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdvanceFrame()
    {
        index++;
        if (index >= currentList.Count)
        {
            index = 0;
            //FindAnyObjectByType<UIManager>().ToggleFillet();
        }

        activeImage.sprite = currentList[index];
        knife.transform.localPosition = knifePositions[index];
    }

    public void SetList(string list)
    {
        switch (list)
        {
            case "Fillet":
            {
                currentList = filletFishSprites;
                break;
            }
            case "Gut":
            {
                currentList = gutFishSprites;
                break;
            }
            case "Mutant":
            {
                currentList = mutantFishSprites;
                break;
            }
        }
    }
}
