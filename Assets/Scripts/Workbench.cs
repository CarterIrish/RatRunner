using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour
{
    [Header("Available Recipes")]
    public List<CraftingRecipe> _availableRecipies;

    [Header("Interaction")]
    public KeyCode _interactKey = KeyCode.E;
    public float _interactRange = 3.0f;



}
