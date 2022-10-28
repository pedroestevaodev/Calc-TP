using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculatorController : MonoBehaviour
{
    [SerializeField]
    private Text Display1;

    [SerializeField]
    private Text Display2;

    private Boolean ExistOperations;
    private Boolean ExistResult;
    private Boolean ExistDecimal;
    private Boolean NextNumber;

    private String InitialValue1 = "0";
    private String InitialValue2 = "";

    // Start is called before the first frame update
    void Start()
    {
        Display1.text = InitialValue1;
        Display2.text = InitialValue2;
    }

    public static string SerializeField(string value)
    {
        switch(value)
        {
            case string serializeComma when (serializeComma.Contains(",") && ((value.Substring(value.Length-1, 1) == ",") || (value.Substring(value.Length-1, 1) == "0" && value.Substring(value.Length-2, 1) == ","))):
                return value.Split(",")[0];
            default:
                return value;
        }
    }

    public void BtnNumber(string number)
    {
        String oldExpression = Display2.text;
        switch(oldExpression)
        {
            case string afterResult when (afterResult.Contains("=") && ExistResult):
                Display1.text = "";
                Display2.text = "";
            break;
            // case string newValue when (newValue.Length > 0 && ExistOperations && !ExistDecimal):
            case string newValue when (newValue.Length > 0 && NextNumber && !ExistDecimal):
                Display1.text = "";
                NextNumber = false;
            break;
        }
        
        String valorAtual = Display1.text;
        if(valorAtual == "0")
        {
            Display1.text = "";
        }

        Display1.text = Display1.text + number;

        ExistResult = false;
    }

    public void BtnOperation(string operation)
    {
        if(ExistOperations && !ExistResult)
        {
            ReCalculateOperation(Display2.text, Display1.text, operation);
        }
        else
        {
            Display2.text = SerializeField(Display1.text)+" "+operation;
        }

        ExistOperations = true;
        ExistDecimal = false;
        NextNumber = true;
    }

    public void BtnComma(string comma)
    {
        String expression = Display1.text;
        if(!expression.Contains(","))
        {
            Display1.text = Display1.text+comma;
        }
        if(expression.Contains(",") && ExistOperations)
        {
            Display1.text = InitialValue1+comma;
        }
        ExistDecimal = true;
    }

    public void CalculateResult(string expression)
    {
        switch(expression)
        {
            case string soma when soma.Contains(" + "):
                String[] somaValores = soma.Split(" + ");
                Decimal somaValor1 = decimal.Parse(somaValores[0]);
                Decimal somaValor2 = decimal.Parse(somaValores[1]);
                Decimal somaResult = somaValor1+somaValor2;
                Display1.text = SerializeField(somaResult.ToString());
            break;
            case string sub when sub.Contains(" - "):
                String[] subValores = sub.Split(" - ");
                Decimal subValor1 = decimal.Parse(subValores[0]);
                Decimal subValor2 = decimal.Parse(subValores[1]);
                Decimal subResult = subValor1-subValor2;
                Display1.text = SerializeField(subResult.ToString());
            break;
            case string multi when multi.Contains(" * "):
                String[] multiValores = multi.Split(" * ");
                Decimal multiValor1 = decimal.Parse(multiValores[0]);
                Decimal multiValor2 = decimal.Parse(multiValores[1]);
                Decimal multiResult = multiValor1*multiValor2;
                Display1.text = SerializeField(multiResult.ToString());
            break;
            case string divi when divi.Contains(" / "):
                String[] diviValores = divi.Split(" / ");
                Decimal diviValor1 = decimal.Parse(diviValores[0]);
                Decimal diviValor2 = decimal.Parse(diviValores[1]);
                Decimal diviResult = diviValor1/diviValor2;
                Display1.text = SerializeField(diviResult.ToString());
            break;
        }
    }

    public void ReCalculateResult(string expression, string newValue)
    {
        switch(expression)
        {
            case string soma when soma.Contains(" + "):
                String[] somaValores = soma.Remove(soma.Length - 2).Split(" + ");
                Decimal somaValor1 = decimal.Parse(newValue);
                Decimal somaValor2 = decimal.Parse(somaValores[1]);
                Decimal somaResult = somaValor1+somaValor2;
                Display2.text = newValue+" + "+somaValores[1];
                Display1.text = SerializeField(somaResult.ToString());
            break;
            case string sub when sub.Contains(" - "):
                String[] subValores = sub.Remove(sub.Length - 2).Split(" - ");
                Decimal subValor1 = decimal.Parse(newValue);
                Decimal subValor2 = decimal.Parse(subValores[1]);
                Decimal subResult = subValor1-subValor2;
                Display2.text = newValue+" - "+subValores[1];
                Display1.text = SerializeField(subResult.ToString());
            break;
            case string multi when multi.Contains(" * "):
                String[] multiValores = multi.Remove(multi.Length - 2).Split(" * ");
                Decimal multiValor1 = decimal.Parse(newValue);
                Decimal multiValor2 = decimal.Parse(multiValores[1]);
                Decimal multiResult = multiValor1*multiValor2;
                Display2.text = newValue+" * "+multiValores[1];
                Display1.text = SerializeField(multiResult.ToString());
            break;
            case string divi when divi.Contains(" / "):
                String[] diviValores = divi.Remove(divi.Length - 2).Split(" / ");
                Decimal diviValor1 = decimal.Parse(newValue);
                Decimal diviValor2 = decimal.Parse(diviValores[1]);
                Decimal diviResult = diviValor1/diviValor2;
                Display2.text = newValue+" / "+diviValores[1];
                Display1.text = SerializeField(diviResult.ToString());
            break;
        }
    }

    public void ReCalculateOperation(string expression, string newValue, string operation)
    {
        switch(expression)
        {
            case string soma when soma.Contains(" +"):
                String[] somaValores = soma.Split(" +");
                Decimal somaValor1 = decimal.Parse(somaValores[0]);
                Decimal somaValor2 = decimal.Parse(newValue);
                Decimal somaResult = somaValor1+somaValor2;
                Display2.text = somaResult+" "+operation;
                Display1.text = SerializeField(somaResult.ToString());
            break;
            case string sub when sub.Contains(" -"):
                String[] subValores = sub.Split(" -");
                Decimal subValor1 = decimal.Parse(subValores[0]);
                Decimal subValor2 = decimal.Parse(newValue);
                Decimal subResult = subValor1-subValor2;
                Display2.text = subResult+" "+operation;
                Display1.text = SerializeField(subResult.ToString());
            break;
            case string multi when multi.Contains(" *"):
                String[] multiValores = multi.Split(" *");
                Decimal multiValor1 = decimal.Parse(multiValores[0]);
                Decimal multiValor2 = decimal.Parse(newValue);
                Decimal multiResult = multiValor1*multiValor2;
                Display2.text = multiResult+" "+operation;
                Display1.text = SerializeField(multiResult.ToString());
            break;
            case string divi when divi.Contains(" /"):
                String[] diviValores = divi.Split(" /");
                Decimal diviValor1 = decimal.Parse(diviValores[0]);
                Decimal diviValor2 = decimal.Parse(newValue);
                Decimal diviResult = diviValor1/diviValor2;
                Display2.text = diviResult+" "+operation;
                Display1.text = SerializeField(diviResult.ToString());
            break;
        }
    }

    public void BtnResult(string character)
    {
        ExistDecimal = false;

        String oldExpression = Display2.text;
        if(oldExpression.Contains("=") && ExistResult)
        {
            ReCalculateResult(oldExpression, Display1.text);
            Display2.text = Display2.text+" "+character;
        }
        else
        {
            Display2.text = Display2.text+" "+SerializeField(Display1.text);
            CalculateResult(Display2.text);
            
            Display2.text = Display2.text+" "+character;
        }
        
        ExistResult = true;
        ExistOperations = false;
    }

    public void BtnBackspace()
    {
        String valorAtual = Display1.text;
        switch(valorAtual)
        {
            case string clearLast when (clearLast.Length > 1 && !ExistResult):
                Display1.text = Display1.text.Remove(Display1.text.Length - 1);
            break;
            case string clearUnic when (clearUnic.Length == 1 && !ExistResult):
                Display1.text = InitialValue1;
            break;
            case string clearResult when (ExistResult):
                Display2.text = InitialValue2;
            break;
        }
    }

    public void BtnClear()
    {
        Display1.text = InitialValue1;
        Display2.text = InitialValue2;

        ExistOperations = false;
        ExistResult = false;
        ExistDecimal = false;
    }
}
