namespace Assignment_1_Tyshchenko;

public class Stack
{
    private const int Capacity = 50;
    private string[] _array = new string[Capacity];
    private int _pointer;
    
    public void Push(string value)
    {
        if (_pointer == _array.Length)
        {
            // this code is raising an exception about reaching stack limit
            throw new Exception("Stack overflowed");
        }
        _array[_pointer] = value;
        _pointer++;
    }
    public string Pull()
    {
        if (_pointer == 0)
        {
            //you can also raise an exception here, but we're simple returning nothing
            return null;
        }
        _pointer--;
        var value = _array[_pointer];
        return value;
    }
}