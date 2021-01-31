using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class tz_quickColorLerp : MonoBehaviour
{

    public Color[] colors;
    public float duration = 3.0f;

    private int index = 0;
    public float timer = 0.0f;
    public Color currentColor;
    public Color startColor;


    public bool canLerp = false;

    public bool changeMainDisplay = false;

    public TextMeshProUGUI[] texts;
    public Image[] images;

    void Start()
    {

        currentColor = new Color(0.066f, 0.912f, 0.275f, 1.000f);
        //		startColor = new Color(0.066f, 0.912f, 0.275f, 1.000f);

        float rand = Random.Range(0, 100);
        if (rand <= 25)
        {
            currentColor = new Color(0.066f, 0.912f, 0.275f, 1.000f);
            colors = new Color[4];
            colors[0] = new Color(0.066f, 0.912f, 0.275f, 1.000f); colors[1] = new Color(0.426f, 0.549f, 1.000f, 1.000f);
            colors[2] = new Color(0.912f, 0.643f, 0.066f, 1.000f); colors[3] = new Color(0.655f, 0.066f, 0.912f, 1.000f);
        }
        else if (rand > 25 && rand <= 50)
        {
            currentColor = new Color(0.655f, 0.066f, 0.912f, 1.000f);
            colors = new Color[4];
            colors[1] = new Color(0.066f, 0.912f, 0.275f, 1.000f); colors[2] = new Color(0.426f, 0.549f, 1.000f, 1.000f);
            colors[3] = new Color(0.912f, 0.643f, 0.066f, 1.000f); colors[0] = new Color(0.655f, 0.066f, 0.912f, 1.000f);
        }
        else if (rand > 50 && rand <= 75)
        {
            currentColor = new Color(0.912f, 0.643f, 0.066f, 1.000f);
            colors = new Color[4];
            colors[2] = new Color(0.066f, 0.912f, 0.275f, 1.000f); colors[3] = new Color(0.426f, 0.549f, 1.000f, 1.000f);
            colors[0] = new Color(0.912f, 0.643f, 0.066f, 1.000f); colors[1] = new Color(0.655f, 0.066f, 0.912f, 1.000f);
        }
        else if (rand > 75)
        {
            currentColor = new Color(0.426f, 0.549f, 1.000f, 1.000f);
            colors = new Color[4];
            colors[3] = new Color(0.066f, 0.912f, 0.275f, 1.000f); colors[0] = new Color(0.426f, 0.549f, 1.000f, 1.000f);
            colors[1] = new Color(0.912f, 0.643f, 0.066f, 1.000f); colors[2] = new Color(0.655f, 0.066f, 0.912f, 1.000f);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (canLerp)
        {
            currentColor = Color.Lerp(startColor, colors[index], timer);

            timer += Time.deltaTime / duration;
            if (timer > 1.0f)
            {
                timer -= 1.0f;
                index++;
                if (index >= colors.Length)
                    index = 0;
                startColor = currentColor;
            }
        }

        if (canLerp) { manageColors(); }
    }

    public void changeLerp(Color changeStartColorTo)
    {
        bool lerpRestart = false;

        if (!canLerp)
        {
            lerpRestart = false;
        }
        else
        {
            lerpRestart = true;
            canLerp = false; //PAUSE LERP
        }

        startColor = changeStartColorTo;
        currentColor = changeStartColorTo;


        timer = 0; //restart timer


        //RESTART OR START
        if (lerpRestart) { canLerp = true; }
    }


    public void manageColors()
    {

        for (int t = 0; t < texts.Length; t++)
        {
            texts[t].color = currentColor;
        }

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = currentColor;
        }

    }
}
