using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace WriteExcelData
{
    public class Program
    {
        // for closing excel app
        // https://www.codeproject.com/Answers/74997/Close-Excel-Process-with-Interop#answer1
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);


        private const int ROW_COUNT = 11;

        public static void Main(string[] args)
        {
            Console.Write("Input Excel file path (if empty, set current location): ");
            string? input = Console.ReadLine();
            string path = !string.IsNullOrWhiteSpace(input) ? input : Path.GetFullPath("./Write ExcelData.xlsx");

            string[][] excelData = new string[ROW_COUNT][];
            excelData[0] = new string[] { "A", "B", "A * B" };

            Random rand = new Random();
            for (int i = 1; i < ROW_COUNT; i++)
            {
                int a = rand.Next(1, 10),
                    b = rand.Next(1, 10);
                excelData[i] = new string[3] { $"{a}", $"{b}", $"{a * b}" };
            }

            if (!WriteExcelData(path, excelData))
            {
                Console.WriteLine("Excel write error!");
                return;
            }

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < excelData.GetLength(0); i++)
            {
                result.AppendLine(string.Join("\t", excelData[i]));
            }
            result.ToString();

            Console.WriteLine(result);
            Console.WriteLine("Excel file is saved in...");
            Console.WriteLine(path);
        }


        /// <summary>
        /// Write data to excel file
        /// </summary>
        /// <param name="path">Excel file path</param>
        /// <param name="targetData">Data to write</param>
        /// <returns>Write Success or Not</returns>
        private static bool WriteExcelData(string path, string[][] targetData)
        {
            uint excelProcessId = 0;
            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                // Execute Excel application
                excelApp = new Excel.Application();
                GetWindowThreadProcessId(new IntPtr(excelApp.Hwnd), out excelProcessId);


                // Open or Save Excel file
                bool isFileExist = File.Exists(path);
                wb = isFileExist ? excelApp.Workbooks.Open(path, ReadOnly: false, Editable: true) : excelApp.Workbooks.Add(Missing.Value);

                ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;
                // Can set worksheet name
                // ws.Name = targetWorksheetName ;
                

                int row = targetData.GetLength(0);
                int column = targetData[0].Length;

                object[,] data = new object[row, column];

                for (int r = 0; r < row; r++)
                {
                    for (int c = 0; c < column; c++)
                    {
                        data[r, c] = targetData[r][c];
                    }
                }

                // Can access cells using row, cell numbers
                // Excel.Range rng = ws.Range[ws.Cells[1, 1], ws.Cells[row, column]];

                Excel.Range rng = ws.get_Range("A1", Missing.Value);
                rng = rng.get_Resize(row, column);

                // Save in many ways
                // rng.Value = data;
                rng.set_Value(Missing.Value, data);

                if (isFileExist)
                {
                    wb.Save(); // Overwrite
                }
                else
                {
                    wb.SaveCopyAs(path); // Generate new file
                }

                wb.Close(false);
                excelApp.Quit();
            }
            catch (Exception e)
            {
                string test = e.GetType().Name;
                if (wb != null)
                {
                    wb.Close(SaveChanges: false);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                }

                return false;
                throw;
            }
            finally
            {
                ReleaseExcelObject(ws);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(excelApp);

                if (excelApp != null && excelProcessId > 0)
                {
                    Process.GetProcessById((int)excelProcessId).Kill();
                }

            }

            return true;
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