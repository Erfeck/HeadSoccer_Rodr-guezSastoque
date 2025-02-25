using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Image emptyX2Image;
    public Image colorX2Image;
    public Image finalX2Image;
    public Image emptyGiantImage;
    public Image colorGiantImage;
    public Image finalGiantImage;
    public float fadeDuration;
    public float elapsedTime;

    void Start()
    {
        elapsedTime = 0f;
        PlayerPrefs.SetString("wasSkillUsed", "No");

        if (PlayerPrefs.GetString("typeSkill").Equals("Giant"))
        {
            Color tempColorGiant = colorGiantImage.color;
            tempColorGiant.a = 0;
            colorGiantImage.color = tempColorGiant;
        }
        else if (PlayerPrefs.GetString("typeSkill").Equals("X2"))
        {
            Color tempColorX2 = colorX2Image.color;
            tempColorX2.a = 0;
            colorX2Image.color = tempColorX2;
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetString("typeSkill").Equals("Giant"))
        {
            colorImage(emptyGiantImage, colorGiantImage, finalGiantImage);
        }
        else if (PlayerPrefs.GetString("typeSkill").Equals("X2"))
        {
            colorImage(emptyX2Image, colorX2Image, finalX2Image);
        }
        
    }

    private void colorImage(Image emptyImage, Image colorImage, Image finalImage)
    {
        if (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            if (colorImage != null)
            {
                colorImage.color = new Color(colorImage.color.r, colorImage.color.g, colorImage.color.b, alpha);
            }
        }
        else
        {
            colorImage.sprite = finalImage.sprite;
        }

        if (PlayerPrefs.GetString("wasSkillUsed").Equals("Yes"))
        {
            colorImage.sprite = emptyImage.sprite;
        }
    }
}
