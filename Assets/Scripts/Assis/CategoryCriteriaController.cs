using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CategoryCriteriaController : MonoBehaviour
{
    public TMP_Dropdown CurrentPriceValue;
    public TMP_Dropdown CurrentSpeedValue;
    public TMP_Dropdown CurrentRatingValue;

    public string PriceValue;
    public string SpeedValue;
    public string RatingValue;

    private void Update()
    {
        OnDropdownValueChanged();
    }
    public void OnDropdownValueChanged()
    {
        int index = CurrentPriceValue.value;
        switch (index)
        {
            case 0:
                PriceValue = "<= 300000";
                break;
            case 1:
                PriceValue = ">= 300000";
                break;

        }

        int index1 = CurrentSpeedValue.value;
        switch (index1)
        {
            case 0:
                SpeedValue = "(MenuItem_Speed >= 7 and MenuItem_Speed <= 9)";
                break;
            case 1:
                SpeedValue = "(MenuItem_Speed >= 5 and MenuItem_Speed <= 6)";
                break;
            case 2:
                SpeedValue = "MenuItem_Speed <= 4";
                break;

        }

        int index2 = CurrentRatingValue.value;
        switch (index2)
        {
            case 0:
                RatingValue = "(MenuItem_Rating >= 1 and MenuItem_Rating <= 2)";
                break;
            case 1:
                RatingValue = "(MenuItem_Rating >= 2 and MenuItem_Rating <= 3)";
                break;
            case 2:
                RatingValue = "(MenuItem_Rating >= 3 and MenuItem_Rating <= 4)";
                break;
            case 3:
                RatingValue = "(MenuItem_Rating >= 4 and MenuItem_Rating <= 5)";
                break;

        }
    }

  
}
