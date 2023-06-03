#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.HighDefinition;

[ExecuteInEditMode]
public class LightLayerChanger : MonoBehaviour
{
    [SerializeField]
    //[ValidateInput("ValidateInput1",
    //    "Same component exists in childs, this is not allowed")]
    LightLayerEnum _targetLayer = LightLayerEnum.LightLayerDefault;
    [SerializeField]
    //[ValidateInput("ValidateInput2",
    //    "Same component exists in parent, this is not allowed")]
    bool _dummyUpdateToggle;

    private void OnValidate()
    {
        // guessing we never want to choose Everything or Nothing as
        // valid layer so we exclude it here to avoid edge cases
        if (_targetLayer == LightLayerEnum.Nothing) return;
        if (_targetLayer == LightLayerEnum.Everything) return;

        System.Array enumValues = System.Enum.GetValues(typeof(LightLayerEnum));

        foreach (Light light in GetComponentsInChildren<Light>(true))
        {
            int bitmask = light.renderingLayerMask;
            bitmask = CalculateBitmask(bitmask, enumValues);
            light.renderingLayerMask = bitmask;
        }

        foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true))
        {
            int bitmask = (int)renderer.renderingLayerMask;
            bitmask = CalculateBitmask(bitmask, enumValues);
            renderer.renderingLayerMask = (uint)bitmask;
        }
    }

    int CalculateBitmask(int currentBitmask, System.Array enumValues)
    {
        //int originalBitVal = currentBitmask;

        foreach (LightLayerEnum current in enumValues)
        {
            // if everything is not set, the inverse in SetBitmask
            // will set all bits to 0, as if nothing was selected
            // so we can just ignore it here
            // it would probably also mess with decal layers
            if (current == LightLayerEnum.Everything) continue;

            int layerBitVal = (int)current;

            bool set = current == _targetLayer;
            //if (set) Debug.Log("Set " + current);
            currentBitmask = SetBitmask(currentBitmask, layerBitVal, set);
        }

        //Debug.Log("| Bitmask : " + currentBitmask +
        //        "\r\n| Original: " + originalBitVal);

        return currentBitmask;
    }

    int SetBitmask(int bitmask, int bitVal, bool set)
    {
        if (set)
            // or "|" will add the value, the 1 at the right position
            bitmask |= bitVal;
        else
            // and "&" will multiply the value, but we take the inverse
            // so the bit position is 0 while all others are 1
            // everything stays as it is, except for the one value
            // which will be set to 0
            bitmask &= ~bitVal;

        return bitmask;
    }

    //bool ValidateInput1(LightLayerEnum layer)
    //{
    //    return GetComponentsInChildren<LightLayerForChilds>(true).Length <= 1;
    //}
    //bool ValidateInput2(bool boolValue)
    //{
    //    return GetComponentsInParent<LightLayerForChilds>(true).Length <= 1;
    //}

}
#endif
