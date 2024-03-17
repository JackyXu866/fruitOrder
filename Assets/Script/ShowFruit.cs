using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShowFruit : MonoBehaviour
{
    [SerializeField] List<Sprite> fruitList = new List<Sprite>();
    [SerializeField] Image fruitImage;
    [SerializeField] float timeToChange = 3f;
    [SerializeField] List<Button> buttons = new List<Button>();
    [SerializeField] GameObject resultPanel, winPanel, losePanel, startPanel;
    public int counter = 0;

    public void RandomizeFruit()
    {
        fruitList = fruitList.OrderBy(x => Random.value).ToList();
    }

    public IEnumerator ShowFruitImage()
    {
        counter = 0;
        RandomizeFruit();
        
        for (int i = 0; i < fruitList.Count; i++) {
            fruitImage.sprite = fruitList[i];
            yield return new WaitForSeconds(timeToChange);
        }

        resultPanel.SetActive(true);

        yield return null;
    }

    public void ToShow() {
        StartCoroutine(ShowFruitImage());
    }

    public void CheckOrder(Sprite fruit) {
        Debug.Log(fruit);
        if (fruit == fruitList[counter]) {
            counter++;
            if (counter == fruitList.Count) {
                // win
                winPanel.SetActive(true);
            }
        } 
        else {
            // lose
            losePanel.SetActive(true);
        }
    }

    private void Awake() {
        resultPanel.SetActive(true);
        if (buttons.Count != fruitList.Count) {
            Debug.LogError("The number of buttons is different from the number of fruits");
        }

        for(int i = 0; i < fruitList.Count; i++) {
            buttons[i].image.sprite = fruitList[i];
            Sprite fruit = fruitList[i];
            buttons[i].onClick.AddListener(() => CheckOrder(fruit));
        }
        resultPanel.SetActive(false);
    }

    public void Restart() {
        resultPanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        startPanel.SetActive(false);
        ToShow();
    }
}

