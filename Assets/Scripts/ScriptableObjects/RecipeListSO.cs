using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Commented out: only one is needed, and won't be accidentally created
//[CreateAssetMenu()]
public class RecipeListSO : ScriptableObject
{
  public List<RecipeSO> recipeSOList;
}
