using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURLButton : MonoBehaviour
{
    public void OpenWizards()
    {
        Debug.Log("eep");
        Application.OpenURL("https://www.google.com/search?client=firefox-b-1-d&sca_esv=6fd0d93f9f01f957&sca_upv=1&sxsrf=ADLYWILRJ1fTkaYAdUQ1TabrfbjlwS2H6Q:1725550465107&q=awesome+wizard+images&udm=2&fbs=AEQNm0Aa4sjWe7Rqy32pFwRj0UkWd8nbOJfsBGGB5IQQO6L3J_86uWOeqwdnV0yaSF-x2jpXXSZVlK6C0YPjHbsLu8HQlFjXJu4aVhI_llTnXJ4lFAWdNYSKl18X_OYOML0jevhpDEumYRwaaY1jEa7vKdTgiN-XUHVrwULe1SBpdZ2b2Qdf9JCr6vszwGWtXx6BBaAiRVuZU26XGXcLhLP1MT26u-HDMw&sa=X&ved=2ahUKEwiH1I60kKyIAxXCBjQIHaejN1AQtKgLegQIFxAB");
    }
}
