using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.UI;

public class SVImageController : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    [SerializeField] Image PickerImage;
    private RawImage svImage;
    private ColourPickerController cpc;
    private RectTransform rectTransform, pickerTransform;

    private void Awake(){
        this.svImage = GetComponent<RawImage>();
        this.cpc = FindObjectOfType<ColourPickerController>();
        this.rectTransform = GetComponent<RectTransform>();
        this.pickerTransform = PickerImage.GetComponent<RectTransform>();
        this.pickerTransform.position = new Vector2(-(rectTransform.sizeDelta.x * 0.5f), -(rectTransform.sizeDelta.y));
    }

    private void UpdateColour(PointerEventData eventData){
        Vector3 position = rectTransform.InverseTransformPoint(eventData.position);
        
        float deltaX = rectTransform.sizeDelta.x * 0.5f;
        float deltaY = rectTransform.sizeDelta.y * 0.5f;

        if(position.x > deltaX){
            position.x = deltaX;
        }else if(position.x < -deltaX){
            position.x = -deltaX;
        }

        if(position.y > deltaY){
            position.y = deltaY;
        }else if(position.y < -deltaY){
            position.y = -deltaY;
        }

        float x = position.x + deltaX;
        float y = position.y + deltaY;

        float xNorm = x / this.rectTransform.sizeDelta.x;
        float yNorm = y / this.rectTransform.sizeDelta.y;

        this.pickerTransform.localPosition = position;
        this.PickerImage.color = Color.HSVToRGB(0, 0, 1 - yNorm);

        cpc.SetSV(xNorm, yNorm);
    }

    public void OnDrag(PointerEventData eventData){
        UpdateColour(eventData);
    }

    public void OnPointerClick(PointerEventData eventData){
        UpdateColour(eventData);
    } 
}
