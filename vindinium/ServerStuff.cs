using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Vindinium.SmartBoard;

namespace Vindinium
{
    public class ServerStuff
    {
        private string key;
        private uint turns;
        private string map;
        private string serverURL;

        public bool InTrainingMode { get; private set; }
        public string PlayURL { get; private set; }
        public string ViewURL { get; private set; }
        public string Uri { get; private set; }

        public TileType[][] BoardArray;
        public GameResponse GameResponse;

        public bool Errored { get; private set; }
        public string ErrorText { get; private set; }

        public GameState GameState { get; private set; }

        //if training mode is false, turns and map are ignored8
        public ServerStuff(string key, bool trainingMode, uint turns, string serverURL, string map)
        {
            this.key = key;
            this.InTrainingMode = trainingMode;
            this.serverURL = serverURL;

            //the reaons im doing the if statement here is so that i dont have to do it later
            if (InTrainingMode)
            {
                this.turns = turns;
                this.map = map;
                Uri = serverURL + "/api/training";
            }
            else
            {
                Uri = serverURL + "/api/arena";
            }
        }

        //initializes a new game, its syncronised
        public GameState CreateGame()
        {
            Errored = false;

            string myParameters = "key=" + key;
            if (InTrainingMode) myParameters += "&turns=" + turns;
            if (map != null) myParameters += "&map=" + map;

            //make the request
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                try
                {
                    string result = client.UploadString(Uri, myParameters);
                    return Deserialize(result);
                }
                catch (WebException exception)
                {
                    Errored = true;
                    using (var reader = new StreamReader(exception.Response.GetResponseStream()))
                    {
                        ErrorText = reader.ReadToEnd();
                    }
                    return null;
                }
            }
        }

        private GameState Deserialize(string json)
        {
            // convert string to stream
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            //byte[] byteArray = Encoding.ASCII.GetBytes(json);
            MemoryStream stream = new MemoryStream(byteArray);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(GameResponse));
            GameResponse gameResponse = (GameResponse)ser.ReadObject(stream);
            GameResponse = gameResponse; // TODO debug

            PlayURL = gameResponse.playUrl;
            ViewURL = gameResponse.viewUrl;


            Hero myHero = gameResponse.hero;
            List<Hero> heroes = gameResponse.game.heroes;

            int currentTurn = gameResponse.game.turn;
            int maxTurns = gameResponse.game.maxTurns;
            bool isFinished = gameResponse.game.finished;

            BoardArray = CreateBoard(gameResponse.game.board.size, gameResponse.game.board.tiles);
            Board board = null;
            if (GameState == null)
            {
                board = new Board(BoardArray.Select(row => row.ToList()).ToList());
            }
            else
            {
                board = GameState.Board.Refresh(BoardArray.Select(row => row.ToList()).ToList());
            }
            GameState = new GameState(myHero, heroes, currentTurn, maxTurns, isFinished, board);

            return GameState;
        }

        public GameState MoveHero(string direction)
        {
            string myParameters = "key=" + key + "&dir=" + direction;

            //make the request
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                try
                {
                    string result = client.UploadString(PlayURL, myParameters);
                    return Deserialize(result);
                }
                catch (WebException exception)
                {
                    Errored = true;
                    using (var reader = new StreamReader(exception.Response.GetResponseStream()))
                    {
                        ErrorText = reader.ReadToEnd();
                    }
                    return null;
                }
            }
        }

        private TileType[][] CreateBoard(int size, string data)
        {
            TileType[][] boardArray = new TileType[size][];

            //need to initialize the lists within the list
            for (int i = 0; i < size; i++)
            {
                boardArray[i] = new TileType[size];
            }


            //convert the string to the TileType[][]
            int x = 0;
            int y = 0;
            char[] charData = data.ToCharArray();

            for (int i = 0; i < charData.Length; i += 2)
            {
                switch (charData[i])
                {
                    case '#':
                        boardArray[x][y] = TileType.IMPASSABLE_WOOD;
                        break;
                    case ' ':
                        boardArray[x][y] = TileType.FREE;
                        break;
                    case '@':
                        switch (charData[i + 1])
                        {
                            case '1':
                                boardArray[x][y] = TileType.HERO_1;
                                break;
                            case '2':
                                boardArray[x][y] = TileType.HERO_2;
                                break;
                            case '3':
                                boardArray[x][y] = TileType.HERO_3;
                                break;
                            case '4':
                                boardArray[x][y] = TileType.HERO_4;
                                break;

                        }
                        break;
                    case '[':
                        boardArray[x][y] = TileType.TAVERN;
                        break;
                    case '$':
                        switch (charData[i + 1])
                        {
                            case '-':
                                boardArray[x][y] = TileType.GOLD_MINE_NEUTRAL;
                                break;
                            case '1':
                                boardArray[x][y] = TileType.GOLD_MINE_1;
                                break;
                            case '2':
                                boardArray[x][y] = TileType.GOLD_MINE_2;
                                break;
                            case '3':
                                boardArray[x][y] = TileType.GOLD_MINE_3;
                                break;
                            case '4':
                                boardArray[x][y] = TileType.GOLD_MINE_4;
                                break;
                        }
                        break;
                }

                //time to increment x and y
                y++;
                if (y == size)
                {
                    y = 0;
                    x++;
                }
            }
            return boardArray;
        }
    }
}