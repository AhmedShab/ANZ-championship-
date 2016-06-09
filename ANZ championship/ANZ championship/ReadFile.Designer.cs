namespace ANZ_championship
{
    partial class ReadFile
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.serviceController1 = new System.ServiceProcess.ServiceController();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.data_listBox = new System.Windows.Forms.ListBox();
            this.restart_button = new System.Windows.Forms.Button();
            this.wonLost_button = new System.Windows.Forms.Button();
            this.country_listBox = new System.Windows.Forms.ListBox();
            this.teamsComparisonbutton = new System.Windows.Forms.Button();
            this.teamcomboBox = new System.Windows.Forms.ComboBox();
            this.compareAlldatabutton = new System.Windows.Forms.Button();
            this.vsTeamcomboBox = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(132, 198);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.chart.Size = new System.Drawing.Size(909, 335);
            this.chart.TabIndex = 6;
            this.chart.Text = "chart";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // data_listBox
            // 
            this.data_listBox.FormattingEnabled = true;
            this.data_listBox.Items.AddRange(new object[] {
            "2008",
            "2009",
            "2010",
            "2011",
            "2012",
            "2013"});
            this.data_listBox.Location = new System.Drawing.Point(32, 8);
            this.data_listBox.Name = "data_listBox";
            this.data_listBox.Size = new System.Drawing.Size(85, 82);
            this.data_listBox.TabIndex = 15;
            this.data_listBox.SelectedIndexChanged += new System.EventHandler(this.data_listBox_SelectedIndexChanged);
            // 
            // restart_button
            // 
            this.restart_button.Location = new System.Drawing.Point(154, 169);
            this.restart_button.Name = "restart_button";
            this.restart_button.Size = new System.Drawing.Size(75, 23);
            this.restart_button.TabIndex = 16;
            this.restart_button.Text = "Reset";
            this.restart_button.UseVisualStyleBackColor = true;
            this.restart_button.Click += new System.EventHandler(this.restart_button_Click);
            // 
            // wonLost_button
            // 
            this.wonLost_button.Location = new System.Drawing.Point(26, 108);
            this.wonLost_button.Name = "wonLost_button";
            this.wonLost_button.Size = new System.Drawing.Size(91, 23);
            this.wonLost_button.TabIndex = 17;
            this.wonLost_button.Text = "Won/Lost Teams";
            this.wonLost_button.UseVisualStyleBackColor = true;
            this.wonLost_button.Click += new System.EventHandler(this.wonLost_button_Click);
            // 
            // country_listBox
            // 
            this.country_listBox.FormattingEnabled = true;
            this.country_listBox.Items.AddRange(new object[] {
            "New Zealand",
            "Australia"});
            this.country_listBox.Location = new System.Drawing.Point(154, 8);
            this.country_listBox.Name = "country_listBox";
            this.country_listBox.Size = new System.Drawing.Size(86, 82);
            this.country_listBox.TabIndex = 19;
            this.country_listBox.SelectedIndexChanged += new System.EventHandler(this.country_listBox_SelectedIndexChanged);
            // 
            // teamsComparisonbutton
            // 
            this.teamsComparisonbutton.Location = new System.Drawing.Point(144, 108);
            this.teamsComparisonbutton.Name = "teamsComparisonbutton";
            this.teamsComparisonbutton.Size = new System.Drawing.Size(105, 23);
            this.teamsComparisonbutton.TabIndex = 24;
            this.teamsComparisonbutton.Text = "Teams comparison";
            this.teamsComparisonbutton.UseVisualStyleBackColor = true;
            this.teamsComparisonbutton.Click += new System.EventHandler(this.teamsComparisonbutton_Click);
            // 
            // teamcomboBox
            // 
            this.teamcomboBox.FormattingEnabled = true;
            this.teamcomboBox.Items.AddRange(new object[] {
            "Central Pulse",
            "Northern Mystics",
            "Waikato Bay of Plenty Magic",
            "Southern Steel",
            "Canterbury Tactix",
            "New South Wales Swifts",
            "Adelaide Thunderbirds",
            "Melbourne Vixens",
            "West Coast Fever",
            "Queensland Firebirds"});
            this.teamcomboBox.Location = new System.Drawing.Point(278, 12);
            this.teamcomboBox.Name = "teamcomboBox";
            this.teamcomboBox.Size = new System.Drawing.Size(158, 21);
            this.teamcomboBox.TabIndex = 25;
            this.teamcomboBox.SelectedIndexChanged += new System.EventHandler(this.teamcomboBox_SelectedIndexChanged);
            // 
            // compareAlldatabutton
            // 
            this.compareAlldatabutton.Location = new System.Drawing.Point(32, 169);
            this.compareAlldatabutton.Name = "compareAlldatabutton";
            this.compareAlldatabutton.Size = new System.Drawing.Size(75, 23);
            this.compareAlldatabutton.TabIndex = 26;
            this.compareAlldatabutton.Text = "2008-2013";
            this.compareAlldatabutton.UseVisualStyleBackColor = true;
            this.compareAlldatabutton.Click += new System.EventHandler(this.compareAlldatabutton_Click);
            // 
            // vsTeamcomboBox
            // 
            this.vsTeamcomboBox.FormattingEnabled = true;
            this.vsTeamcomboBox.Items.AddRange(new object[] {
            "Central Pulse",
            "Northern Mystics",
            "Waikato Bay of Plenty Magic",
            "Southern Steel",
            "Canterbury Tactix",
            "New South Wales Swifts",
            "Adelaide Thunderbirds",
            "Melbourne Vixens",
            "West Coast Fever",
            "Queensland Firebirds"});
            this.vsTeamcomboBox.Location = new System.Drawing.Point(526, 12);
            this.vsTeamcomboBox.Name = "vsTeamcomboBox";
            this.vsTeamcomboBox.Size = new System.Drawing.Size(158, 21);
            this.vsTeamcomboBox.TabIndex = 27;
            this.vsTeamcomboBox.SelectedIndexChanged += new System.EventHandler(this.vsTeamcomboBox_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(460, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(38, 20);
            this.textBox1.TabIndex = 28;
            this.textBox1.Text = "VS";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(132, 539);
            this.chart2.Name = "chart2";
            this.chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.chart2.Size = new System.Drawing.Size(909, 347);
            this.chart2.TabIndex = 29;
            this.chart2.Text = "chart";
            // 
            // ReadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 898);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.vsTeamcomboBox);
            this.Controls.Add(this.compareAlldatabutton);
            this.Controls.Add(this.teamcomboBox);
            this.Controls.Add(this.teamsComparisonbutton);
            this.Controls.Add(this.country_listBox);
            this.Controls.Add(this.wonLost_button);
            this.Controls.Add(this.restart_button);
            this.Controls.Add(this.data_listBox);
            this.Controls.Add(this.chart);
            this.Name = "ReadFile";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ServiceProcess.ServiceController serviceController1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ListBox data_listBox;
        private System.Windows.Forms.Button restart_button;
        private System.Windows.Forms.Button wonLost_button;
        private System.Windows.Forms.ListBox country_listBox;
        private System.Windows.Forms.Button teamsComparisonbutton;
        private System.Windows.Forms.ComboBox teamcomboBox;
        private System.Windows.Forms.Button compareAlldatabutton;
        private System.Windows.Forms.ComboBox vsTeamcomboBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
    }
}

