using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;

namespace ANZ_championship
{
    public partial class ReadFile : Form
    {
        TextFieldParser parser;
        ArrayList matches;
        ArrayList teams; // team objects
        Dictionary<Team, String> country;
        Dictionary<Team, String> allData;
        Boolean drawWonLost = false;


        int round = 0;
        String date = "";
        String time = "";
        String homeTeam = "";
        int homeScore = 0;
        int awayScore = 0;
        String awayTeam = "";
        String venue = "";
        String winningTeam = "";

        // Match wonTeam;

        public ReadFile()
        {
            InitializeComponent();
            chart.Series.Clear();
        }

        private void data_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           // chart.Titles.Clear();
            //teamcomboBox.SelectedIndex = -1;
           // vsTeamcomboBox.SelectedIndex = -1;

            ///////////2008///////////
            if (data_listBox.SelectedIndex == 0)
            {
                parser = new TextFieldParser("2008-Table1.csv");
                build();
                parser.Close();


                parser = new TextFieldParser("2008-Table1.csv");
                generateData();
                parser.Close();



            }


            ///////////2009///////////
            if (data_listBox.SelectedIndex == 1)
            {
                parser = new TextFieldParser("2009-Table1.csv");
                build();
                parser.Close();


                parser = new TextFieldParser("2009-Table1.csv");
                generateData();
                parser.Close();



            }

            ///////////2010///////////
            if (data_listBox.SelectedIndex == 2)
            {
                parser = new TextFieldParser("2010-Table1.csv");
                build();


                parser = new TextFieldParser("2010-Table1.csv");
                generateData();



            }

            ///////////2011///////////
            if (data_listBox.SelectedIndex == 3)
            {
                parser = new TextFieldParser("2011-Table1.csv");
                build();


                parser = new TextFieldParser("2011-Table1.csv");
                generateData();


            }

            ///////////2012///////////
            if (data_listBox.SelectedIndex == 4)
            {
                parser = new TextFieldParser("2012-Table1.csv");
                build();


                parser = new TextFieldParser("2012-Table1.csv");
                generateData();


            }


            ///////////2013///////////
            if (data_listBox.SelectedIndex == 5)
            {
                parser = new TextFieldParser("2013-Table1.csv");
                build();


                parser = new TextFieldParser("2013-Table1.csv");
                generateData();


            }
        }

        private void country_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (data_listBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a season");
            }


            else
            {




                buildCountryTeam();

                // clear the chart
                foreach (var series in chart.Series)
                {
                    series.Points.Clear();
                }

                // clear the AxisX
                foreach (var charArea in chart.ChartAreas)
                {
                    charArea.AxisX.CustomLabels.Clear();
                }

                chart.Titles.Clear();
                chart.Series.Clear();

                chart.Series.Add("Won");
                chart.Series.Add("Lost");


                ///////////////Draw the graph//////////////////
                int index = 0;

                // NZ teams
                if (country_listBox.SelectedIndex == 0)
                {

                    foreach (KeyValuePair<Team, String> entry in country)
                    {
                        // Check how many times NZ won
                        if (entry.Value.Contains("NZ"))
                        {

                            chart.Series["Won"].Points.AddY(entry.Key.getAllWin());
                            chart.Series["Lost"].Points.AddY(entry.Key.getAllLose());

                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, entry.Key.getName());
                            index++;
                        }
                    }

                    chart.Titles.Add("Number of times New Zealand's Teams Won or Lost in " + data_listBox.Text);

                }


                //AU teams
                if (country_listBox.SelectedIndex == 1)
                {


                    foreach (KeyValuePair<Team, String> entry in country)
                    {
                        // Check how many times AU won
                        if (entry.Value.Contains("AU"))
                        {

                            chart.Series["Won"].Points.AddY(entry.Key.getAllWin());
                            chart.Series["Lost"].Points.AddY(entry.Key.getAllLose());

                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, entry.Key.getName());
                            index++;
                        }
                    }

