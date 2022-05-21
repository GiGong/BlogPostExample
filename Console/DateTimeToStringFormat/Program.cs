using System.Globalization;

namespace DateTimeToStringFormat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DateTime now = DateTime.Now;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("ko-KR");

            Console.WriteLine("한국 예시");
            Console.WriteLine("기본 출력 방식");
            Console.WriteLine(now.ToString());
            Console.WriteLine();

            Console.WriteLine("Format String 예시");
            Console.WriteLine("{0:yyyy}년 {0:MM}월 {0:dd}일, {0:tt} {0:hh}시 {0:mm}분 {0:ss}초 입니다", now);
            Console.WriteLine(now.ToString("yyyy.MM.dd HH:mm"));
            Console.WriteLine(now.ToString("MM월 dd일 HH시 입니다."));
            Console.WriteLine(now.ToString("현재 시각 HH:mm:ss"));
            Console.WriteLine(now.ToString("tt h:mm:ss"));
            Console.WriteLine(now.ToString("yyyy MM dd tt hh mm ss"));


            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("English sample.");
            Console.WriteLine("Default");
            Console.WriteLine(now.ToString());
            Console.WriteLine();
            Console.WriteLine("Use format string");
            Console.WriteLine(now.ToString("MM.dd.yyyy HH:mm:ss (tt)"));
            Console.WriteLine("years: {0:yyyy}\nmonths: {0:MM}\ndays: {0:dd}\nhours:{0:HH}\nminutes:{0:mm}\nseconds:{0:ss}", now);
        }
    }
}

/*
한국 예시
기본 출력 방식
2022-05-21 오후 7:14:19

Format String 예시
2022년 05월 21일, 오후 07시 14분 19초 입니다
2022.05.21 19:14
05월 21일 19시 입니다.
현재 시각 19:14:19
오후 7:14:19
2022 05 21 오후 07 14 19


English sample.
Default
5/21/2022 7:14:19 PM

Use format string
05.21.2022 19:14:19 (PM)
years: 2022
months: 05
days: 21
hours:19
minutes:14
seconds:19
*/