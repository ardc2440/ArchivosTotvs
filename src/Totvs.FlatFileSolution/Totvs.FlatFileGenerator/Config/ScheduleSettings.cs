namespace Totvs.FlatFileGenerator.Config
{
    internal class ScheduleSettings
    {
        public string ExecuteTiming { get; set; }
        public int ExecuteDelayInSeconds { get; set; }
        public string CleaningTiming { get; set; }
        public int CleaningDelayInDays { get; set; }
    }
}
