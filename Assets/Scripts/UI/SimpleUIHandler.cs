using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class SimpleUIHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Button _resetButton; 

    public void OnPointerClick(PointerEventData eventData)
    {
        _resetButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}