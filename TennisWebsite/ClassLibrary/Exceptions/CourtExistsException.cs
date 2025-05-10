namespace TennisWebsite.ClassLibrary.Exceptions
{
    public class CourtExistsException: Exception
    {
        public CourtExistsException() { }
        public CourtExistsException(string message) : base(message){ }
    }
}
