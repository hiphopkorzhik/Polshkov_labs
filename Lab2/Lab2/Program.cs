

using System.Diagnostics;

const int N = 300;
const int Element = 200000;

int[] numberThread = new int[] { 1, 2, 4, 10 };
int[,] arr = new int[N, Element];



object locker = new();


MyDelegate[] delsArray = new MyDelegate[]
{
    FillList, CreateThread
};

foreach(MyDelegate del in delsArray)
{
    del();
}


void FillList()
{
   
    var rnd = new Random();
    for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < Element; j++)
        {
            arr[i, j] = rnd.Next(0, 6);
        }
    }
    Console.WriteLine("Массив заполнен");
}

void CreateThread()
{
    foreach (int th in numberThread)
    {
        int tmp = N / th;
        int start = 0;
        int end = tmp;
        int positive = 0;
        int negative = 0;
        //Console.WriteLine($"Start: {start}, end: {end}, tnp: {tmp}");
        var timer = new Stopwatch();
        timer.Start();
        Parallel.For(0, th, (i, state) =>
        {
            var tuple = CountNegativeOrPositiveNumber(start, end);
            start = end;
            end = end + tmp;
            lock (locker)
            {
                positive += tuple.Item1;
                negative += tuple.Item2;
            }
            
        });
        timer.Stop();
        TimeSpan timeSpan = timer.Elapsed;
        Console.WriteLine($"Поток: {th}, Время выполнения: {timeSpan.ToString(@"ss\.ffff")}");
        positive = 0;
        negative = 0;
    }
}


(int, int) CountNegativeOrPositiveNumber(int start, int end)
{
    int positive = 0;
    int negative = 0;

    for (int i = start; i < end; i++)
    {
        for (int j = 0; j < Element; j++)
        {
            
            if (arr[i, j] % 2 == 0) positive++;
            else negative++;
                        
        }
    }

    return (positive, negative);
}


delegate void MyDelegate();


