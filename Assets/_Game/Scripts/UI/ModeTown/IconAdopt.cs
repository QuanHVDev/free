using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IconAdopt : MonoBehaviour
{
    [SerializeField] private TMP_Text txtTag;
    public TagCat Tag { get; private set; }
    public void Init(TagCat tag)
    {
        this.txtTag.text = tag.ToString();
        this.Tag = tag;
        ActiveTagText(true);
    }

    public void Reset()
    {
        Tag = TagCat.None;
        gameObject.SetActive(false);
    }

    public void ActiveTagText(bool enable)
    {
        txtTag.gameObject.SetActive(enable);
    }
}
