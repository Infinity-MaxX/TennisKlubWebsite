namespace TennisWebsite.ClassLibrary.Models
{
    public class Gear
    {
        #region Instances
        private int _id;
        private bool _isAvailable;
        #endregion

        #region Properties
        public int Count { get; set; }
        public int ID { get { return _id; } }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Status
        {
            get { return _isAvailable; }
            set { _isAvailable = value; }
        }
        #endregion

        #region Constructor
        // default cosntructor
        public Gear()
        {

        }

        // parameterised constructor
        public Gear(string name, string? description)
        {
            _id++;
            _isAvailable = true;
            if (null != description) { Description = description; }
        }
        public Gear(int id, string name, string? description)
        {
            _id = id;
            _isAvailable = true;
            if (null != description) { Description = description; }
        }
        #endregion
    }
}
