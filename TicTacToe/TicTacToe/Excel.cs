using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace TicTacToe
{
    class Excel
    {
		private string path;
		_Application excel = new _Excel.Application();
		Workbook wb;
		Worksheet ws;
		int lastRow = 0;

		public void setLastRow(int newRow)
		{
			this.lastRow = newRow;
		}

		public int getLastRow()
		{
			return this.lastRow;
		}

		public Excel()
		{
			this.path = "";
		}
		public Excel(string newPath, int sheet)
		{
			this.path = newPath;
			if (!File.Exists(path))
			{
				this.wb = excel.Workbooks.Add(1);
				this.ws = (_Excel.Worksheet)wb.Worksheets.get_Item(1);
				this.ws.Cells[1, 1] = "Player 1";
				this.ws.Cells[1, 2] = "Player 2";
				this.ws.Cells[1, 3] = "Result";
				this.ws.Range["A1:C1"].ColumnWidth = 15;
				Range cells = wb.Worksheets[1].Cells;
				cells.NumberFormat = "@";
				this.wb.SaveAs(path);
			}
			this.wb = this.excel.Workbooks.Open(path, ReadOnly: false);
			this.ws = this.wb.Worksheets[sheet];
		}

		public string ReadCell(int row, int col)
		{
			if (row <= 0 || col <= 0)
			{
				return "Input cell doesn't exist";
			}
			else if (this.ws.Cells[row, col].Value2 == null)
			{
				return "Cell (" + row + "," + col + ") is empty";
			}
			return (this.ws.Cells[row, col].Value2).ToString();
		}

		public void WriteInCell(int row, int col, string str)
		{
			if (row <= 0 || col <= 0)
			{
				return;
			}
			this.ws.Cells[row, col].Value2 = str;
		}

		public void findLastRow()
		{
			bool isLast = false;
			int row = 1;
			int col = 1;
			while (!isLast)
			{
				if (this.ws.Cells[row, col].Value2 != null)
				{
					row++;
				}
				else
				{
					isLast = true;
				}
			}
			setLastRow(row);
		}

		public void Save()
		{
			this.wb.Save();
		}

		public void SaveAs(string path)
		{
			this.wb.SaveAs(path);
		}

		public void Close()
		{
			this.wb.Close();
		}

		public void Release()
		{
			Marshal.ReleaseComObject(excel);
		}

		public void Quit()
		{
			this.excel.Quit();
		}
	}
}
