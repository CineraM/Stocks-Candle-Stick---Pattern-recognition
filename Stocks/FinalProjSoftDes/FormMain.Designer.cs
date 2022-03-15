
namespace FinalProjSoftDes
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.labelTicker = new System.Windows.Forms.Label();
            this.labelStartDate = new System.Windows.Forms.Label();
            this.labelEndDate = new System.Windows.Forms.Label();
            this.labelCandleStick = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.comboBoxCandlestick = new System.Windows.Forms.ComboBox();
            this.comboBoxTicker = new System.Windows.Forms.ComboBox();
            this.pictureBox_stonks = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_stonks)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTicker
            // 
            this.labelTicker.AutoSize = true;
            this.labelTicker.BackColor = System.Drawing.Color.Transparent;
            this.labelTicker.Location = new System.Drawing.Point(112, 25);
            this.labelTicker.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTicker.Name = "labelTicker";
            this.labelTicker.Size = new System.Drawing.Size(51, 17);
            this.labelTicker.TabIndex = 0;
            this.labelTicker.Text = "Ticker:";
            // 
            // labelStartDate
            // 
            this.labelStartDate.AutoSize = true;
            this.labelStartDate.BackColor = System.Drawing.Color.Transparent;
            this.labelStartDate.Location = new System.Drawing.Point(112, 89);
            this.labelStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStartDate.Name = "labelStartDate";
            this.labelStartDate.Size = new System.Drawing.Size(76, 17);
            this.labelStartDate.TabIndex = 1;
            this.labelStartDate.Text = "Start Date:";
            // 
            // labelEndDate
            // 
            this.labelEndDate.AutoSize = true;
            this.labelEndDate.BackColor = System.Drawing.Color.Transparent;
            this.labelEndDate.Location = new System.Drawing.Point(112, 151);
            this.labelEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEndDate.Name = "labelEndDate";
            this.labelEndDate.Size = new System.Drawing.Size(71, 17);
            this.labelEndDate.TabIndex = 5;
            this.labelEndDate.Text = "End Date:";
            // 
            // labelCandleStick
            // 
            this.labelCandleStick.AutoSize = true;
            this.labelCandleStick.BackColor = System.Drawing.Color.Transparent;
            this.labelCandleStick.Location = new System.Drawing.Point(112, 220);
            this.labelCandleStick.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCandleStick.Name = "labelCandleStick";
            this.labelCandleStick.Size = new System.Drawing.Size(129, 17);
            this.labelCandleStick.TabIndex = 6;
            this.labelCandleStick.Text = "Candlestick Period:";
            // 
            // buttonSearch
            // 
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSearch.Location = new System.Drawing.Point(115, 292);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(113, 35);
            this.buttonSearch.TabIndex = 9;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerStart.Location = new System.Drawing.Point(116, 108);
            this.dateTimePickerStart.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerStart.MaxDate = new System.DateTime(2021, 11, 21, 0, 0, 0, 0);
            this.dateTimePickerStart.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(113, 22);
            this.dateTimePickerStart.TabIndex = 10;
            this.dateTimePickerStart.Value = new System.DateTime(2021, 8, 15, 0, 0, 0, 0);
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(116, 171);
            this.dateTimePickerEnd.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerEnd.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(113, 22);
            this.dateTimePickerEnd.TabIndex = 11;
            this.dateTimePickerEnd.Value = new System.DateTime(2021, 10, 28, 0, 0, 0, 0);
            // 
            // comboBoxCandlestick
            // 
            this.comboBoxCandlestick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCandlestick.FormattingEnabled = true;
            this.comboBoxCandlestick.Items.AddRange(new object[] {
            "1d",
            "1wk",
            "1mo"});
            this.comboBoxCandlestick.Location = new System.Drawing.Point(116, 240);
            this.comboBoxCandlestick.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxCandlestick.Name = "comboBoxCandlestick";
            this.comboBoxCandlestick.Size = new System.Drawing.Size(113, 24);
            this.comboBoxCandlestick.TabIndex = 12;
            // 
            // comboBoxTicker
            // 
            this.comboBoxTicker.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxTicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTicker.FormattingEnabled = true;
            this.comboBoxTicker.Items.AddRange(new object[] {
            "AAPL",
            "MSFT",
            "NVDA",
            "AMD",
            "INTC",
            "FB",
            "TSLA",
            "AMZN",
            "AXP",
            "T"});
            this.comboBoxTicker.Location = new System.Drawing.Point(115, 46);
            this.comboBoxTicker.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxTicker.Name = "comboBoxTicker";
            this.comboBoxTicker.Size = new System.Drawing.Size(113, 24);
            this.comboBoxTicker.TabIndex = 15;
            // 
            // pictureBox_stonks
            // 
            this.pictureBox_stonks.InitialImage = global::FinalProjSoftDes.Properties.Resources.stonks;
            this.pictureBox_stonks.Location = new System.Drawing.Point(-14, -9);
            this.pictureBox_stonks.Name = "pictureBox_stonks";
            this.pictureBox_stonks.Size = new System.Drawing.Size(364, 382);
            this.pictureBox_stonks.TabIndex = 16;
            this.pictureBox_stonks.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 364);
            this.Controls.Add(this.pictureBox_stonks);
            this.Controls.Add(this.comboBoxTicker);
            this.Controls.Add(this.comboBoxCandlestick);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.labelCandleStick);
            this.Controls.Add(this.labelEndDate);
            this.Controls.Add(this.labelStartDate);
            this.Controls.Add(this.labelTicker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stonks";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_stonks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTicker;
        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.Label labelEndDate;
        private System.Windows.Forms.Label labelCandleStick;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.ComboBox comboBoxCandlestick;
        private System.Windows.Forms.ComboBox comboBoxTicker;
        private System.Windows.Forms.PictureBox pictureBox_stonks;
    }
}

