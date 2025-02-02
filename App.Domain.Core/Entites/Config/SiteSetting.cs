namespace App.Domain.Core.Entites.Config
{
    public class SiteSetting
    {
        public Connectionstring ConnectionString { get; set; }

        public LimitTask LimitTask { get; set; }
    }
    public class Connectionstring
    {
        public string SqlConnection { get; set; }
    }

    public class LimitTask
    {
        public string TaskUnfinished { get; set; }
    }


}
