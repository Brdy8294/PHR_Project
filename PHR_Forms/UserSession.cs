namespace PHR_Forms
{
    public static class UserSession
    {
        public static int MemberId { get; set; } = 0;

        public static string UserName { get; set; } = string.Empty;

        public static bool IsLoggedIn
        {
            get { return MemberId != 0; }
        }

        public static void ClearSession()
        {
            MemberId = 0;
            UserName = string.Empty;
        }
    }
}