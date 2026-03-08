namespace Assignment_1_Tyshchenko;

public class Program
{
    static void Main()
    {
        string numbers = ""; // буфер для цифр
        var[] postfix = new var[100];
        int res = 0;
        Stack myStack = new Stack();

        string sm_string = "1+  3*(  2+ 4)";

        for (int i = 0; i < sm_string.Length; i++)
        {
            if (sm_string[i] is int)
            {
                numbers += sm_string[i];
                
                if (i == sm_string.Length - 1 || sm_string[i] is not int)
                {
                    i++;
                    postfix.Add(numbers);
                    numbers = "";
                }
            }
            else
            {
                myStack.Pull(sm_string[i]);
            }
        }
        
        for (int i = 0; i<postfix.Length; i++)
        {
            if (postfix[i] is not int)
            {
                
            }
        }
        
        
        
        // // 1. Створення об'єкта класу Stack
        // Stack myStack = new Stack();
        //
        // // 2. Додавання елементів (Push)
        // myStack.Push("Перший");
        // myStack.Push("Другий");
        // myStack.Push("Третій");
        //
        // // 3. Вилучення елементів (Pull)
        // // Оскільки це стек (LIFO), "Третій" вийде першим
        // string item1 = myStack.Pull(); 
        // Console.WriteLine(item1); // Виведе: Третій
        //
        // string item2 = myStack.Pull();
        // Console.WriteLine(item2); // Виведе: Другий
        //
        // // 4. Демонстрація порожнього стека
        // myStack.Pull(); // Витягли "Перший"
        // var empty = myStack.Pull(); // Тут поверне null, як прописано в логіці
    }
}
    
    