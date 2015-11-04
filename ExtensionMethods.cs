/*
 * Created by SharpDevelop.
 * User: bsmith
 * Date: 10/31/2014
 * Time: 2:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;

namespace ProView
{
	/// <summary>
	/// Description of ExtensionMethods.
	/// </summary>
	public static class ExtensionMethods
	{
		public static string GetFormattedValue(this List<string> list, string term)
		{
			if (!string.IsNullOrWhiteSpace(term))
			{
				var comparison = StringComparison.CurrentCultureIgnoreCase;
				foreach (string item in list)
				{
					if (item.Equals(term, comparison))
						return item;
				}
			}
			return null;
		}
		
		public static bool Contains(this List<string> list, string termToCompare, bool ignoreCase)
		{
			if (!string.IsNullOrWhiteSpace(termToCompare))
			{
				var comparison = ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
				foreach (string item in list)
				{
					if (item.Equals(termToCompare, comparison))
						return true;
				}
			}
			return false;
		}
		
		public static List<string> Search(this List<string> list, string searchTerm, bool ignoreCase, out int bestMatchIndex)
		{
            bestMatchIndex = -1;
			var results = new List<string>();
			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				var comparison = ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None;
				foreach (string item in list)
				{
                    int charIndex = Application.CurrentCulture.CompareInfo.IndexOf(item, searchTerm, comparison);
                    if (charIndex >= 0)
                        results.Add(item);
                    if (charIndex == 0 && bestMatchIndex == -1)
                        bestMatchIndex = results.IndexOf(item);
				}
			}
			return results;
		}
		
		public static string GetConcatenatedColumnValue(this DataGridViewRow row, string delimiter)
		{
			if (row == null || delimiter == null)
				throw new NullReferenceException("One or more parameters were null");
			string concatenated = "";
			for (int i = 1; i < row.Cells.Count; i++)
			{
				string value = row.Cells[i].Value as string;
				if (!string.IsNullOrEmpty(value))
				{
					concatenated += row.Cells[i].Value.ToString();
					if (i != row.Cells.Count - 1)
						concatenated += delimiter;
				}
			}
			return concatenated;
		}
	}
}
