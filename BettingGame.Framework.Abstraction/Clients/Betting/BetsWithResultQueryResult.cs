namespace BettingGame.Framework.Abstraction.Clients.Betting
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.42.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class BetsWithResultQueryResult : System.ComponentModel.INotifyPropertyChanged
    {
        private System.Guid? _id;
        private int? _score;
        private System.Guid? _userId;

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

        [Newtonsoft.Json.JsonProperty("score", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Score
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("userId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Guid? UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static BetsWithResultQueryResult FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<BetsWithResultQueryResult>(data);
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