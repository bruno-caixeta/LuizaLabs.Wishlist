namespace LuizaLabs.Wishlist.App.ResponseModels
{
    public class ClientResponseMessages
    {
        public string ClientNotFound { get; set; }
        public string ClientNotFoundDescription { get; set; }
        public string EmailAlreadyUsed { get; set; }
        public string EmailAlreadyUsedDescription { get; set; }
        public string NoClientChanged { get; set; }
        public string NoClientChangedDescription { get; set; }
        public string InternalError { get; set; }
        public string InternalErrorDescription { get; set; }
    }
}
