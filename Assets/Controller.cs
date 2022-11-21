using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class Controller : MonoBehaviour
{
    [SerializeField] GameObject inputField;

    [SerializeField] TMP_InputField passLength_Input;
    [SerializeField] Slider passLength_Slider;

    [SerializeField] Toggle AZUpper;
    [SerializeField] Toggle azLower;
    [SerializeField] Toggle digits;
    [SerializeField] Toggle specialSymbols;
    [SerializeField] Toggle advoidSimilar;

    [SerializeField] TMP_InputField passResult;

    [SerializeField] Button hideButton;
    private bool isHide = false;

    [SerializeField] Button copyButton;
    [SerializeField] Button saveButton;

    [SerializeField] Button calButton;

    private List<GameObject> list = new List<GameObject>();
    private List<double> listInt = new List<double>();
    private int amount = 0;
    private double result = 0;

    private int type = 1;

    [SerializeField] DataSaveController saveController;

    //Singleton
    public static Controller Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        Clear();
        passLength_Input.onDeselect.AddListener(delegate { OnInputDeselect(); });
        passLength_Slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

        hideButton.onClick.AddListener(delegate { ShowPass(); });
        copyButton.onClick.AddListener(delegate { CopyToClibboard(); });
        saveButton.onClick.AddListener(delegate { SaveToFile(); });

        SaveFileBtn.onClick.AddListener(delegate { SaveToDisk(); });
        fileName_Input.onDeselect.AddListener(delegate { CheckFileName(); });

    }

    private void DropdownitemSelected()
    {
        
    }


    public void OnValueChanged()
    {
        if(CheckValidate())
        {
            calButton.interactable = true;
        }
        else
        {
            calButton.interactable= false;
        }
    }

    private bool CheckValidate()
    {
        if (passLength_Input.text == "")
        {
            return false;
        }

        //return text.All(char.IsDigit);
        return true;
    }

    private void OnSliderValueChanged()
    {
        passLength_Input.text = passLength_Slider.value.ToString();
    }    

    private void OnInputDeselect()
    {
        try
        {
            int length = int.Parse(passLength_Input.text);
            if (length > 2048) 
            { 
                passLength_Input.text = "2048";
                passLength_Slider.value = 2048;
            }else if(length < 0)
            {
                passLength_Input.text = "0";
                passLength_Slider.value = 0;
            }
            else
            {
                //passLength_Input.text = "2048";
                passLength_Slider.value = length;
            }
        }catch (System.Exception e)
        {

        }
    }


    public void Process()
    {
        GenerateListCharacters();
        GenneratePass();
    }

    private void GenneratePass()
    {
        var length_m = (int)passLength_Slider.value;

        string result = "";

        int lengthOfListCharacters = currentListCharacter.Length;
        for(int i = 0; i < length_m; i++)
        {
            result += currentListCharacter[UnityEngine.Random.Range(0, lengthOfListCharacters)];
        }

        passResult.text = result;
    }

    string currentListCharacter = "";
    string listAdvoidCharacters = "1IlioO0";
    private void GenerateListCharacters()
    {
        currentListCharacter = "";

        if (AZUpper.isOn)
        {
            currentListCharacter += "QWERTYUIOPASDFGHJKLZXCVBNM";
        }

        if(azLower.isOn)
        {
            currentListCharacter += "qwertyuiopasdfghjklzxcvbnm";
        }

        if(digits.isOn)
        {
            currentListCharacter += "0123456789";
        }

        if(specialSymbols.isOn)
        {
            currentListCharacter += "!@#$%^&*=";
        }

        if(advoidSimilar.isOn)
        {
            string temp = "";
            for(int i = 0; i < currentListCharacter.Length; i++)
            {
                if (listAdvoidCharacters.Contains(currentListCharacter[i])) continue;
                temp += currentListCharacter[i];
            }

            currentListCharacter = temp;
        }
    }

    private void ShowPass()
    {
        isHide = !isHide;
        if (isHide)
            passResult.contentType = TMP_InputField.ContentType.Password;
        else
        {
            passResult.contentType = TMP_InputField.ContentType.Standard;
            passResult.lineType = TMP_InputField.LineType.MultiLineNewline;
        }
        passResult.ForceLabelUpdate();
    }

    private void CopyToClibboard()
    {
        GUIUtility.systemCopyBuffer = passResult.text;
    }

    [SerializeField] GameObject savePopup;
    [SerializeField] TMP_InputField fileName_Input;
    [SerializeField] Button SaveFileBtn;
    private void SaveToFile()
    {
        savePopup.SetActive(true);
        
    }

    private void CheckFileName()
    {
        if(fileName_Input.text != "")
        {
            SaveFileBtn.interactable = true;
        }
    }

    private void SaveToDisk()
    {
        SaveFileBtn.interactable = false;
        savePopup.SetActive(false);
        saveController.WriteFile(passResult.text, fileName_Input.text);
    }

    public void Clear()
    {
        passLength_Input.text = "";
        passLength_Slider.value = 0;
        passLength_Slider.maxValue = 2048;
        passResult.text = "";

        calButton.interactable = false;
        SaveFileBtn.interactable = false;
        savePopup.SetActive(false);
    }

    public void Quit()
    {
        Clear();
        Application.Quit();
    }
}
