
class Program
{
    static bool Vozavorkovanospravne(string vstup)
    {
        Stack<char> zasobnik = new Stack<char>();
        foreach (char znak in vstup)
        {
            if (znak == '(' || znak == '[' || znak == '{')
            {
                zasobnik.Push(znak);
            }
            else if (znak ==')' || znak == ']' || znak == '}')
            {
                if (zasobnik.Count == 0) {
                    return false;
            }
        char prectenej = zasobnik.Pop();
        if (znak == ')' && prectenej != '(') {
            return false ;}
                if (znak == ']' && prectenej != '[')
                {
                    return false;
                }
                if (znak == '}' && prectenej != '{')
                {
                    return false;
                }
            }
        }
        return zasobnik.Count == 0;
    }

}