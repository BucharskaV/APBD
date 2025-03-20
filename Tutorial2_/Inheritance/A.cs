namespace Tutorial2_.Inheritance;

public class A
{
    public int Number { get; set; }
    private int number;

    public A(int number)
    {
        this.number = number;
    }

    public int GetNumber(){return number;}
    public void SetNumber(int number){this.number = number;}
}