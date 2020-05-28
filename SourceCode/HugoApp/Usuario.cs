namespace HugoApp
{
    public class Usuario
    {
        public string iduser { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool userType { get; set; }
        

        public Usuario()
        {
            iduser = "";
            fullname = "";
            username = "";
            password = "";
            userType = false;
        }
    }
}