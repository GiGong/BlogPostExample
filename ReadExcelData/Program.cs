using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReadExcelData
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("input path of excel: ");
            string? input = Console.ReadLine();
            if (input == null)
            {
                return;
            }
            string path = input;

            Console.Write("input num of excel column: ");
            input = Console.ReadLine();
            if (!int.TryParse(input, out int numOfColumn))
            {
                return;
            }

            var excelData = ReadExcelData(path, numOfColumn);

            StringBuilder res = new StringBuilder();

            for (int i = 0; i < excelData.GetLength(0); i++)
            {
                res.AppendLine(string.Join("\t", excelData[i]));
            }
            res.ToString();

            Console.WriteLine(res);
        }

        /// <summary>
        /// Read excel data
        /// </summary>
        /// <param name="path">excel file path</param>
        /// <param name="numOfColumn">number of column bigger than 0</param>
        /// <returns>excel data in type string[][]</returns>
        /// <see href="https://www.gigong.io/2021/12/14/CSharp-read-excel-data"/>
        private static string[][] ReadExcelData(string path, int numOfColumn)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            if (numOfColumn < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            List<string[]> result = new List<string[]>();

            try
            {
                // 엑셀 프로그램 실행
                excelApp = new Excel.Application();

                // 엑셀 파일 열기
                wb = excelApp.Workbooks.Open(path);

                // 첫번째 Worksheet
                ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;

                // 현재 Worksheet에서 사용된 Range 전체를 선택
                Excel.Range rng = ws.UsedRange;

                int row = ws.UsedRange.EntireRow.Count;

                //Excel.Range rng = ws.Range[ws.Cells[1, 1], ws.Cells[row, Column]];

                // Range 데이타를 배열 (One-based array)로
                object[,] data = (object[,])rng.Value;

                for (int r = 1; r <= data.GetLength(0); r++)
                {
                    int length = Math.Min(data.GetLength(1), numOfColumn);
                    string[] arr = new string[length];

                    for (int c = 1; c <= length; c++)
                    {
                        if (data[r, c] == null)
                        {
                            continue;
                        }
                        else if (data[r, c] is string)
                        {
                            arr[c - 1] = data[r, c] as string;
                        }
                        else
                        {
                            arr[c - 1] = data[r, c].ToString();
                        }
                    }

                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(arr[i]) == false)
                        {
                            result.Add(arr);
                            break;
                        }
                    }

                }

                wb.Close(true);
                excelApp.Quit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Clean up
                ReleaseExcelObject(ws);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(excelApp);
            }

            return result.ToArray();
        }

        /// <summary>
        /// Release excel object
        /// </summary>
        /// <param name="obj">target object to release</param>
        private static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception)
            {
                obj = null;
                throw;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}