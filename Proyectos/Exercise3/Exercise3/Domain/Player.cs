using Exercise3.Persistence.manages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise3.Domain
{
    class Player
    {
        private static int nextId = 1;
        private int id;
        private string nickname;
        private DateTime gameDate;
        private int playerLevel;
        private int score;
        public RankingManage rm;


        public Player(string nickname, DateTime gameDate, int playerLevel, int score)
        {
            rm = new RankingManage();

            this.id = rm.getLastId(this);
            this.nickname = nickname;
            this.gameDate = gameDate;
            this.playerLevel = playerLevel;
            this.score = score;
        }

        public Player(int id, string nickname, DateTime gameDate, int playerLevel, int score)
        {
            rm = new RankingManage();

            this.id = id;
            this.nickname = nickname;
            this.gameDate = gameDate;
            this.playerLevel = playerLevel;
            this.score = score;
        }

        public Player ()
        {
            rm = new RankingManage();
        }


        public void insertarNuevoPlayer()
        {
            rm.insertarNuevoPlayer(this);
        }

        public string Nickname { get => nickname; set => nickname = value; }
        public DateTime GameDate { get => gameDate; set => gameDate = value; }
        public int PlayerLevel { get => playerLevel;    set => playerLevel = value; }
        public int Score { get => score; set => score = value; }
        public int Id { get => id; set => id = value; }
    }
}
