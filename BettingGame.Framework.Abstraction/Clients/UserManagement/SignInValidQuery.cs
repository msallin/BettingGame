namespace BettingGame.Framework.Abstraction.Clients.UserManagement
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.42.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class SignInValidQuery : System.ComponentModel.INotifyPropertyChanged
    {
        private string _email;
        private string _password;

        [Newtonsoft.Json.JsonProperty("email", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("password", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static SignInValidQuery FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<SignInValidQuery>(data);
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