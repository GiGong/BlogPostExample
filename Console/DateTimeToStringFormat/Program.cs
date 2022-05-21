using System.Globalization;

namespace DateTimeToStringFormat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DateTime now = DateTime.Now;

            Console.WriteLine(now.ToString("yyyy.MM.dd HH:mm"));
            Console.WriteLine(now.ToString("MM월 dd일 HH시 입니다."));
            Console.WriteLine(now.ToString("현재 시각 HH:mm:ss"));
            Console.WriteLine(now.ToString("tt h:mm:ss"));
            Console.WriteLine(now.ToString("yyyy MM dd tt hh mm ss"));


            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Console.WriteLine();
            Console.WriteLine("English sample.");
            Console.WriteLine(now.ToString("yyyy MM dd tt hh mm ss"));

        }
    }
}

/*
예시 결과

2022.05.21 18:00
05월 21일 18시 입니다.
현재 시각 18:00:02
오후 6:00:02
2022 05 21 오후 06 00 02

English sample.
2022 05 21 PM 06 00 02
*/