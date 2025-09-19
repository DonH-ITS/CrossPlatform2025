namespace ConsoleApp13;

public class Shirt : Clothes
{
    private bool longsleeves;
    public Shirt(string colour, bool longsleeves) : base(colour) {
        this.longsleeves = longsleeves;
    }
    public override string WhatType() {
        string result = "I am a ";
        if (longsleeves)
            result += "long ";
        else
            result += "short ";
        result += "sleeved " + Colour + " shirt";
        return result;
    }
}

