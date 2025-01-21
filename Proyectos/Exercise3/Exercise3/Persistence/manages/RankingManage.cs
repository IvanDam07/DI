using AgendaContactosBBDD.persistence;
using Exercise3.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise3.Persistence.manages
{
    class RankingManage
    {
        DBBroker dbbroker;
        Player player;
        int lastId;
        public RankingManage()
        {
            dbbroker = DBBroker.obtenerAgente();
            player = new Player();
        }

        public int getLastId(Player player)
        {
            List<Object> listaAux;
            listaAux = DBBroker.obtenerAgente().leer("SELECT COALESCE(MAX(id), 0) FROM mydb.Player");

            foreach (List<Object> aux in listaAux)
            {
                lastId = Convert.ToInt32(aux[0]) + 1;
            }
            return lastId;
        }

        public void insertarNuevoPlayer(Player player)
        {
            dbbroker.modificar("INSERT INTO mydb.Player (nickname, date, level, score) VALUES ( " + player.Nickname + ", '" + player.GameDate + "', " + player.PlayerLevel + ", " + player.Score + ")");
        }

        public void eliminarPlayer(Player player)
        {
            DBBroker.obtenerAgente().modificar("DELETE FROM mydb.Player WHERE id = " + player.Id + ";");
        }

        public void modificarPlayer(Player player)
        {
            DBBroker.obtenerAgente().modificar("UPDATE mydb.Player SET nickname = '" + player.Nickname + "', level = " + player.PlayerLevel + ";");
        }
    }
}
