namespace GrindTracker
{
    public  interface IExtractor
    {
        public  void extract(Message msg, Tracker tracker);
    }
}