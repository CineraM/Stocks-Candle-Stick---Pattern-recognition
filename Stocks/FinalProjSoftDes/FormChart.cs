using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FinalProjSoftDes
{
    public partial class FormChart : Form
    {
        string webquery;
        private DataRow[] rows;
        private double x_range;
        private double y_range;
        List<RectangleAnnotation> remove = new List<RectangleAnnotation>();


        public FormChart(object sender, EventArgs e, string webquery)
        {
            InitializeComponent();
            this.webquery = webquery;
            pictureBox_stonks.Image = Properties.Resources.stonks2;
            pictureBox_stonks.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        /// <summary>
        /// Function that initializes the data for the canddlestick chart
        /// </summary>
        private void initialize_data()
        {
            // Create web request.
            WebRequest request = WebRequest.Create(webquery);
            // If required by the server, set the credentials.
            //request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            
            // Display the status.
            //Console.WriteLine(response.StatusDescription);

            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content.
            string responseFromServer = reader.ReadLine();

            // Datatable to store/display webquery.
            DataTable worktable = new DataTable();

            // Candlestick chart prep.
            Series price = new Series("price");

            // Setting up the graph variables 
            chart1.Series.Add(price);
            chart1.Series["price"].ChartType = SeriesChartType.Candlestick;
            chart1.Series["price"]["OpenCloseStyle"] = "Triangle";
            chart1.Series["price"]["ShowOpenClose"] = "Both";
            chart1.Series["price"]["PointWidth"] = "0.9";
            chart1.Series["price"]["PriceUpColor"] = "Green";
            chart1.Series["price"]["PriceDownColor"] = "Red";
            chart1.Series["price"].BorderWidth = 4;

            // Create dynamic Y range.
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;

            // Remove default series from chart.
            chart1.Series.RemoveAt(0);

            // Change the colors of the interlacing lines.
            chart1.ChartAreas[0].AxisX.InterlacedColor = Color.LightGray;
            chart1.ChartAreas[0].AxisY.InterlacedColor = Color.LightGray;

            // Change the color of the grid lines.
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;


            // Set DataTable columns.
            worktable.Columns.Add("Date", typeof(DateTime));
            worktable.Columns.Add("Open", typeof(double));
            worktable.Columns.Add("High", typeof(double));
            worktable.Columns.Add("Low", typeof(double));
            worktable.Columns.Add("Close", typeof(double));
            worktable.Columns.Add("Adj Close", typeof(double));
            worktable.Columns.Add("Volume", typeof(double));

            // Read information from URL and store in datatable.
            while ((responseFromServer = reader.ReadLine()) != null)
            {
                string[] data = responseFromServer.Split(',');
                worktable.Rows.Add(data);
            }

            // Read rows from datatable and update chart.
            rows = worktable.Select();

            double axis_high = Convert.ToDouble(rows[0][2]);
            double axis_low = Convert.ToDouble(rows[0][3]);

            for (int i = 0; i < rows.Length; i++)
            {
                if (Convert.ToDouble(rows[i][2]) > axis_high)
                    axis_high = Convert.ToDouble(rows[i][2]);

                if (Convert.ToDouble(rows[i][3]) < axis_low)
                    axis_low = Convert.ToDouble(rows[i][3]);

                // adding date and high
                chart1.Series["price"].Points.AddXY(rows[i][0], rows[i][2]);
                // adding low
                chart1.Series["price"].Points[i].YValues[1] = Convert.ToDouble(rows[i][3]);
                // adding open
                chart1.Series["price"].Points[i].YValues[2] = Convert.ToDouble(rows[i][1]);
                // adding close
                chart1.Series["price"].Points[i].YValues[3] = Convert.ToDouble(rows[i][4]);
            }
            // for drawing reactangle
            axis_high = Math.Truncate(axis_high) + 1;
            axis_low = Math.Truncate(axis_low) - 1;

            // calculated x and y axis range for plotting purpuses 
            // will use those values to plot the candlesticks later
            y_range = axis_high - axis_low;
            x_range = rows.Length;

            chart1.ChartAreas[0].AxisY.Maximum = axis_high;
            chart1.ChartAreas[0].AxisY.Minimum = axis_low;

            Console.WriteLine("X range: " + x_range + " Y range: " + y_range);

            // Populate gridview with datatable
            dataGridView1.DataSource = worktable;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
        }

        // initialize the data
        private void FormChart_Load(object sender, EventArgs e)
        {
            initialize_data();
        }
        // https://upload.wikimedia.org/wikipedia/commons/thumb/e/ea/Candlestick_chart_scheme_03-en.svg/1200px-Candlestick_chart_scheme_03-en.svg.png
        
        /*
         The following functions are used to identify all the required 
         candlestick patterns. The setup code for all of them is the same:
            1. Initialize a count variable to count the number of patterns found
            2. Initialize the open, close, high, low variables
            3. initialize the range: range = high - low
         */

        /// <summary>
        /// Indentify a doji. Create an extra couple of variables that 
        /// skew the open and close. Use the range to evaluate if the pattern found is a
        /// standard doji. 
        /// </summary>
        private void doji() {
            int count = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                Double d_open = Math.Round((Double)Convert.ToDouble(rows[i][1]), 1);
                Double d_close = Math.Round((Double)Convert.ToDouble(rows[i][4]), 1);

                Double open = (Double)Convert.ToDouble(rows[i][1]);
                Double close = (Double)Convert.ToDouble(rows[i][4]);
                Double high = Convert.ToDouble(rows[i][2]);
                Double low = Convert.ToDouble(rows[i][3]);

                Double upper;
                Double lower;

                if (close > open)
                {
                    upper = high - close;
                    lower = open - low;
                }
                else 
                {
                    upper = high - open;
                    lower = close - low;
                }

                double range = high - low;
                double doji_eval = Math.Abs(d_open - d_close);
                
                if (doji_eval >= 0 && doji_eval <= 0.4) // if open and close differ by around 0.4 decimals, use percentages for better accuracy
                {
                    if (upper >= range * 0.3 && lower >= range * 0.3)  // if upper and lower are in the same range (30% disparty)
                    {
                        double height = chart1.Series["price"].Points[i].YValues[0] - chart1.Series["price"].Points[i].YValues[1];
                        RectangleAnnotation annotation = new RectangleAnnotation();
                        draw_annotation(annotation, i, height, Color.Black);
                        count++;
                    }
                }
            }
            label1.Text = "# of patterns found: " + count.ToString();
        }

        /// <summary>
        /// Based on standard doji function. However the range of the upper
        /// and lower canddlestick needs to be withing 5% the middlepoint (bassically checking if the doji is in the middle)
        /// </summary>
        private void long_legged_doji()
        {
            int count = 0;

            for (int i = 0; i < rows.Length; i++)
            {

                Double d_open = Math.Round((Double)Convert.ToDouble(rows[i][1]), 1);
                Double d_close = Math.Round((Double)Convert.ToDouble(rows[i][4]), 1);

                Double open = (Double)Convert.ToDouble(rows[i][1]);
                Double close = (Double)Convert.ToDouble(rows[i][4]);
                Double high = Convert.ToDouble(rows[i][2]);
                Double low = Convert.ToDouble(rows[i][3]);

                Double upper;
                Double lower;

                if (close > open)
                {
                    upper = high - close;
                    lower = open - low;
                }
                else
                {
                    upper = high - open;
                    lower = close - low;
                }

                double range = high - low;
                double doji_eval = Math.Abs(d_open - d_close);

                if (doji_eval >= 0 && doji_eval <= 0.4)
                {
                    if (upper >= range * 0.45 && lower >= range * 0.45)           // if upper and lower are the same (15% disparty)
                    {
                        double height = chart1.Series["price"].Points[i].YValues[0] - chart1.Series["price"].Points[i].YValues[1];
                        RectangleAnnotation annotation = new RectangleAnnotation();
                        draw_annotation(annotation, i, height, Color.Black);
                        count++;
                    }
                }
            }
            label1.Text = "# of patterns found: " + count.ToString();
        }

        /// <summary>
        /// based on the standard doji fcuntion.
        /// check if the lower part of the candlestick covers most of the range
        /// </summary>
        private void dragonfly_doji()
        {
            int count = 0;

            for (int i = 0; i < rows.Length; i++)
            {
                Double d_open = Math.Round((Double)Convert.ToDouble(rows[i][1]), 1);
                Double d_close = Math.Round((Double)Convert.ToDouble(rows[i][4]), 1);
                Double open = (Double)Convert.ToDouble(rows[i][1]);
                Double close = (Double)Convert.ToDouble(rows[i][4]);
                Double high = Convert.ToDouble(rows[i][2]);
                Double low = Convert.ToDouble(rows[i][3]);
                Double upper;
                Double lower;

                if (close > open)
                {
                    upper = high - close;
                    lower = open - low;
                }
                else
                {
                    upper = high - open;
                    lower = close - low;
                }

                double dragonfly_compare = lower - upper;
                double doji_eval = Math.Abs(d_open - d_close);

                if (doji_eval >= 0 && doji_eval <= 0.4)
                {
                    if (dragonfly_compare >= 0.80) // if the lower doji covers 80 or more (20% disparty)
                    {
                        double height = chart1.Series["price"].Points[i].YValues[0] - chart1.Series["price"].Points[i].YValues[1];
                        RectangleAnnotation annotation = new RectangleAnnotation();
                        draw_annotation(annotation, i, height, Color.Black);
                        count++;
                    }
                }
            }
            label1.Text = "# of patterns found: " + count.ToString();
        }

        /// <summary>
        /// Based on the dragon fly doji.
        /// check if the upper part of the doji covers most of the range
        /// </summary>
        private void gravestone_doji()
        {
            int count = 0;

            for (int i = 0; i < rows.Length; i++)
            {

                Double d_open = Math.Round((Double)Convert.ToDouble(rows[i][1]), 1);
                Double d_close = Math.Round((Double)Convert.ToDouble(rows[i][4]), 1);

                Double open = (Double)Convert.ToDouble(rows[i][1]);
                Double close = (Double)Convert.ToDouble(rows[i][4]);
                Double high = Convert.ToDouble(rows[i][2]);
                Double low = Convert.ToDouble(rows[i][3]);

                Double upper;
                Double lower;

                if (close > open)
                {
                    upper = high - close;
                    lower = open - low;
                }
                else
                {
                    upper = high - open;
                    lower = close - low;
                }

                double dragonfly_compare = upper - lower;
                double doji_eval = Math.Abs(d_open - d_close);

                if (doji_eval >= 0 && doji_eval <= 0.4)
                {
                    if (dragonfly_compare >= 0.80) // uppser part covers 80% or more
                    {
                        double height = chart1.Series["price"].Points[i].YValues[0] - chart1.Series["price"].Points[i].YValues[1];
                        RectangleAnnotation annotation = new RectangleAnnotation();
                        draw_annotation(annotation, i, height, Color.Black);
                        count++;
                    }
                }
            }
            label1.Text = "# of patterns found: " + count.ToString();
        }

        /// <summary>
        /// Check if the upper and lower cover 10% or less of the range
        /// if close is larger than open, it is a bullish
        /// </summary>
        private void marubozu_green()
        {
            int count = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                Double open = (Double)Convert.ToDouble(rows[i][1]);
                Double close = (Double)Convert.ToDouble(rows[i][4]);
                Double high = Convert.ToDouble(rows[i][2]);
                Double low = Convert.ToDouble(rows[i][3]);

                Double upper;
                Double lower;

                if (close > open)
                {
                    upper = high - close;
                    lower = open - low;
                }
                else
                {
                    upper = high - open;
                    lower = close - low;
                }

                double range = high - low;
                // compare value should = 0 to get a pure marubozu
                if (upper <= range * 0.1 && lower <= range * 0.1) 
                {
                    if (close > open) {
                        double height = chart1.Series["price"].Points[i].YValues[0] - chart1.Series["price"].Points[i].YValues[1];
                        RectangleAnnotation annotation = new RectangleAnnotation();
                        draw_annotation(annotation, i, height, Color.Black);
                        count++;
                    }             
                }
            }
            label1.Text = "# of patterns found: " + count.ToString();
        }

        /// <summary>
        /// Check if the upper and lower cover 10% or less of the range
        /// if open is larger than close, it is a bearish
        /// </summary>
        private void marubozu_red()
        {
            int count = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                Double open = (Double)Convert.ToDouble(rows[i][1]);
                Double close = (Double)Convert.ToDouble(rows[i][4]);
                Double high = Convert.ToDouble(rows[i][2]);
                Double low = Convert.ToDouble(rows[i][3]);

                Double upper;
                Double lower;

                if (close > open)
                {
                    upper = high - close;
                    lower = open - low;
                }
                else
                {
                    upper = high - open;
                    lower = close - low;
                }

                double range = high - low;
                // compare value should = 0 to get a pure marubozu
                if (upper <= range * 0.1 && lower <= range * 0.1)
                {
                    if (close < open)
                    {
                        double height = chart1.Series["price"].Points[i].YValues[0] - chart1.Series["price"].Points[i].YValues[1];
                        RectangleAnnotation annotation = new RectangleAnnotation();
                        draw_annotation(annotation, i, height, Color.Black);
                        count++;
                    }
                }
            }
            label1.Text = "# of patterns found: " + count.ToString();
        }

        /// <summary>
        /// for this fun funciton check create another set of open, close... values
        /// for the second canddlesitck. Check if the previous canddlestick's range is 
        /// at least 50% larger and if its bearish. The current candle sitck needs to be bullish
        /// </summary>
        private void bearish_harami()
        {
            bool color_flag = Capture;
            Color annotation_color = Color.Black;

            
            int count = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                if (color_flag)
                    annotation_color = Color.Yellow;
                else
                    annotation_color = Color.Purple;

                Double open_1 = 0;
                Double close_1 = 0;
                Double high_1;
                Double low_1;
                Double upper_1;
                Double lower_1;
                double range_1 = 0;

                if (i > 0) {
                    open_1 = (Double)Convert.ToDouble(rows[i-1][1]);
                    close_1 = (Double)Convert.ToDouble(rows[i - 1][4]);
                    high_1 = Convert.ToDouble(rows[i - 1][2]);
                    low_1 = Convert.ToDouble(rows[i - 1][3]);

                    if (close_1 > open_1)
                    {
                        upper_1 = high_1 - close_1;
                        lower_1 = open_1 - low_1;
                    }
                    else
                    {
                        upper_1 = high_1 - open_1;
                        lower_1 = close_1 - low_1;
                    }

                    range_1 = high_1 - low_1;
                }

                Double open = (Double)Convert.ToDouble(rows[i][1]);
                Double close = (Double)Convert.ToDouble(rows[i][4]);
                Double high = Convert.ToDouble(rows[i][2]);
                Double low = Convert.ToDouble(rows[i][3]);
                Double upper;
                Double lower;

                if (close > open)
                {
                    upper = high - close;
                    lower = open - low;
                }
                else
                {
                    upper = high - open;
                    lower = close - low;
                }

                double range = high - low;
                
                if (range <= range_1 * 0.6 && i > 0)
                {
                    if (close_1 > open_1 && close < open)
                    {
                        double height = chart1.Series["price"].Points[i].YValues[0] - chart1.Series["price"].Points[i].YValues[1];
                        RectangleAnnotation annotation = new RectangleAnnotation();
                        draw_annotation(annotation, i, height, annotation_color);


                        height = chart1.Series["price"].Points[i-1].YValues[0] - chart1.Series["price"].Points[i - 1].YValues[1];
                        RectangleAnnotation annotation2 = new RectangleAnnotation();
                        draw_annotation(annotation2, i-1, height, annotation_color);
                        count++;

                        if (color_flag)
                            color_flag = false;
                        else
                            color_flag = true;
                    }
                }
            }
            label1.Text = "# of patterns found: " + count.ToString();
        }

        /// <summary>
        /// for this fun funciton check create another set of open, close... values
        /// for the second canddlesitck. Check if the previous canddlestick's range is 
        /// at least 50% larger and if its bullish. The current candle sitck needs to be bearsih
        /// </summary>
        private void bullish_harami()
        {
            bool color_flag = Capture;
            Color annotation_color = Color.Black;

            int count = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                if (color_flag)
                    annotation_color = Color.Yellow;
                else
                    annotation_color = Color.Purple;

                Double open_1 = 0;
                Double close_1 = 0;
                Double high_1;
                Double low_1;
                Double upper_1;
                Double lower_1;
                double range_1 = 0;

                if (i > 0)
                {
                    open_1 = (Double)Convert.ToDouble(rows[i - 1][1]);
                    close_1 = (Double)Convert.ToDouble(rows[i - 1][4]);
                    high_1 = Convert.ToDouble(rows[i - 1][2]);
                    low_1 = Convert.ToDouble(rows[i - 1][3]);

                    if (close_1 > open_1)
                    {
                        upper_1 = high_1 - close_1;
                        lower_1 = open_1 - low_1;
                    }
                    else
                    {
                        upper_1 = high_1 - open_1;
                        lower_1 = close_1 - low_1;
                    }

                    range_1 = high_1 - low_1;
                }

                Double open = (Double)Convert.ToDouble(rows[i][1]);
                Double close = (Double)Convert.ToDouble(rows[i][4]);
                Double high = Convert.ToDouble(rows[i][2]);
                Double low = Convert.ToDouble(rows[i][3]);
                Double upper;
                Double lower;

                if (close > open)
                {
                    upper = high - close;
                    lower = open - low;
                }
                else
                {
                    upper = high - open;
                    lower = close - low;
                }

                double range = high - low;

                if (range <= range_1 * 0.6 && i > 0)
                {
                    if (close_1 < open_1 && close > open)
                    {
                        double height = chart1.Series["price"].Points[i].YValues[0] - chart1.Series["price"].Points[i].YValues[1];
                        RectangleAnnotation annotation = new RectangleAnnotation();
                        draw_annotation(annotation, i, height, annotation_color);

                        height = chart1.Series["price"].Points[i - 1].YValues[0] - chart1.Series["price"].Points[i - 1].YValues[1];
                        RectangleAnnotation annotation2 = new RectangleAnnotation();
                        draw_annotation(annotation2, i-1, height, annotation_color);
                        count++;

                        if (color_flag)
                            color_flag = false;
                        else
                            color_flag = true;
                    }
                }
            }
            label1.Text = "# of patterns found: " + count.ToString();
        }

        /// <summary>
        /// highlights all canddlesticks
        /// </summary>
        private void pattern_testing() {
            for (int i = 0; i < rows.Length; i++)
            {
                Double high = Math.Round((Double)Convert.ToDouble(rows[i][1]), 1);
                Double low = Math.Round((Double)Convert.ToDouble(rows[i][4]), 1);
                double height = chart1.Series["price"].Points[i].YValues[0] - chart1.Series["price"].Points[i].YValues[1];
                Console.WriteLine("Zero: " + chart1.Series["price"].Points[i].YValues[0]);
                Console.WriteLine("One: " + chart1.Series["price"].Points[i].YValues[1]);
                RectangleAnnotation annotation = new RectangleAnnotation();

                draw_annotation(annotation, i, height, Color.Yellow);
                Console.WriteLine("high: " + high + " low: " + low);   
            }
        }

        /// <summary>
        /// remove all annotations and resets the label
        /// </summary>
        private void remove_annotations() 
        {
            foreach (RectangleAnnotation rectangle in chart1.Annotations)
            {
                remove.Add(rectangle);
            }

            foreach (var x in remove)
                chart1.Annotations.Remove(x);

            remove.Clear();
            label1.Text = "# of patterns found: ";
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            remove_annotations();
        }

        /// <summary>
        /// places the nnotation in the grapgh, passes:
        /// annotation: annotation object
        /// i: index of the rows (wich contains the data of the graph)
        /// height: height of the graph
        /// </summary>
        private void draw_annotation(RectangleAnnotation annotation , int i, double height, Color color)
        {
            annotation.BackColor = Color.Transparent;
            annotation.LineColor = color;
            annotation.ToolTip = "Pattern";
            annotation.Width = 50 / x_range;    // DONT CHANGE THE SIZE OF THE GRAPH
            annotation.Height = (height / y_range) * 80;  // THE SIZE OF EACH ANNOATION DEPEND ON THEM
            annotation.LineWidth = 2;
            annotation.AnchorOffsetY = -(annotation.Height);
            annotation.SetAnchor(chart1.Series["price"].Points[i]);
            annotation.IsSizeAlwaysRelative = true;
            chart1.Annotations.Add(annotation);
        }

        /// <summary>
        /// basic combobox to call each function to display
        /// </summary>
        private void comboBox_patterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            remove_annotations();
            int selected = comboBox_patterns.SelectedIndex;
            switch (selected) {
                case 0:     // default doji
                    doji();
                    break;
                case 1:     // long leged doji
                    long_legged_doji();
                    break;
                case 2:     // dragonfly doji
                    dragonfly_doji();
                    break;
                case 3:     // gravestone doji
                    gravestone_doji();
                    break;
                case 4:     // mazuburu bullish green 
                    marubozu_green();
                    break;
                case 5:     // mazuburu bearish red
                    marubozu_red();
                    break;
                case 6:     // bullish
                    bullish_harami();
                    break;
                case 7:     // bearish haramis
                    bearish_harami();
                    break;
                case 8:     
                    pattern_testing();
                    break;
            }
        }
    }
}