                    chart.Titles.Add("Number of times Australia's Teams Won or Lost in " + data_listBox.Text);
                }
            }
        }




        // add all 10 teams into teams object
        public void build()
        {
            teams = new ArrayList();
            ArrayList temp = new ArrayList();


            parser.Delimiters = new String[] { "," };
            String[] row = parser.ReadFields();

            while (!parser.EndOfData)
            {
                String[] cols = parser.ReadFields();


                // for 2008 data
                if (row.Length == 9)
                {

                    if (cols[1].StartsWith("BYES:"))
                    {
                    }

                    else
                    {
                        if (teams.Count == 0)
                        {
                            teams.Add(new Team(cols[3]));
                            temp.Add(cols[3]);
                        }


                        if (!temp.Contains(cols[3]))
                        {
                            teams.Add(new Team(cols[3]));
                            temp.Add(cols[3]);
                        }

                    }

                }

                // the remaining data
                else
                {
                    if (cols[1].StartsWith("BYES:"))
                    {
                    }

                    else
                    {
                        if (teams.Count == 0)
                        {
                            teams.Add(new Team(cols[2]));
                            temp.Add(cols[2]);
                        }


                        if (!temp.Contains(cols[2]))
                        {
                            teams.Add(new Team(cols[2]));
                            temp.Add(cols[2]);
                        }

                    }
                }
            }

        }











        private void generateData()
        {
            parser.Delimiters = new String[] { "," };
            String[] firstRow = parser.ReadFields();
            matches = new ArrayList();



            while (!parser.EndOfData)
            {
                String[] row = parser.ReadFields();

                if (row[1].StartsWith("BYES:"))
                {
                }

                else
                {


                    this.buildTeamList(row);   // call build team list method  here cols
                    this.buildMatchDetail(row); // build match details

                }

            }

        }


        public void buildMatchDetail(String[] row)
        {

            // for 2008 data
            if (row.Length == 9)
            {

                round = int.Parse(row[0]);
                date = row[1];
                time = row[2];
                homeTeam = row[3];
                homeScore = int.Parse(row[4]);
                awayScore = int.Parse(row[5]);
                awayTeam = row[6];
                venue = row[7];
                winningTeam = row[8];

                Match m = new Match(round);
                m.setRoundNum(round);
                m.setDate(date);
                m.setTime(time);
                m.setHomeTeam(homeTeam);
                m.setHomeScore(homeScore);
                m.setAwayTeam(awayTeam);
                m.setAwayScore(awayScore);
                m.setVenue(venue);
                matches.Add(m);
            }

            // all the other data
            else
            {
                round = int.Parse(row[0]);
                date = row[1];
                homeTeam = row[2];
                homeScore = int.Parse(row[3]);
                awayScore = int.Parse(row[4]);
                awayTeam = row[5];
                venue = row[6];
                winningTeam = row[7];


                Match m = new Match(round);
                m.setRoundNum(round);
                m.setDate(date);
                m.setTime(time);
                m.setHomeTeam(homeTeam);
                m.setHomeScore(homeScore);
                m.setAwayTeam(awayTeam);
                m.setAwayScore(awayScore);
                m.setVenue(venue);
                matches.Add(m);
            }
        }



        public void buildTeamList(String[] row)
        {


            // for 2008 data
            if (row.Length == 9)
            {
                round = int.Parse(row[0]);
                date = row[1];
                time = row[2];
                homeTeam = row[3];
                homeScore = int.Parse(row[4]);
                awayScore = int.Parse(row[5]);
                awayTeam = row[6];
                venue = row[7];
                winningTeam = row[8];
            }

            // all the other data
            else
            {
                round = int.Parse(row[0]);
                date = row[1];
                homeTeam = row[2];
                homeScore = int.Parse(row[3]);
                awayScore = int.Parse(row[4]);
                awayTeam = row[5];
                venue = row[6];
                winningTeam = row[7];
            }



            foreach (Team t in teams)
            {

                // Check first if the team in the list is the home team or not
                if (t.getName().Equals(homeTeam))
                {


                    // if it is then see if they're winning or lossing 
                    if (t.getName().Equals(winningTeam))
                    {
                        t.setHomeWin(1);

                    }
                    else
                    {
                        t.setHomelose(1);

                    }




                }

                // else check with the same thing with the away team
                else if (t.getName().Equals(awayTeam))
                {

                    if (t.getName().Equals(winningTeam))
                    {
                        t.setAwayWin(1);
                    }

                    else
                    {
                        t.setAwaylose(1);
                    }
                }

            }

            // after buiding teams, set add up the teams won and lost
            foreach (Team t in teams)
            {
                t.setAllWin(t.getHomeWin() + t.getAwayWin());
                t.setAllLose(t.getHomelose() + t.getAwaylose());
            }
        }

        private void restart_button_Click(object sender, EventArgs e)
        {

            // clear the chart
            foreach (var series in chart.Series)
            {
                series.Points.Clear();
            }

            // clear the AxisX
            foreach (var charArea in chart.ChartAreas)
            {
                charArea.AxisX.CustomLabels.Clear();
            }

            chart.Series.Clear();
            chart.Titles.Clear();


            // clear2 the chart
            foreach (var series in chart2.Series)
            {
                series.Points.Clear();
            }

            // clear the AxisX
            foreach (var charArea in chart.ChartAreas)
            {
                charArea.AxisX.CustomLabels.Clear();
            }

            chart2.Series.Clear();
            chart2.Titles.Clear();


            this.drawWonLost = false;
            
            
            // clear the selected list
            data_listBox.ClearSelected();
            country_listBox.ClearSelected();
            teamcomboBox.SelectedIndex = -1;
            vsTeamcomboBox.SelectedIndex = -1;

        }

        private void buildCountryTeam()
        {

            country = new Dictionary<Team, String>();

            foreach (Team t in teams)
            {
                // find NZ teams
                if (t.getName().Equals("Central Pulse") || t.getName().Equals("Northern Mystics") || t.getName().Equals("Waikato Bay of Plenty Magic")
                    || t.getName().Equals("Southern Steel") || t.getName().Equals("Canterbury Tactix"))
                {
                    country.Add(t, "NZ");
                }

                else
                {
                    country.Add(t, "AU");
                }
            }
        }

        private void teamcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (data_listBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a season");
            }


            else
            {

                // clear the chart
                foreach (var series in chart.Series)
                {
                    series.Points.Clear();
                }

                // clear the AxisX
                foreach (var charArea in chart.ChartAreas)
                {
                    charArea.AxisX.CustomLabels.Clear();
                }

                chart.Series.Clear();
                chart.Titles.Clear();

                drawWonLost = false;


                int index = 0;

                

                // Central Pulse
                if (teamcomboBox.SelectedIndex == 0)
                {
                   chart.Series.Add("Central Pulse");
                   chart.Series["Central Pulse"].ChartType = SeriesChartType.Spline;
                   chart.Series["Central Pulse"].MarkerStyle = MarkerStyle.Diamond;
                   chart.Series["Central Pulse"].MarkerBorderWidth = 10;
                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Central Pulse"].Points.AddY(m.getHomeScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Central Pulse"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Central Pulse"].Points.AddY(m.getAwayScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Central Pulse"].Points[index].ToolTip = m.getInfo();
                            index++;

                        }
                    }

                    chart.Titles.Add("The score for team " + teamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                // Northern Mystics
                else if (teamcomboBox.SelectedIndex == 1)
                {
                    chart.Series.Add("Northern Mystics");
                    chart.Series["Northern Mystics"].ChartType = SeriesChartType.Spline;
                    chart.Series["Northern Mystics"].MarkerStyle = MarkerStyle.Diamond;
                    chart.Series["Northern Mystics"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Northern Mystics"].Points.AddY(m.getHomeScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Northern Mystics"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Northern Mystics"].Points.AddY(m.getAwayScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Northern Mystics"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    //chart.Series["Northern Mystics"].Points.
                    }

                    chart.Titles.Add("The score for team " + teamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                // Waikato Bay of Plenty Magic
                else if (teamcomboBox.SelectedIndex == 2)
                {
                    chart.Series.Add("Waikato Bay of Plenty Magic");
                    chart.Series["Waikato Bay of Plenty Magic"].ChartType = SeriesChartType.Spline;
                    chart.Series["Waikato Bay of Plenty Magic"].MarkerStyle = MarkerStyle.Diamond;
                    chart.Series["Waikato Bay of Plenty Magic"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Waikato Bay of Plenty Magic"].Points.AddY(m.getHomeScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Waikato Bay of Plenty Magic"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Waikato Bay of Plenty Magic"].Points.AddY(m.getAwayScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Waikato Bay of Plenty Magic"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart.Titles.Add("The score for team " + teamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                // Southern Steel
                else if (teamcomboBox.SelectedIndex == 3)
                {
                    chart.Series.Add("Southern Steel");
                    chart.Series["Southern Steel"].ChartType = SeriesChartType.Spline;
                    chart.Series["Southern Steel"].MarkerStyle = MarkerStyle.Diamond;
                    chart.Series["Southern Steel"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Southern Steel"].Points.AddY(m.getHomeScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Southern Steel"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Southern Steel"].Points.AddY(m.getAwayScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Southern Steel"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart.Titles.Add("The score for team " + teamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                // Canterbury Tactix
                else if (teamcomboBox.SelectedIndex == 4)
                {
                    chart.Series.Add("Canterbury Tactix");
                    chart.Series["Canterbury Tactix"].ChartType = SeriesChartType.Spline;
                    chart.Series["Canterbury Tactix"].MarkerStyle = MarkerStyle.Diamond;
                    chart.Series["Canterbury Tactix"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Canterbury Tactix"].Points.AddY(m.getHomeScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Canterbury Tactix"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Canterbury Tactix"].Points.AddY(m.getAwayScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Canterbury Tactix"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart.Titles.Add("The score for team " + teamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");
                }

                // New South Wales Swifts
                else if (teamcomboBox.SelectedIndex == 5)
                {
                    chart.Series.Add("New South Wales Swifts");
                    chart.Series["New South Wales Swifts"].ChartType = SeriesChartType.Spline;
                    chart.Series["New South Wales Swifts"].MarkerStyle = MarkerStyle.Diamond;
                    chart.Series["New South Wales Swifts"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["New South Wales Swifts"].Points.AddY(m.getHomeScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["New South Wales Swifts"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["New South Wales Swifts"].Points.AddY(m.getAwayScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["New South Wales Swifts"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart.Titles.Add("The score for team " + teamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                 // Adelaide Thunderbirds
                else if (teamcomboBox.SelectedIndex == 6)
                {
                    chart.Series.Add("Adelaide Thunderbirds");
                    chart.Series["Adelaide Thunderbirds"].ChartType = SeriesChartType.Spline;
                    chart.Series["Adelaide Thunderbirds"].MarkerStyle = MarkerStyle.Diamond;
                    chart.Series["Adelaide Thunderbirds"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Adelaide Thunderbirds"].Points.AddY(m.getHomeScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Adelaide Thunderbirds"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Adelaide Thunderbirds"].Points.AddY(m.getAwayScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Adelaide Thunderbirds"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart.Titles.Add("The score for team " + teamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                 // Melbourne Vixens
                else if (teamcomboBox.SelectedIndex == 7)
                {
                    chart.Series.Add("Melbourne Vixens");
                    chart.Series["Melbourne Vixens"].ChartType = SeriesChartType.Spline;
                    chart.Series["Melbourne Vixens"].MarkerStyle = MarkerStyle.Diamond;
                    chart.Series["Melbourne Vixens"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Melbourne Vixens"].Points.AddY(m.getHomeScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Melbourne Vixens"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Melbourne Vixens"].Points.AddY(m.getAwayScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Melbourne Vixens"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart.Titles.Add("The score for team " + teamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                // West Coast Fever
                else if (teamcomboBox.SelectedIndex == 8)
                {
                    chart.Series.Add("West Coast Fever");
                    chart.Series["West Coast Fever"].ChartType = SeriesChartType.Spline;
                    chart.Series["West Coast Fever"].MarkerStyle = MarkerStyle.Diamond;
                    chart.Series["West Coast Fever"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["West Coast Fever"].Points.AddY(m.getHomeScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["West Coast Fever"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["West Coast Fever"].Points.AddY(m.getAwayScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["West Coast Fever"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart.Titles.Add("The score for team " + teamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                // Queensland Firebirds
                else if (teamcomboBox.SelectedIndex == 9)
                {
                    chart.Series.Add("Queensland Firebirds");
                    chart.Series["Queensland Firebirds"].ChartType = SeriesChartType.Spline;
                    chart.Series["Queensland Firebirds"].MarkerStyle = MarkerStyle.Diamond;
                    chart.Series["Queensland Firebirds"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Queensland Firebirds"].Points.AddY(m.getHomeScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Queensland Firebirds"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(teamcomboBox.SelectedItem))
                        {
                            chart.Series["Queensland Firebirds"].Points.AddY(m.getAwayScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart.Series["Queensland Firebirds"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart.Titles.Add("The score for team " + teamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }
            }
            
        }

        private void wonLost_button_Click(object sender, EventArgs e)
        {

            if (data_listBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a season");
            }


            else
            {


                // clear the chart
                foreach (var series in chart.Series)
                {
                    series.Points.Clear();
                }

                // clear the AxisX
                foreach (var charArea in chart.ChartAreas)
                {
                    charArea.AxisX.CustomLabels.Clear();
                }

                chart.Titles.Clear();


                int index = 0;

                if (drawWonLost == false)
                {
                    chart.Series.Clear();
                    chart.Series.Add("Won");
                    chart.Series.Add("Lost");
                    chart.Series["Won"].ChartType = SeriesChartType.Column;
                    chart.Series["Lost"].ChartType = SeriesChartType.Column;
                    drawWonLost = true;

                }

                drawWonLost = false;

                if (chart.Series.Contains(chart.Series[0]))
                {
 
                }


                ///////////2008///////////
                if (data_listBox.SelectedIndex == 0)
                {

                    chart.Titles.Add("Number of times each team won or lost in " + data_listBox.Text);

                    foreach (Team t in teams)
                    {
                        chart.Series["Won"].Points.AddY(t.getAllWin());
                        chart.Series["Lost"].Points.AddY(t.getAllLose());

                        chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, t.getName());
                        index++;
                    }
                }



                ///////////2009///////////
                if (data_listBox.SelectedIndex == 1)
                {

                    chart.Titles.Add("Number of times each team won or lost in " + data_listBox.Text);

                    foreach (Team t in teams)
                    {
                        chart.Series["Won"].Points.AddY(t.getAllWin());
                        chart.Series["Lost"].Points.AddY(t.getAllLose());

                        chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, t.getName());
                        index++;
                    }


                }

                ///////////2010///////////
                if (data_listBox.SelectedIndex == 2)
                {
                    chart.Titles.Add("Number of times each team won or lost in " + data_listBox.Text);

                    foreach (Team t in teams)
                    {
                        chart.Series["Won"].Points.AddY(t.getAllWin());
                        chart.Series["Lost"].Points.AddY(t.getAllLose());

                        chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, t.getName());
                        index++;
                    }


                }

                ///////////2011///////////
                if (data_listBox.SelectedIndex == 3)
                {
                    chart.Titles.Add("Number of times each team won or lost in " + data_listBox.Text);

                    foreach (Team t in teams)
                    {
                        chart.Series["Won"].Points.AddY(t.getAllWin());
                        chart.Series["Lost"].Points.AddY(t.getAllLose());

                        chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, t.getName());
                        index++;
                    }


                }

                ///////////2012///////////
                if (data_listBox.SelectedIndex == 4)
                {
                    chart.Titles.Add("Number of times each team won or lost in " + data_listBox.Text);

                    foreach (Team t in teams)
                    {
                        chart.Series["Won"].Points.AddY(t.getAllWin());
                        chart.Series["Lost"].Points.AddY(t.getAllLose());

                        chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, t.getName());
                        index++;
                    }


                }


                ///////////2013///////////
                if (data_listBox.SelectedIndex == 5)
                {
                    chart.Titles.Add("Number of times each team won or lost in " + data_listBox.Text);

                    foreach (var charArea in chart.ChartAreas)
                    {
                        charArea.AxisX.CustomLabels.Clear();
                    }



                    foreach (Team t in teams)
                    {
                        chart.Series["Won"].Points.AddY(t.getAllWin());
                        chart.Series["Lost"].Points.AddY(t.getAllLose());

                        chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, t.getName());
                        index++;
                    }
                }
            }
        }

        private void teamsComparisonbutton_Click(object sender, EventArgs e)
        {
            // clear the chart
            foreach (var series in chart.Series)
            {
                series.Points.Clear();
            }

            // clear the AxisX
            foreach (var charArea in chart.ChartAreas)
            {
                charArea.AxisX.CustomLabels.Clear();
            }

            chart.Series.Clear();
            chart.Titles.Clear();
            drawWonLost = false;


            if (data_listBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a season");
            }


            else
            {


                // Central Pulse
                chart.Series.Add("Central Pulse");
                chart.Series["Central Pulse"].ChartType = SeriesChartType.Bar;

                // Northern Mystics
                chart.Series.Add("Northern Mystics");
                chart.Series["Northern Mystics"].ChartType = SeriesChartType.Bar;

                // Waikato Bay of Plenty Magic
                chart.Series.Add("Waikato Bay of Plenty Magic");
                chart.Series["Waikato Bay of Plenty Magic"].ChartType = SeriesChartType.Bar;

                // Southern Steel
                chart.Series.Add("Southern Steel");
                chart.Series["Southern Steel"].ChartType = SeriesChartType.Bar;


                // Canterbury Tactix
                chart.Series.Add("Canterbury Tactix");
                chart.Series["Canterbury Tactix"].ChartType = SeriesChartType.Bar;

                // New South Wales Swifts
                chart.Series.Add("New South Wales Swifts");
                chart.Series["New South Wales Swifts"].ChartType = SeriesChartType.Bar;


                // Adelaide Thunderbirds

                chart.Series.Add("Adelaide Thunderbirds");
                chart.Series["Adelaide Thunderbirds"].ChartType = SeriesChartType.Bar;

                // Melbourne Vixens
                chart.Series.Add("Melbourne Vixens");
                chart.Series["Melbourne Vixens"].ChartType = SeriesChartType.Bar;


                // West Coast Fever
                chart.Series.Add("West Coast Fever");
                chart.Series["West Coast Fever"].ChartType = SeriesChartType.Bar;


                // Queensland Firebirds
                chart.Series.Add("Queensland Firebirds");
                chart.Series["Queensland Firebirds"].ChartType = SeriesChartType.Bar;

                int totScoreTeam1 = 0;
                int totScoreTeam2 = 0;
                int totScoreTeam3 = 0;
                int totScoreTeam4 = 0;
                int totScoreTeam5 = 0;
                int totScoreTeam6 = 0;
                int totScoreTeam7 = 0;
                int totScoreTeam8 = 0;
                int totScoreTeam9 = 0;
                int totScoreTeam10 = 0;

                foreach (Match m in matches)
                {
                    if (m.getHomeTeam().Equals("Central Pulse"))
                    {

                        totScoreTeam1 += m.getHomeScore();
                    }

                    if (m.getAwayTeam().Equals("Central Pulse"))
                    {
                        totScoreTeam1 += m.getAwayScore();
                    }


                    if (m.getHomeTeam().Equals("Northern Mystics"))
                    {

                        totScoreTeam2 += m.getHomeScore();
                    }

                    if (m.getAwayTeam().Equals("Northern Mystics"))
                    {

                        totScoreTeam2 += m.getAwayScore();
                    }



                    if (m.getHomeTeam().Equals("Waikato Bay of Plenty Magic"))
                    {

                        totScoreTeam3 += m.getHomeScore();
                    }

                    if (m.getAwayTeam().Equals("Waikato Bay of Plenty Magic"))
                    {

                        totScoreTeam3 += m.getAwayScore();
                    }


                    if (m.getHomeTeam().Equals("Southern Steel"))
                    {


                        totScoreTeam4 += m.getHomeScore();
                    }

                    if (m.getAwayTeam().Equals("Southern Steel"))
                    {

                        totScoreTeam4 += m.getAwayScore();
                    }



                    if (m.getHomeTeam().Equals("Canterbury Tactix"))
                    {

                        totScoreTeam5 += m.getHomeScore();
                    }

                    if (m.getAwayTeam().Equals("Canterbury Tactix"))
                    {

                        totScoreTeam5 += m.getAwayScore();
                    }



                    if (m.getHomeTeam().Equals("New South Wales Swifts"))
                    {

                        totScoreTeam6 += m.getHomeScore();
                    }

                    if (m.getAwayTeam().Equals("New South Wales Swifts"))
                    {

                        totScoreTeam6 += m.getAwayScore();
                    }


                    if (m.getHomeTeam().Equals("Adelaide Thunderbirds"))
                    {

                        totScoreTeam7 += m.getHomeScore();
                    }

                    if (m.getAwayTeam().Equals("Adelaide Thunderbirds"))
                    {

                        totScoreTeam7 += m.getAwayScore();
                    }


                    if (m.getHomeTeam().Equals("Melbourne Vixens"))
                    {

                        totScoreTeam8 += m.getHomeScore();
                    }

                    if (m.getAwayTeam().Equals("Melbourne Vixens"))
                    {

                        totScoreTeam8 += m.getAwayScore();
                    }



                    if (m.getHomeTeam().Equals("West Coast Fever"))
                    {

                        totScoreTeam9 += m.getHomeScore();
                    }

                    if (m.getAwayTeam().Equals("West Coast Fever"))
                    {

                        totScoreTeam9 += m.getAwayScore();

                    }



                    if (m.getHomeTeam().Equals("Queensland Firebirds"))
                    {

                        totScoreTeam10 += m.getHomeScore();
                    }

                    if (m.getAwayTeam().Equals("Queensland Firebirds"))
                    {
                        totScoreTeam10 += m.getAwayScore();
                    }
                }


                chart.Series["Central Pulse"].Points.AddY(totScoreTeam1);
                chart.Series["Northern Mystics"].Points.AddY(totScoreTeam2);
                chart.Series["Waikato Bay of Plenty Magic"].Points.AddY(totScoreTeam3);
                chart.Series["Southern Steel"].Points.AddY(totScoreTeam4);
                chart.Series["Canterbury Tactix"].Points.AddY(totScoreTeam5);
                chart.Series["New South Wales Swifts"].Points.AddY(totScoreTeam6);
                chart.Series["Adelaide Thunderbirds"].Points.AddY(totScoreTeam7);
                chart.Series["Melbourne Vixens"].Points.AddY(totScoreTeam8);
                chart.Series["West Coast Fever"].Points.AddY(totScoreTeam9);
                chart.Series["Queensland Firebirds"].Points.AddY(totScoreTeam10);

                chart.ChartAreas[0].AxisX.CustomLabels.Add(0 + 0.5, 0 + 1.5, "Total Score");
                chart.Titles.Add("The overall number of Scores for each team in " + data_listBox.Text);

            }
        }

        private void compareAlldatabutton_Click(object sender, EventArgs e)
        {

            allData = new Dictionary<Team, String>();
            String year = "2008";
            
            /////////////2008/////////////////
            parser = new TextFieldParser("2008-Table1.csv");
            build();
            parser.Close();

            parser = new TextFieldParser("2008-Table1.csv");
            generateData();
            parser.Close();

            buildAllData(year);


            /////////////2009/////////////////
            parser = new TextFieldParser("2009-Table1.csv");
            build();
            parser.Close();

            parser = new TextFieldParser("2009-Table1.csv");
            generateData();
            parser.Close();

            year = "2009";
            buildAllData(year);


            /////////////2010/////////////////
            parser = new TextFieldParser("2010-Table1.csv");
            build();
            parser.Close();

            parser = new TextFieldParser("2010-Table1.csv");
            generateData();
            parser.Close();

            year = "2010";
            buildAllData(year);


            /////////////2011/////////////////
            parser = new TextFieldParser("2011-Table1.csv");
            build();
            parser.Close();

            parser = new TextFieldParser("2011-Table1.csv");
            generateData();
            parser.Close();

            year = "2011";
            buildAllData(year);


            /////////////2012/////////////////
            parser = new TextFieldParser("2012-Table1.csv");
            build();
            parser.Close();

            parser = new TextFieldParser("2012-Table1.csv");
            generateData();
            parser.Close();

            year = "2012";
            buildAllData(year);


            /////////////2013/////////////////
            parser = new TextFieldParser("2013-Table1.csv");
            build();
            parser.Close();

            parser = new TextFieldParser("2013-Table1.csv");
            generateData();
            parser.Close();

            year = "2013";
            buildAllData(year);





            int totalTeam1Won = 0;
            int totalTeam1Lost = 0;

            int totalTeam2Won = 0;
            int totalTeam2Lost = 0;

            int totalTeam3Won = 0;
            int totalTeam3Lost = 0;

            int totalTeam4Won = 0;
            int totalTeam4Lost = 0;

            int totalTeam5Won = 0;
            int totalTeam5Lost = 0;

            int totalTeam6Won = 0;
            int totalTeam6Lost = 0;

            int totalTeam7Won = 0;
            int totalTeam7Lost = 0;

            int totalTeam8Won = 0;
            int totalTeam8Lost = 0;

            int totalTeam9Won = 0;
            int totalTeam9Lost = 0;

            int totalTeam10Won = 0;
            int totalTeam10Lost = 0;



            foreach (KeyValuePair<Team, String> entry in allData)
            {
                if (entry.Key.getName().Equals("Central Pulse"))
                {
                    totalTeam1Won += entry.Key.getAllWin();
                    totalTeam1Lost += entry.Key.getAllLose();
                }

                else if (entry.Key.getName().Equals("Northern Mystics"))
                {
                    totalTeam2Won += entry.Key.getAllWin();
                    totalTeam2Lost += entry.Key.getAllLose();
                }

                else if (entry.Key.getName().Equals("Waikato Bay of Plenty Magic"))
                {
                    totalTeam3Won += entry.Key.getAllWin();
                    totalTeam3Lost += entry.Key.getAllLose();
                }

                else if (entry.Key.getName().Equals("Southern Steel"))
                {
                    totalTeam4Won += entry.Key.getAllWin();
                    totalTeam4Lost += entry.Key.getAllLose();
                }

                else if (entry.Key.getName().Equals("Canterbury Tactix"))
                {
                    totalTeam5Won += entry.Key.getAllWin();
                    totalTeam5Lost += entry.Key.getAllLose();
                }

                else if (entry.Key.getName().Equals("New South Wales Swifts"))
                {
                    totalTeam6Won += entry.Key.getAllWin();
                    totalTeam6Lost += entry.Key.getAllLose();
                }


                else if (entry.Key.getName().Equals("Adelaide Thunderbirds"))
                {
                    totalTeam7Won += entry.Key.getAllWin();
                    totalTeam7Lost += entry.Key.getAllLose();
                }

                else if (entry.Key.getName().Equals("Melbourne Vixens"))
                {
                    totalTeam8Won += entry.Key.getAllWin();
                    totalTeam8Lost += entry.Key.getAllLose();
                }

                else if (entry.Key.getName().Equals("West Coast Fever"))
                {
                    totalTeam9Won += entry.Key.getAllWin();
                    totalTeam9Lost += entry.Key.getAllLose();
                }

                else if (entry.Key.getName().Equals("Queensland Firebirds"))
                {
                    totalTeam10Won += entry.Key.getAllWin();
                    totalTeam10Lost += entry.Key.getAllLose();
                }

            }

            ////////////////////Draw All data here//////////////////////

            // clear the chart
            foreach (var series in chart.Series)
            {
                series.Points.Clear();
            }

            // clear the AxisX
            foreach (var charArea in chart.ChartAreas)
            {
                charArea.AxisX.CustomLabels.Clear();
            }

            chart.Series.Clear();
            chart.Titles.Clear();

            chart.Titles.Add("Number of times each team won or lost overall in 2008-2013");


            int index = 0;
            chart.Series.Add("Won");
            chart.Series.Add("Lost");
            drawWonLost = true;



            
            ////////////team1/////////////
            chart.Series["Won"].Points.AddY(totalTeam1Won);
            chart.Series["Lost"].Points.AddY(totalTeam1Lost);

            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, "Central Pulse");
            index++;



            ////////////team2/////////////
            chart.Series["Won"].Points.AddY(totalTeam2Won);
            chart.Series["Lost"].Points.AddY(totalTeam2Lost);

            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, "Northern Mystics");
            index++;
            
            
            
            ////////////team3/////////////
            chart.Series["Won"].Points.AddY(totalTeam3Won);
            chart.Series["Lost"].Points.AddY(totalTeam3Lost);

            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, "Waikato Bay of Plenty Magic");
            index++;


            ////////////team4/////////////
            chart.Series["Won"].Points.AddY(totalTeam4Won);
            chart.Series["Lost"].Points.AddY(totalTeam4Lost);

            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, "Southern Steel");
            index++;


            ////////////team5/////////////
            chart.Series["Won"].Points.AddY(totalTeam5Won);
            chart.Series["Lost"].Points.AddY(totalTeam5Lost);

            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, "Canterbury Tactix");
            index++;

            ////////////team6/////////////
            chart.Series["Won"].Points.AddY(totalTeam6Won);
            chart.Series["Lost"].Points.AddY(totalTeam6Lost);

            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, "New South Wales Swifts");
            index++;

            ////////////team7/////////////
            chart.Series["Won"].Points.AddY(totalTeam7Won);
            chart.Series["Lost"].Points.AddY(totalTeam7Lost);

            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, "Adelaide Thunderbirds");
            index++;

            ////////////team8/////////////
            chart.Series["Won"].Points.AddY(totalTeam8Won);
            chart.Series["Lost"].Points.AddY(totalTeam8Lost);

            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, "Melbourne Vixens");
            index++;


            ////////////team9/////////////
            chart.Series["Won"].Points.AddY(totalTeam9Won);
            chart.Series["Lost"].Points.AddY(totalTeam9Lost);

            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, "West Coast Fever");
            index++;

            ////////////team10/////////////
            chart.Series["Won"].Points.AddY(totalTeam10Won);
            chart.Series["Lost"].Points.AddY(totalTeam10Lost);

            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, "Queensland Firebirds");
            index++;

        }

        private void buildAllData(String year)
        {
            foreach (Team t in teams)
            {
                allData.Add(t, year);
            }
        }

        private void vsTeamcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (data_listBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a season");
            }


            else
            {

                // clear the chart
                foreach (var series in chart2.Series)
                {
                    series.Points.Clear();
                }

                // clear the AxisX
                foreach (var charArea in chart2.ChartAreas)
                {
                    charArea.AxisX.CustomLabels.Clear();
                }

                chart2.Series.Clear();
                chart2.Titles.Clear();

                drawWonLost = false;


                int index = 0;



                // Central Pulse
                if (vsTeamcomboBox.SelectedIndex == 0)
                {
                    chart2.Series.Add("Central Pulse");
                    chart2.Series["Central Pulse"].ChartType = SeriesChartType.Spline;
                    chart2.Series["Central Pulse"].MarkerStyle = MarkerStyle.Diamond;
                    chart2.Series["Central Pulse"].MarkerBorderWidth = 10;
                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Central Pulse"].Points.AddY(m.getHomeScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Central Pulse"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Central Pulse"].Points.AddY(m.getAwayScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Central Pulse"].Points[index].ToolTip = m.getInfo();
                            index++;

                        }
                    }

                    chart2.Titles.Add("The score for team " + vsTeamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                // Northern Mystics
                else if (vsTeamcomboBox.SelectedIndex == 1)
                {
                    chart2.Series.Add("Northern Mystics");
                    chart2.Series["Northern Mystics"].ChartType = SeriesChartType.Spline;
                    chart2.Series["Northern Mystics"].MarkerStyle = MarkerStyle.Diamond;
                    chart2.Series["Northern Mystics"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Northern Mystics"].Points.AddY(m.getHomeScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Northern Mystics"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Northern Mystics"].Points.AddY(m.getAwayScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Northern Mystics"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                        //chart.Series["Northern Mystics"].Points.
                    }

                    chart2.Titles.Add("The score for team " + vsTeamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                // Waikato Bay of Plenty Magic
                else if (vsTeamcomboBox.SelectedIndex == 2)
                {
                    chart2.Series.Add("Waikato Bay of Plenty Magic");
                    chart2.Series["Waikato Bay of Plenty Magic"].ChartType = SeriesChartType.Spline;
                    chart2.Series["Waikato Bay of Plenty Magic"].MarkerStyle = MarkerStyle.Diamond;
                    chart2.Series["Waikato Bay of Plenty Magic"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Waikato Bay of Plenty Magic"].Points.AddY(m.getHomeScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Waikato Bay of Plenty Magic"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Waikato Bay of Plenty Magic"].Points.AddY(m.getAwayScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Waikato Bay of Plenty Magic"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart2.Titles.Add("The score for team " + vsTeamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                // Southern Steel
                else if (vsTeamcomboBox.SelectedIndex == 3)
                {
                    chart2.Series.Add("Southern Steel");
                    chart2.Series["Southern Steel"].ChartType = SeriesChartType.Spline;
                    chart2.Series["Southern Steel"].MarkerStyle = MarkerStyle.Diamond;
                    chart2.Series["Southern Steel"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Southern Steel"].Points.AddY(m.getHomeScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Southern Steel"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Southern Steel"].Points.AddY(m.getAwayScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Southern Steel"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart2.Titles.Add("The score for team " + vsTeamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                // Canterbury Tactix
                else if (vsTeamcomboBox.SelectedIndex == 4)
                {
                    chart2.Series.Add("Canterbury Tactix");
                    chart2.Series["Canterbury Tactix"].ChartType = SeriesChartType.Spline;
                    chart2.Series["Canterbury Tactix"].MarkerStyle = MarkerStyle.Diamond;
                    chart2.Series["Canterbury Tactix"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Canterbury Tactix"].Points.AddY(m.getHomeScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Canterbury Tactix"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Canterbury Tactix"].Points.AddY(m.getAwayScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Canterbury Tactix"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart2.Titles.Add("The score for team " + vsTeamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");
                }

                // New South Wales Swifts
                else if (vsTeamcomboBox.SelectedIndex == 5)
                {
                    chart2.Series.Add("New South Wales Swifts");
                    chart2.Series["New South Wales Swifts"].ChartType = SeriesChartType.Spline;
                    chart2.Series["New South Wales Swifts"].MarkerStyle = MarkerStyle.Diamond;
                    chart2.Series["New South Wales Swifts"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["New South Wales Swifts"].Points.AddY(m.getHomeScore());
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["New South Wales Swifts"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["New South Wales Swifts"].Points.AddY(m.getAwayScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["New South Wales Swifts"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart2.Titles.Add("The score for team " + vsTeamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                 // Adelaide Thunderbirds
                else if (vsTeamcomboBox.SelectedIndex == 6)
                {
                    chart2.Series.Add("Adelaide Thunderbirds");
                    chart2.Series["Adelaide Thunderbirds"].ChartType = SeriesChartType.Spline;
                    chart2.Series["Adelaide Thunderbirds"].MarkerStyle = MarkerStyle.Diamond;
                    chart2.Series["Adelaide Thunderbirds"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Adelaide Thunderbirds"].Points.AddY(m.getHomeScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Adelaide Thunderbirds"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Adelaide Thunderbirds"].Points.AddY(m.getAwayScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Adelaide Thunderbirds"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart2.Titles.Add("The score for team " + vsTeamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                 // Melbourne Vixens
                else if (vsTeamcomboBox.SelectedIndex == 7)
                {
                    chart2.Series.Add("Melbourne Vixens");
                    chart2.Series["Melbourne Vixens"].ChartType = SeriesChartType.Spline;
                    chart2.Series["Melbourne Vixens"].MarkerStyle = MarkerStyle.Diamond;
                    chart2.Series["Melbourne Vixens"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Melbourne Vixens"].Points.AddY(m.getHomeScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Melbourne Vixens"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Melbourne Vixens"].Points.AddY(m.getAwayScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Melbourne Vixens"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart2.Titles.Add("The score for team " + vsTeamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                // West Coast Fever
                else if (vsTeamcomboBox.SelectedIndex == 8)
                {
                    chart2.Series.Add("West Coast Fever");
                    chart2.Series["West Coast Fever"].ChartType = SeriesChartType.Spline;
                    chart2.Series["West Coast Fever"].MarkerStyle = MarkerStyle.Diamond;
                    chart2.Series["West Coast Fever"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["West Coast Fever"].Points.AddY(m.getHomeScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["West Coast Fever"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["West Coast Fever"].Points.AddY(m.getAwayScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["West Coast Fever"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart2.Titles.Add("The score for team " + vsTeamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }


                // Queensland Firebirds
                else if (vsTeamcomboBox.SelectedIndex == 9)
                {
                    chart2.Series.Add("Queensland Firebirds");
                    chart2.Series["Queensland Firebirds"].ChartType = SeriesChartType.Spline;
                    chart2.Series["Queensland Firebirds"].MarkerStyle = MarkerStyle.Diamond;
                    chart2.Series["Queensland Firebirds"].MarkerBorderWidth = 10;

                    foreach (Match m in matches)
                    {
                        if (m.getHomeTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Queensland Firebirds"].Points.AddY(m.getHomeScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Queensland Firebirds"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }

                        if (m.getAwayTeam().Equals(vsTeamcomboBox.SelectedItem))
                        {
                            chart2.Series["Queensland Firebirds"].Points.AddY(m.getAwayScore());
                            chart2.ChartAreas[0].AxisX.CustomLabels.Add(index + 0.5, index + 1.5, m.getRoundNum().ToString());
                            chart2.Series["Queensland Firebirds"].Points[index].ToolTip = m.getInfo();
                            index++;
                        }
                    }

                    chart2.Titles.Add("The score for team " + vsTeamcomboBox.Text + " in " + data_listBox.Text + " for each round they played");

                }
            }
        }
    }
}