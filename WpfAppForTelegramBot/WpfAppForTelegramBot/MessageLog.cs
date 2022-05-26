namespace WpfAppForTelegramBot
{
    internal struct MessageLog
    {
        public string Time { get; set; }
        public long Id { get; set; }
        public string Message { get; set; }
        public string FirstName { get; set; }

        public MessageLog(string time, string message, string firstName, long id)
        {
            Time = time;
            Message = message;
            FirstName = firstName;
            Id = id;
        }

        //public override string ToString()
        //{
        //    return $"{Time} {Msg} {FirstName}";
        //}
    }
}
