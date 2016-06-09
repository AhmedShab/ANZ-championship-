using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ANZ_championship
{
    class Match
    {
        int roundNum = 0;
        String date = "";
        String time = "";
        String homeTeam = "";
        int homeScore = 0;
        String awayTeam = "";
        int awayScore = 0;
        String venue = "";


        public Match(int roundNum)
        {
            this.setRoundNum(roundNum);
        }


        public int getRoundNum()
        {
            return roundNum;
        }


        public void setRoundNum(int roundNum)
        {
            this.roundNum = roundNum;
        }


        public String getDate()
        {
            return date;
        }


        public void setDate(String date)
        {
            this.date = date;
        }


        public String getTime()
        {
            return time;
        }


        public void setTime(String time)
        {
            this.time = time;
        }


        public String getHomeTeam()
        {
            return homeTeam;
        }


        public void setHomeTeam(String homeTeam)
        {
            this.homeTeam = homeTeam;
        }


        public int getHomeScore()
        {
            return homeScore;
        }


        public void setHomeScore(int homeScore)
        {
            this.homeScore = homeScore;
        }


        public String getAwayTeam()
        {
            return awayTeam;
        }


        public void setAwayTeam(String awayTeam)
        {
            this.awayTeam = awayTeam;
        }


        public int getAwayScore()
        {
            return awayScore;
        }


        public void setAwayScore(int awayScore)
        {
            this.awayScore = awayScore;
        }


        public String getVenue()
        {
            return venue;
        }


        public void setVenue(String venue)
        {
            this.venue = venue;
        }


        public String getInfo()
        {


            String str = roundNum + "  " + date + "  " + time + "  \n " + homeTeam + " VS " + awayTeam + "\n " + venue;




            return str;
        }
    }
}
