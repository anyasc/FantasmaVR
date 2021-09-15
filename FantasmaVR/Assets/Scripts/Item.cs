using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemName
    {
        Key,
        Crowbar,
        Brush,
        Photo,
        Lighter,
    }

    public ItemName itemName;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemName)
        {
            default:
            case ItemName.Key: return ItemAssets.Instance.key;
            case ItemName.Crowbar: return ItemAssets.Instance.crowbar;
            case ItemName.Brush: return ItemAssets.Instance.brush;
            case ItemName.Lighter: return ItemAssets.Instance.lighter;
            case ItemName.Photo: return ItemAssets.Instance.photo;
        }
    }

    public string GetInteractible()
    {
        switch (itemName)
        {
            default:
            case ItemName.Key: return "portaQuarto";
            case ItemName.Crowbar: return "BasementDoor";
            case ItemName.Brush: return "PaintCanvas";
            case ItemName.Lighter: return "Candle";
            case ItemName.Photo: return "Frame";

        }
    }

    public string GetItemDescription()
    {
        switch (itemName)
        {
            default:
            case ItemName.Key: return "Uma chave... O que ela abre?";
            case ItemName.Crowbar: return "Não sei o que vou fazer com esse pé de cabra";
            case ItemName.Brush: return "Pincel, se me der vontade de pintar por aí";
            case ItemName.Lighter: return "Uma caixa de fósforos, pode ser útil";
            case ItemName.Photo:
                switch (amount)
                {
                    default:
                    case 1: return "Um pedaço de papel pintado";
                    case 2: return "Dois pedaços de papel pintados";
                    case 3: return "Três pedaços de papel pintados";
                    case 4: return "Quatro pedaços de papel pintados";
                }

        }
    }



    public bool isStackable()
    {
        switch (itemName)
        {
            default:
            case ItemName.Key:
            case ItemName.Crowbar:
            case ItemName.Brush:
            case ItemName.Lighter:
                return false;
            case ItemName.Photo:
                return true;
        }
    }
}
