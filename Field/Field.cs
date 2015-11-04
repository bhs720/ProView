/*
 * Created by SharpDevelop.
 * User: bsmith
 * Date: 10/10/2014
 * Time: 11:42 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ProView
{
	/// <summary>
	/// Description of Field.
	/// </summary>
	public class Field
	{
		public Field()
		{
			Source = new List<string>();
			Name = "Field1";
			DataType = DataType.Text;
			SourceMode = SourceMode.None;
		}
		
		public string Name { get; set; }
		public DataType DataType { get; set; }
		public List<string> Source { get; private set; }
		public SourceMode SourceMode { get; set; }
		public string SourceFile { get; set; }
		public bool WasModified { get; set; }

        public void Add(string item)
        {
            if (SourceMode != SourceMode.Append)
                return;
            if (Source.Contains(item, true))
                return;
            Source.Add(item);
            Source.Sort();
            WasModified = true;
        }
		
		public Field Clone()
		{
			var field = new Field();
			field.Name = Name;
			field.DataType = DataType;
			var source = new List<string>();							
			foreach (string item in Source)
				source.Add(item);
			field.Source = source;
			field.SourceMode = SourceMode;
			field.SourceFile = SourceFile;
			return field;
		}
	}
	
	public enum SourceMode
	{
		None, Adhere, Append
	}
	
	public enum DataType
	{
		Text, Date, Number
	}
}
