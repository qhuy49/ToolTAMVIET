﻿using System;
using System.Windows.Forms;
using MinvoiceLoadDataMisa.Forms;

namespace MinvoiceLoadDataMisa
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DevExpress.UserSkins.BonusSkins.Register();
DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2013");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new XtraForm1());}
    }
}
