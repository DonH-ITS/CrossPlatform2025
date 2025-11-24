namespace ConsoleApp13;
public class Clothes
{
    public string Colour { get; }
    public Clothes(string colour) {
        Colour = colour;
    }
    public virtual string WhatType() {
        return "I am clothes with colour " + Colour;
    }
}
