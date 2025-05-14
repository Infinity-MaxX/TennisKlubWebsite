namespace TennisWebsite.ClassLibrary.Helpers
{
    public class DLStringScoreObject<T>
    {
        public T Data { get; private set; }

        public int Score { get; set; }

        public string DLString { get; set; }

        public DLStringScoreObject(T data, int score, string dLString)
        {
            Data = data;
            Score = score;
            DLString = dLString;
        }
    }
}
