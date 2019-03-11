namespace BettingGame.Framework.Abstraction.Clients.Betting
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.42.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class SetActualResultCommand : System.ComponentModel.INotifyPropertyChanged
    {
        private System.Guid _gameId;
        private int _scoreTeamA;
        private int _scoreTeamB;

        [Newtonsoft.Json.JsonProperty("gameId", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid GameId
        {
            get { return _gameId; }
            set
            {
                if (_gameId != value)
                {
                    _gameId = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("scoreTeamA", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Range(0, 50)]
        public int ScoreTeamA
        {
            get { return _scoreTeamA; }
            set
            {
                if (_scoreTeamA != value)
                {
                    _scoreTeamA = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("scoreTeamB", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Range(0, 50)]
        public int ScoreTeamB
        {
            get { return _scoreTeamB; }
            set
            {
                if (_scoreTeamB != value)
                {
                    _scoreTeamB = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static SetActualResultCommand FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<SetActualResultCommand>(data);
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

    }
}