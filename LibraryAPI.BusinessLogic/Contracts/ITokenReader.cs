namespace LibraryAPI.BusinessLogic.Contracts
{
    public interface ITokenReader
    {
        public int UserId { get; }
        public string Email { get; }
        public string FullName { get; }
        public string Role { get; }
        public bool IsLoggedIn { get; }
    }
}
