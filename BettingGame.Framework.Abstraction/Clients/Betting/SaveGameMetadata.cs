namespace BettingGame.Framework.Abstraction.Clients.Betting
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.42.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class SaveGameMetadata : System.ComponentModel.INotifyPropertyChanged
    {
        private System.Guid? _id;
        private System.DateTimeOffset _startDate;
        private System.Guid? _teamA;
        private System.Guid? _teamB;

        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Guid? Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("startDate", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.DateTimeOffset StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("teamA", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Guid? TeamA
        {
            get { return _teamA; }
            set
            {
                if (_teamA != value)
                {
                    _teamA = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("teamB", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Guid? TeamB
        {
            get { return _teamB; }
            set
            {
                if (_teamB != value)
                {
                    _teamB = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static SaveGameMetadata FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<SaveGameMetadata>(data);
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