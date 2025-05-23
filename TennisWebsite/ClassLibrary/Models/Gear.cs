namespace TennisWebsite.ClassLibrary.Models
{
    public class Gear
    {
        #region Instances
        private int _id;
        #endregion

        #region Properties
        public int Count { get; set; }
        public int ID { get { return _id; } }
        public string Name { get; set; }
        public string? Description { get; set; }
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
            if (null != description) { Description = description; }
        }
        public Gear(int id, string name, string? description)
        {
            _id = id;
            if (null != description) { Description = description; }
        }
        #endregion
    }
}
