using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace FinalProjSoftDes
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            // initialize the background picture
            pictureBox_stonks.Image = Properties.Resources.stonks;
            pictureBox_stonks.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_stonks.SendToBack();
            // select the first index for both combo boxes
            comboBoxCandlestick.SelectedIndex = 0;
            comboBoxTicker.SelectedIndex = 0;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // Set dates to total seconds for direct URL.
            DateTime minDate = new DateTime(1970, 1, 1);
            DateTime startDate = new DateTime(dateTimePickerStart.Value.Year,
                                              dateTimePickerStart.Value.Month,
                                              dateTimePickerStart.Value.Day);

            DateTime endDate = new DateTime(dateTimePickerEnd.Value.Year,
                                            dateTimePickerEnd.Value.Month,
                                            dateTimePickerEnd.Value.Day);

            var diffStart = startDate.Subtract(minDate);
            var diffEnd = endDate.Subtract(minDate);

            // Create a request for the URL.
            string webquery = "https://query1.finance.yahoo.com/v7/finance/download/" + comboBoxTicker.Text.ToString() + 
                              "?period1=" + diffStart.TotalSeconds.ToString() + 
                              "&period2=" + diffEnd.TotalSeconds.ToString() + 
                              "&interval=" + comboBoxCandlestick.Text.ToString() + 
                              "&events=history&includeAdjustedClose=true";


            Console.WriteLine(webquery);
            FormChart frmlaunch = new FormChart(sender, e, webquery);
            frmlaunch.Show();
        }
    }
}
