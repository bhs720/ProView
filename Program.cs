﻿using System;
using System.Windows.Forms;

namespace ProView
{
    internal sealed class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new PVMainForm());
        }

    }
}
