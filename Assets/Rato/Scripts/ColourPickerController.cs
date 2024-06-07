using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColourPickerController : MonoBehaviour
{
    private float currentHue, currentSat, currentVal;
    [SerializeField] private RawImage hueImage, svImage, outputImage;
    [SerializeField] private Slider hueSlider;
    [SerializeField] private TMP_InputField hexInputField;
    private Texture2D hueTexture, svTexture, outputTexture;
    private Color currentColour;

    private void Start(){
        CreateHueImage();
        CreateSVImage();
        CreateOutputImage();
        UpdateOutputImage();
    }

    private void CreateHueImage(){
        this.hueTexture = new Texture2D(1, 16);
        this.hueTexture.wrapMode = TextureWrapMode.Clamp;
        this.hueTexture.name = "HueTexture";

        for(int i = 0; i < this.hueTexture.height; i++){
            this.hueTexture.SetPixel(0, i, Color.HSVToRGB((float) i / this.hueTexture.height, 1, 1f));
        }

        this.hueTexture.Apply();
        this.currentHue = 0;

        this.hueImage.texture = this.hueTexture;
    }

    private void CreateSVImage(){
        this.svTexture = new Texture2D(16, 16);
        this.svTexture.wrapMode = TextureWrapMode.Clamp;
        this.svTexture.name = "SVTexture";

        for(int i = 0; i < this.svTexture.height; i++){
            for(int j = 0; j < svTexture.width; j++){
                this.svTexture.SetPixel(i, j, Color.HSVToRGB(currentHue, (float)i / this.svTexture.width, (float)j / this.svTexture.height));
            }
        }

        this.svTexture.Apply();
        this.currentSat = 0;
        this.currentVal = 0;

        this.svImage.texture = this.svTexture;
    }

    private void CreateOutputImage(){
        this.outputTexture = new Texture2D(1, 16);
        this.outputTexture.wrapMode = TextureWrapMode.Clamp;
        this.outputTexture.name = "OutputTexture";

        Color currentColour = Color.HSVToRGB(this.currentHue, this.currentSat, this.currentVal);

        for(int i = 0; i < this.outputTexture.height; i++){
            this.outputTexture.SetPixel(0, i, currentColour);
        }

        this.outputTexture.Apply();

        this.outputImage.texture = this.outputTexture;
    }

    private void UpdateOutputImage(){
        this.currentColour = Color.HSVToRGB(this.currentHue, this.currentSat, this.currentVal);
        
        for(int i = 0; i < this.outputTexture.height; i++){
            this.outputTexture.SetPixel(0, i, currentColour);
        }

        this.outputTexture.Apply();

        hexInputField.text = ColorUtility.ToHtmlStringRGB(currentColour);
    }

    public void SetSV(float s, float v){
        this.currentSat = s;
        this.currentVal = v;

        UpdateOutputImage();
    }

    public void UpdateSVImage(){
        this.currentHue = this.hueSlider.value;
        
        for(int i = 0; i < this.svTexture.height; i++){
            for(int j = 0; j < this.svTexture.width; j++){
                this.svTexture.SetPixel(j, i, Color.HSVToRGB(this.currentHue, (float) j / this.svTexture.width, (float) i / this.svTexture.height));
            }
        }

        this.svTexture.Apply();

        UpdateOutputImage();
    }

    public void OnTextInput(){
        if(hexInputField.text.Length < 6){
            return;
        }

        Color newColor;
        if(ColorUtility.TryParseHtmlString("#" + hexInputField.text, out newColor)){
            Color.RGBToHSV(newColor, out currentHue, out currentSat, out currentVal);
        }

        hueSlider.value = currentHue;
        hexInputField.text = "";
        UpdateOutputImage();
    }

    public Color GetCurrentColour(){
        return this.currentColour;
    }

    public void SetCurrentColour(Color color){
        this.currentColour = color;
        CreateHueImage();
        CreateSVImage();
        CreateOutputImage();
        UpdateOutputImage();
    }
}
