namespace BettingGame.Framework.Abstraction.Clients.Betting
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.42.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class SaveTeamMetadata : System.ComponentModel.INotifyPropertyChanged
    {
        private System.Guid? _id;
        private System.String _fifaCode;

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

        [Newtonsoft.Json.JsonProperty("fifaCode", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.String FifaCode
        {
            get { return _fifaCode; }
            set
            {
                if (_fifaCode != value)
                {
                    _fifaCode = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static SaveTeamMetadata FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<SaveTeamMetadata>(data);
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