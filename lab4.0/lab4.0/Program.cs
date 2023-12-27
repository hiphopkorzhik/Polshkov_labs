string text = "";
int countWiseMan1 = 0;
int countWiseMan2 = 0;

string WiseMan1(int countWiseMan2)
{
    countWiseMan1++;
    if (countWiseMan2 % 2 == 0)
    {
        return text;
    }
    return "Не услышал что сказал Мудрец 2";
    
}

string WiseMan2(int countWiseMan1)
{
    countWiseMan2++;
    if (countWiseMan1 % 2 == 0)
    {
        return text;
    }
    return "Не услышал что сказал Мудрец 1";
}

while (true)
{

    var outer = Task.Factory.StartNew(() =>
    {
        text = $"Умные мысли мудрецов и счетчик: {countWiseMan1}";
        Console.WriteLine(WiseMan1(countWiseMan2));
        var inner = Task.Factory.StartNew(() =>
        {
            Thread.Sleep(1000);
            Console.WriteLine(WiseMan2(countWiseMan1));
            Console.WriteLine(WiseMan1(countWiseMan2));



        }, TaskCreationOptions.AttachedToParent);

    });

    outer.Wait();
}


